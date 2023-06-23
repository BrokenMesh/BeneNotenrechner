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

        public static void Start() { 
            timer = new Timer((object? state) => { 
                CheckForOutdatedUsers(); 
            }, null, 0, OutdatedUsersCheckPeriod_mil);
        }

        public static Tuple<string, string> LoginUser(string _username, string _password, string _salt) {

            EMailManager.instance.SendTokenMail("elkordhicham@gmail.com", "this is a token");

            int _userid = DBManager.instance.GetUser(_username);

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
            if (!ValidateMail(_usermail)) return "E-Mail Adresse ist nicht valid!";

            string _validationToken = EncriptionManager.GenRandomString(8).ToUpper();

            tempUserlist.Add(_validationToken, new TempUser(_username, _password, _usermail));

            return "";
        }

        private static bool ValidateMail(string _mail) {
            var valid = true;

            try {
                var emailAddress = new MailAddress(_mail);
            }
            catch {
                valid = false;
            }

            return valid;
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
    }
}
