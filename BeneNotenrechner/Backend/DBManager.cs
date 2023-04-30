using MySql.Data.MySqlClient;

namespace BeneNotenrechner.Backend
{
    public class DBManager 
    {
        private MySqlConnection db;

        private int counter = 1;

        public static DBManager instance;

        public DBManager(string _server, string _userid, string _password, string _database) {
            instance = this;

            string _constr = $"server={_server};userid={_userid};password={_password};database={_database};";
            db = new MySqlConnection(_constr);
            db.Open();

            Console.WriteLine($"MySql version: {db.ServerVersion}");
        }

        public int CreateUser(string _username, string _password) {
            string _sql = "INSERT INTO users(username, users.password) VALUES (@username,  @password)";
            MySqlCommand _command = new MySqlCommand(_sql, db);

            _command.Parameters.AddWithValue("@username", _username);
            _command.Parameters.AddWithValue("@password", _password);

            _command.ExecuteNonQuery();

            return GetUser(_username, _password);
        }

        public int GetUser(string _username, string _password) {
            string _sql = "SELECT user_id FROM users WHERE username = @username AND password = @password";
            MySqlCommand _command = new MySqlCommand(_sql, db);

            _command.Parameters.AddWithValue("@username", _username);
            _command.Parameters.AddWithValue("@password", _password);

            MySqlDataReader _reader = _command.ExecuteReader();

            int _user_id = -1;

            while (_reader.Read()) {
                _user_id = _reader.GetInt32("user_id"); 
            }

            _reader.Close();

            return _user_id;
        }

    }
}
