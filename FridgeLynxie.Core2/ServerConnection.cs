using Android.Widget;
using FridgeLynxie.Core.Models;
using FridgeLynxie.Core2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core
{
    public class ServerConnection
    {
        private const string APP_PATH = "http://109.86.186.163:5555";
        private static Network currentNetwork;
        private static User currentUser;

        public string RegisterNewNetwork(string name, string pass)
        {

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Custom", "sample");
                Network net = new Network() { Name = name, Password = pass };
                net.CreatorUserId = 11;
                var response = client.PostAsJsonAsync<Network>(APP_PATH + "/api/networks/CreateNewNetwork", net).Result;
                return response.StatusCode.ToString();
            }
        }

        public void RegisterNewUser(string userName)
        {
            test();
            //return;
            //using (var client = new HttpClient())
            //{
            //    //client.DefaultRequestHeaders.Add("Custom", "sample");

            //    User user = new User() { Name = userName };
            //    try
            //    {
            //        var fullPath = APP_PATH + "/api/user/CreateNewUser";
            //        var response = client.PostAsJsonAsync<User>(fullPath, user).Result;
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine(ex);
            //    }

            //}
            User user = new User();
            user.Name = userName;
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.PostAsJsonAsync<User>(APP_PATH + "/api/user/CreateNewUser", user);
                    if (response.Result.StatusCode == HttpStatusCode.Created)
                    {
                        var result = response.Result.Content.ReadAsStringAsync().Result;
                        user = JsonConvert.DeserializeObject<User>(result);
                    }
                    else
                        Logger.Log.Error($@"Server couldn't create new user. Wrong response status: /n {response.Status}");
                }
                catch (Exception e)
                {
                    Logger.Log.Error($@"Can't connect to server for new user creating. With exception: /n {e}");
                }
            }
        }

        public void test()
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync<string>("http://109.86.186.163:5555/api/values", "sdfgsdfsdf").Result;
                //System.Diagnostics.Debug.WriteLine(response.StatusCode.ToString());
            }
        }
    }
}
