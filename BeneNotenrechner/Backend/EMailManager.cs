

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace BeneNotenrechner.Backend {
    public class EMailManager 
    {
        public string API_TokenMailUrl = "";

        public static EMailManager instance { get; private set; }

        public EMailManager(Config _config) {
            instance = this;
            API_TokenMailUrl = _config.Mail_TokenMailUrl;
        }

        // Send an API request to the Dev_API Project to send out a Token E-Mail.
        // This is done because asp.net has strict certification rules when it comes to webservers.
        // you cannot just start another ssl connection from the webserver but you can do an API fetch.
        // So, the Dev_API project handles the actual SMTP request and here we just call the required API
        public async void SendTokenMail(string _mail, string _token) {
            NetTokenEmail _netTokenEmail = new NetTokenEmail(_mail, _token);

            try {
                using (var wb = new HttpClient()) {
                    StringContent data = new StringContent(JsonSerializer.Serialize(_netTokenEmail), Encoding.UTF8, "application/json");

                    string url = API_TokenMailUrl;

                    using (var client = new HttpClient()) {
                        HttpResponseMessage response = await client.PostAsync(url, data);

                        string result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                }
            }
            catch (Exception _e) {
                Console.WriteLine("Sending E-Mail with api call failed: " + _e);
            }

        }

    }

    public class NetTokenEmail {
        [Required] public string EMail { get; }
        [Required] public string Token { get; }

        public NetTokenEmail(string eMail, string token) {
            EMail = eMail;
            Token = token;
        }
    }
}
