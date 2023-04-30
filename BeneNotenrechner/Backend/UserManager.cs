namespace BeneNotenrechner.Backend
{
    public class UserManager
    {
        private static List<User> userlist = new List<User>();
        private static Random random = new Random();

        public static Tuple<string, string> LoginUser(string _username, string _password) {
            int _userid = DBManager.instance.GetUser(_username, _password);

            if (_userid == -1) {
                _userid = DBManager.instance.CreateUser(_username, _password);

                if (_userid == -1) 
                    return new Tuple<string, string>("", "Error: could not create new user");
            }

            uint token = (uint)random.Next(1_000_000_000, int.MaxValue);

            userlist.Add(new User(_userid, _username, token));

            return new Tuple<string, string>(token.ToString(), "");
        }
    }
}
