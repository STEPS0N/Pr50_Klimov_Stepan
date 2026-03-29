using AppKeyPass.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppKeyPass.Context
{
    public class UserContext
    {
        static string url = "https://localhost:7092/user/";

        public static async Task<string> Login(string login, string password)
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, url + "login"))
                {
                    Dictionary<string, string> FormData = new Dictionary<string, string>
                    {
                        ["login"] = login,
                        ["password"] = password
                    };
                    FormUrlEncodedContent Content = new FormUrlEncodedContent(FormData);
                    Request.Content = Content;
                    var Response = await Client.SendAsync(Request);

                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        Auth DataAuth = JsonConvert.DeserializeObject<Auth>(sResponse);
                        return DataAuth.Token;
                    }
                }
            }
            return null;
        }
    }
}
