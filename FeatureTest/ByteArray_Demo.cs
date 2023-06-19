using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FeatureTest
{
    public class ByteArray_Demo
    {
        private static Random random = new Random();


        public static void Run() {
            string _constr = $"server=localhost;userid=Hicham;password=Hallosaid1;database=demo-db;";
            MySqlConnection _db = new MySqlConnection(_constr);
            _db.Open();

            Console.WriteLine(_db.ServerVersion);

            byte[] _array = Encoding.ASCII.GetBytes(GenRandomString(64), 0, 64);

            string _sql = "INSERT INTO users(users.name, money, arr) VALUES (@name, @money, @arr)";
            using (MySqlCommand _command = new MySqlCommand(_sql, _db)) {
                _command.Parameters.AddWithValue("@name", "Hichan");
                _command.Parameters.AddWithValue("@money", 12f);
                _command.Parameters.AddWithValue("@arr", _array);
                _command.ExecuteNonQuery();
            }

        }


        private const string randomStringChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string GenRandomString(int _length) {
            string _output = "";

            for (int i = 0; i < _length; i++) {
                _output += randomStringChars[random.Next(randomStringChars.Length)];
            }

            return _output;
        }

    }
}
