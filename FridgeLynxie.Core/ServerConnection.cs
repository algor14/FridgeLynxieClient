//using Android.Widget;
using FridgeLynxie.Core.Models;
using FridgeLynxie.Core.Service;

//no checking if net the network with entered credentials already exists


using FridgeLynxie.Core.SQLite.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FridgeLynxie.Core
{
    public class ServerConnection
    {
        private const string APP_PATH = "";
        //private CurrentNetwork currentNetwork;      //local variant of model 'Network' from DB
        //private CurrentUser currentUser;            //local variant of model 'User' from DB
        private SQLiteDataService localDb;

        public void RegisterNewNetwork(string name, string pass)
        {
            try
            {
                localDb = new SQLiteDataService();
                using (var client = new HttpClient())
                {
                    var currentUser = localDb.GetCurrentUser();
                    Network net = new Network() { Name = name, Password = pass, CreatorUserId = currentUser.UserId };
                    var response = client.PostAsJsonAsync<Network>(APP_PATH + "/api/network/CreateNewNetwork", net);
                    Network deserializedNetwork;
                    if (response.Result.StatusCode == HttpStatusCode.Created)
                    {
                        deserializedNetwork = JsonConvert.DeserializeObject<Network>(response.Result.Content.ReadAsStringAsync().Result);
                        SQLiteDataService localBD = new SQLiteDataService();
                        localBD.AddCurrentNetwork(deserializedNetwork);
                        currentUser.NetworkId = deserializedNetwork.NetworkId;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Got wrong response StatusCode while creating new network: {response.Result.StatusCode}");
                        Log.Error($"Got wrong response StatusCode while creating new network: {response.Result.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Couldn't create new network: {e}");
                Log.Error(e, "Couldn't create new network");
            }
        }

        public bool JoinToNetwork(string name, string pass)
        {
            try
            {
                localDb = new SQLiteDataService();
                using (var client = new HttpClient())
                {
                    var currentUser = localDb.GetCurrentUser();
                    var tt = $"{APP_PATH}/api/network/GetNetwork?name={name}&pass={pass}";
                    var response = client.GetAsync($"{APP_PATH}/api/network/GetNetwork?name={name}&pass={pass}");
                    Network deserializedNetwork;
                    if (response.Result.StatusCode == HttpStatusCode.Found)
                    {
                        deserializedNetwork = JsonConvert.DeserializeObject<Network>(response.Result.Content.ReadAsStringAsync().Result);
                        SQLiteDataService localBD = new SQLiteDataService();
                        localBD.AddCurrentNetwork(deserializedNetwork);
                        currentUser.NetworkId = deserializedNetwork.NetworkId;
                        return true;
                    }
                    else if (response.Result.StatusCode == HttpStatusCode.NotFound)
                        return false;
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Got wrong response StatusCode while creating new network: {response.Result.StatusCode}");
                        Log.Error($"Got wrong response StatusCode while creating new network: {response.Result.StatusCode}");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Couldn't get a network: {e}");
                Log.Error(e, "Couldn't get a network");
                return false;
            }
        }

        public void RegisterNewUser(string userName)
        {
            User user = new User();
            user.Name = userName;
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.PostAsJsonAsync<User>(APP_PATH + "/api/user/CreateNewUser", user);
                    User deserializedUser;
                    if (response.Result.StatusCode == HttpStatusCode.Created)
                    {
                        deserializedUser = JsonConvert.DeserializeObject<User>(response.Result.Content.ReadAsStringAsync().Result);
                        //MessagingCenter.Send<ServerConnection, string>(this, "UpdateUserName_NetworkActivity", $"{deserializedUser.Name} {deserializedUser.UserId}");
                        SQLiteDataService localBD = new SQLiteDataService();
                        localBD.AddCurrentUser(deserializedUser);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Got wrong response StatusCode while creating new user: {response.Result.StatusCode}");
                        Log.Error($"Got wrong response StatusCode while creating new user: {response.Result.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Couldn't create new user: {e}");
                    Log.Error(e, "Couldn't create new user");
                }
            }
        }

        public void DeleteProfile()
        {
            SQLiteDataService localBD = new SQLiteDataService();
            //localBD.DeleteAllCurrentUsers();
            CurrentUser curUS = localBD.GetCurrentUser();
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.DeleteAsync($"{APP_PATH}/api/user/DeleteUser?id={curUS.UserId}");
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        localBD.DeleteAllCurrentUsers();
                        localBD.DeleteAllCurrentNetworks();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Got wrong response StatusCode while creating new network: {response.Result.StatusCode}");
                        Log.Error($"Got wrong response StatusCode while deleting current user: {response.Result.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Couldn't delete user: {e}");
                    Log.Error(e, "Couldn't delete user");
                }
            }
        }
    }
}
