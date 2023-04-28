using MySql.Data.MySqlClient;

namespace BeneNotenrechner {
    public class DBManager {

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

        public string GetData() {
            string sql = $"SELECT date, customerID FROM bill WHERE idBill={counter}";
            MySqlCommand command = new MySqlCommand(sql, db);
            MySqlDataReader reader = command.ExecuteReader();

            string output = "";

            while (reader.Read()) {
                output += reader[0] + " -- " + reader[1];
            }
            reader.Close();

            counter++;

            return output;
        }

    }
}
