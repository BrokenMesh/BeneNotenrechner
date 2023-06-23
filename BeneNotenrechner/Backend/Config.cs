using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner.Backend 
{

    public class ConfigLoader 
    {

        public static Config LoadConfig(string _path) {

            try {
                string _file = File.ReadAllText(_path);
                Config? _config = JsonSerializer.Deserialize<Config>(_file);

                if(_config != null) { 
                    return _config; 
                }

            } catch (Exception _e) {
                Console.WriteLine("Could not load Config file: " + _e);
                Console.WriteLine("Using standart configuration");
            }

            return new Config("localhost", "root", "", "benenotenrechner_db");
        }
    }

    public class Config 
    {   
        [Required] public string DB_Server { get; }
        [Required] public string DB_User { get; } 
        [Required] public string DB_Password { get; }
        [Required] public string DB_Database { get; }

        public Config(string dB_Server, string dB_User, string dB_Password, string dB_Database) {
            DB_Server = dB_Server;
            DB_User = dB_User;
            DB_Password = dB_Password;
            DB_Database = dB_Database;
        }
    }
}
