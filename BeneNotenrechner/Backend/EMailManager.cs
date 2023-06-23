

using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

namespace BeneNotenrechner.Backend {
    public class EMailManager 
    {

        public static async void TestAPI() {

            Console.WriteLine("Net Test");

            NetTokenEmail _netTokenEmail = new NetTokenEmail("elkordhicham@gmail.com", "13579");

            using (var wb = new HttpClient()) {
                StringContent data = new StringContent(JsonSerializer.Serialize(_netTokenEmail), Encoding.UTF8, "application/json");

                string url = "http://localhost:5008/api/Demo";

                using (var client = new HttpClient()) {
                    HttpResponseMessage response = await client.PostAsync(url, data);

                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }

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
