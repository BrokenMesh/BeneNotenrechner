using Org.BouncyCastle.Bcpg.OpenPgp;

namespace BeneNotenrechner.Backend
{
    public class UserManager
    {
        private static Dictionary<string, User> userlist = new Dictionary<string, User>();
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

            EMailManager.TestAPI();

            bool _isNewUser = false;

            // Validation
            if (_username.Length > 45) return new Tuple<string, string>("", "Error: username is to long");

            int _userid = DBManager.instance.GetUser(_username);

            if (_userid == -1) { // TODO: New Users are created here, this must be moved!
                _userid = DBManager.instance.CreateUser(_username, _password);
                _isNewUser = true;
                if (_userid == -1) return new Tuple<string, string>("", "Error: could not create new user");
            }

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

            if (_isNewUser) {
                DBManager.instance.CreateProfile(userlist[_token], 1);
            }

            Console.Write("> ");
            foreach (User _user in userlist.Values) 
                Console.Write($"{_user.Username}, ");
            Console.WriteLine();

            return new Tuple<string, string>(_token.ToString(), "");
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
