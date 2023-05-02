namespace BeneNotenrechner.Backend
{
    public class UserManager
    {
        private static Dictionary<uint, User> userlist = new Dictionary<uint, User>();
        private static Random random = new Random();

        private static Timer timer;

        private const int OutdatedUsersCheckPeriod_mil = 1000 * 60 * 5;
        private const float MaxOutdatedTime_min = 7f;

        public static void Start() { 
            timer = new Timer((object? state) => { 
                CheckForOutdatedUsers(); 
            }, null, 0, OutdatedUsersCheckPeriod_mil);
        }

        public static Tuple<string, string> LoginUser(string _username, string _password) {
            if (_username.Length > 45) return new Tuple<string, string>("", "Error: username is to long");

            int _userid = DBManager.instance.GetUser(_username);

            if (_userid == -1) {
                _userid = DBManager.instance.CreateUser(_username, _password);
                if (_userid == -1) return new Tuple<string, string>("", "Error: could not create new user");
            }

            if (!DBManager.instance.CheckPassword(_userid, _password)) return new Tuple<string, string>("", "Password ist incorrect!");  

            uint _token = (uint)random.Next(1_000_000_000, int.MaxValue);

            while(userlist.ContainsKey(_token)) {
                _token = (uint)random.Next(1_000_000_000, int.MaxValue);
            }

            userlist.Add(_token, new User(_userid, _username));

            Console.Write("> ");
            foreach (uint _userId in userlist.Keys) 
                Console.Write($"{userlist[_userId].Username}, ");
            Console.WriteLine();

            return new Tuple<string, string>(_token.ToString(), "");
        }

        private static void CheckForOutdatedUsers() {
            List<uint> _usersToDelete = new List<uint>();

            foreach (uint _userId in userlist.Keys) {
                if (userlist[_userId].LastAuthentication.AddMinutes(MaxOutdatedTime_min) < DateTime.Now) {
                    _usersToDelete.Add(_userId);
                }
            }

            foreach (uint _userId in _usersToDelete) {
                Console.WriteLine($"Removed {userlist[_userId].Username} from Users list");
                userlist.Remove(_userId);
            }
        }

        public static void ReauthenticateUser(uint _token) {
            if(userlist.ContainsKey(_token)) {
                userlist[_token].SetAutenticationToNow();
            }
        }
    }
}
