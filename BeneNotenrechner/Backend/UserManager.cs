using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Net.Mail;

namespace BeneNotenrechner.Backend
{
    public class UserManager
    {
        private static Dictionary<string, User> userlist = new Dictionary<string, User>();
        private static Dictionary<string, TempUser> tempUserlist = new Dictionary<string, TempUser>();
        private static Random random = new Random();

        private static Timer timer;

        private const int OutdatedUsersCheckPeriod_mil = 1000 * 60 * 5;

        private const float MaxOutdatedTime_min = 7f;
        private const float MaxOutdateValidationCodeTime_min = 1f;

        private static string ReqiredEmailHost = "";

        public static void Start() { 
            timer = new Timer((object? state) => { 
                CheckForOutdatedUsers(); 
            }, null, 0, OutdatedUsersCheckPeriod_mil);
        }

        public static Tuple<string, string> LoginUser(string _usernameOrMail, string _password, string _salt) {
            int _userid = DBManager.instance.GetUser(_usernameOrMail);

            if (_userid == -1) return new Tuple<string, string>("", "Nutzer konnte nicht gefunden werden!");

            if (!DBManager.instance.CheckPassword(_userid, _password)) return new Tuple<string, string>("", "Password ist incorrect!");

            // Remove duplicate User
            foreach (var _userkvp in userlist) {
                if (_userkvp.Value.Id == _userid) { 
                    userlist.Remove(_userkvp.Key);
                    break;
                }
            }

            // Generate session token for authentication 
            string _token = EncriptionManager.GenRandomString(64);

            while(userlist.ContainsKey(_token)) {
                _token = EncriptionManager.GenRandomString(64);
            }

            string? _username = DBManager.instance.GetUsername(_userid);
            if (_username == null) _username = _usernameOrMail;

            userlist.Add(_token, new User(_userid, _username, _password, _salt));

            Console.Write("> ");
            foreach (User _user in userlist.Values) 
                Console.Write($"{_user.Username}, ");
            Console.WriteLine();

            return new Tuple<string, string>(_token.ToString(), "");
        }

        // Create temporary user for registration
        public static string CreateTempUser(string _username, string _password, string _usermail) {
            if (DBManager.instance.GetUser(_username) != -1) return "Username wird schon verwendet!";
            if (_username.Length > 45) return "Error: username is to long";
            if (_usermail.Length > 100) return "Error: email is to long";
            if (!ValidateMail(_usermail)) return "E-Mail Adresse ist nicht valid!";
            if (ValidateMail(_username)) return "Username darf keine E-Mail Adresse sein!";

            if (!string.IsNullOrEmpty(ReqiredEmailHost)) {
                if (!ValidateMailHost(_usermail, ReqiredEmailHost))
                    return $"E-Mail muss eine '@{ReqiredEmailHost}' sein";
            }

            // Generate Validation
            string _validationToken = EncriptionManager.GenRandomString(8).ToUpper();

            while(userlist.ContainsKey(_validationToken)) {
                _validationToken = EncriptionManager.GenRandomString(8).ToUpper();
            }

            // Remove all outdated and dublicate users
            List<string> _toRemove = new List<string>();

            foreach ((string _key, TempUser _user) in tempUserlist) {
                if (_user.Usermail == _usermail) _toRemove.Add(_key);
                if (_user.TimeOfCreation.AddMinutes(MaxOutdateValidationCodeTime_min) < DateTime.Now)
                    _toRemove.Add(_key);
            }

            foreach (string _key in _toRemove) {
                tempUserlist.Remove(_key);
            }

            // Add User
            tempUserlist.Add(_validationToken, new TempUser(_username, _password, _usermail));

            EMailManager.instance.SendTokenMail(_usermail, _validationToken);

            return "";
        }

        public static string ValidateAndCreateUser(string _validationToken) {
            _validationToken = _validationToken.ToUpper();
            if (!tempUserlist.ContainsKey(_validationToken)) return "Code ist falsch oder abgelaufen!";

            TempUser _tempuser = tempUserlist[_validationToken];

            int _userid = DBManager.instance.CreateUser(_tempuser.Username, _tempuser.Password, _tempuser.Usermail);

            CreateSampleData(_userid);

            return "";
        }

        private static void CreateSampleData(int _userid) {
            User _user = new User(_userid, "", "", "");

            for (int i = 1; i <= 8; i++) {
                DBManager.instance.CreateProfile(_user, i);
            }

            Profile? _profile = DBManager.instance.GetProfile(_user, false);
            if (_profile == null) return;

            DBManager.instance.CreateSuperSubject(_profile, "Informatik Fächer");
            DBManager.instance.CreateSuperSubject(_profile, "Allgemeine Fächer");
        }

        private static bool ValidateMail(string _mail) {
            try {
                var emailAddress = new MailAddress(_mail);
                return true;
            } catch {
                return false;
            }
        }

        private static bool ValidateMailHost(string _mail, string _reqiredHost) {
            try {
                var emailAddress = new MailAddress(_mail);
                return emailAddress.Host == _reqiredHost;
            } catch {
                return false;
            }
        }

        private static void CheckForOutdatedUsers() {
            List<string> _usersToDelete = new List<string>();

            foreach (string _userId in userlist.Keys) {
                if (userlist[_userId].LastAuthentication.AddMinutes(MaxOutdatedTime_min) < DateTime.Now) {
                    _usersToDelete.Add(_userId);
                }
            }

            foreach (string _userId in _usersToDelete) {
                Console.WriteLine($"Removed {userlist[_userId].Username} from Users list");
                userlist.Remove(_userId);
            }
        }

        public static void ReauthenticateUser(string _token) {
            if (string.IsNullOrEmpty(_token)) return;
               
            if(userlist.ContainsKey(_token)) {
                userlist[_token].SetAutenticationToNow();
            }
        }

        public static User? GetUserFromToken(string _token) {
            if (!string.IsNullOrEmpty(_token)) {
                if (userlist.ContainsKey(_token)) {
                    return userlist[_token];
                }
            }

            return null;
        }

        public static void SetReqiredEmailHost(string _reqiredHost) {
            ReqiredEmailHost = _reqiredHost;
        }
    }
}
