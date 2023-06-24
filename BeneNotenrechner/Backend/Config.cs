using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner.Backend 
{
    public class Config 
    {   
        [Required] public string DB_Server { get; }
        [Required] public string DB_User { get; } 
        [Required] public string DB_Password { get; }
        [Required] public string DB_Database { get; }
        [Required] public string Mail_TokenMailUrl { get; }
        [Required] public string Mail_ReqiredHost { get; }

        public Config(string dB_Server, string dB_User, string dB_Password, string dB_Database, string mail_TokenMailUrl, string mail_ReqiredHost) {
            DB_Server = dB_Server;
            DB_User = dB_User;
            DB_Password = dB_Password;
            DB_Database = dB_Database;
            Mail_TokenMailUrl = mail_TokenMailUrl;
            Mail_ReqiredHost = mail_ReqiredHost;
        }

        public static Config LoadConfig(string _path) {

            try {
                string _file = File.ReadAllText(_path);
                Config? _config = JsonSerializer.Deserialize<Config>(_file);

                if (_config != null) {
                    Console.WriteLine("Loaded Config File: " + _path);
                    return _config;
                }
            }
            catch (Exception _e) {
                Console.WriteLine("Could not load Config file: " + _e);
                Console.WriteLine("Using standart configuration");
            }

            return new Config("localhost", "root", "", "benenotenrechner_db", "http://localhost:5008/api/TokenMail", "");
        }
    }
}
