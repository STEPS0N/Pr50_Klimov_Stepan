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
    public class StorageContext
    {
        static string url = "https://localhost:7092/storage/";

        public static async Task<List<Storage>?> Get()
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Get, url + "get"))
                {
                    Request.Headers.Add("token", MainWindow.Token);

                    var Response = await Client.SendAsync(Request);

                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        List<Storage> Storages = JsonConvert.DeserializeObject<List<Storage>>(sResponse);
                        return Storages;
                    }
                }
            }
            return null;
        }

        public static async Task<Storage> Add(Storage storage)
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, url + "add"))
                {
                    Request.Headers.Add("token", MainWindow.Token);

                    string JsonStorage = JsonConvert.SerializeObject(storage);

                    var Content = new StringContent(JsonStorage, Encoding.UTF8, "application/json");
                    Request.Content = Content;

                    var Response = await Client.SendAsync(Request);

                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        Storage Storage = JsonConvert.DeserializeObject<Storage>(sResponse);
                        return Storage;
                    }
                }
            }
            return null;
        }

        public static async Task<Storage> Update(Storage storage)
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Put, url + "update"))
                {
                    Request.Headers.Add("token", MainWindow.Token);

                    string JsonStorage = JsonConvert.SerializeObject(storage);

                    var Content = new StringContent(JsonStorage, Encoding.UTF8, "application/json");
                    Request.Content = Content;

                    var Response = await Client.SendAsync(Request);

                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        Storage Storage = JsonConvert.DeserializeObject<Storage>(sResponse);
                        return Storage;
                    }
                }
            }
            return null;
        }

        public static async Task Delete(int id)
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Delete, url + "delete"))
                {
                    Request.Headers.Add("token", MainWindow.Token);

                    Dictionary<string, string> FormData = new Dictionary<string, string>
                    {
                        ["id"] = id.ToString()
                    };

                    FormUrlEncodedContent Content = new FormUrlEncodedContent(FormData);
                    Request.Content = Content;

                    var Response = await Client.SendAsync(Request);

                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}
