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

            return new Config("localhost", "root", "", "benenotenrechner_db", "smtp.gmail.com", "elkordhicham55demo@gmail.com", "evhqqxffufyogqqp", "");
        }
    }

    public class Config 
    {   
        [Required] public string DB_Server { get; }
        [Required] public string DB_User { get; } 
        [Required] public string DB_Password { get; }
        [Required] public string DB_Database { get; }
        [Required] public string MAIL_SMTPServer { get; }
        [Required] public string MAIL_HostMail{ get; }
        [Required] public string MAIL_HostPassword { get; }
        [Required] public string MAIL_HTMLPath{ get; }

        public Config(string dB_Server, string dB_User, string dB_Password, string dB_Database, string mAIL_SMTPServer, string mAIL_HostMail, string mAIL_HostPassword, string mAIL_HTMLPath) {
            DB_Server = dB_Server;
            DB_User = dB_User;
            DB_Password = dB_Password;
            DB_Database = dB_Database;
            MAIL_SMTPServer = mAIL_SMTPServer;
            MAIL_HostMail = mAIL_HostMail;
            MAIL_HostPassword = mAIL_HostPassword;
            MAIL_HTMLPath = mAIL_HTMLPath;
        }
    }
}
