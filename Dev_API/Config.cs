using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner_MailAPI
{
    public class Config {
        [Required] public string Mail_Host { get; }
        [Required] public string Mail_UserMail { get; }
        [Required] public string Mail_Password { get; }
        [Required] public string Mail_TokenMailHtmlPath { get; }

        public Config(string mail_Host, string mail_UserMail, string mail_Password, string mail_TokenMailHtmlPath) {
            Mail_Host = mail_Host;
            Mail_UserMail = mail_UserMail;
            Mail_Password = mail_Password;
            Mail_TokenMailHtmlPath = mail_TokenMailHtmlPath;
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

            return new Config("smtp.google.com", "elkordhicham55demo@gmail.com", "evhqqxffufyogqqp", "./TokenEMail.html");
        }
    }
}
