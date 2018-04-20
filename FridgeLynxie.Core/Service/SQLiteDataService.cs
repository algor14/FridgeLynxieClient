using FridgeLynxie.Core.Models;
using FridgeLynxie.Core.Repository;
using FridgeLynxie.Core.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Service
{
    public class SQLiteDataService
    {
        private static SQLiteRepository sqliteRepository = new SQLiteRepository();

        public void AddCurrentUser(User user)
        {
            CurrentUser curUs = new CurrentUser()
            {
                Name = user.Name,
                NetworkId = user.NetworkId,
                UserId = user.UserId
            };
            sqliteRepository.SaveCurrentUser(curUs);
        }

        public CurrentUser GetCurrentUser()
        {
            return sqliteRepository.GetCurrentUser(1);
        }

        internal void AddCurrentNetwork(Network net)
        {
            CurrentNetwork curNet = new CurrentNetwork()
            {
                Name = net.Name,
                NetworkId = net.NetworkId,
                Password = net.Password,
                CreatorUserId = net.CreatorUserId
            };
            sqliteRepository.SaveCurrentNetwork(curNet);
        }

        internal void DeleteCurrentUser(CurrentUser curUS)
        {
            sqliteRepository.DeleteCurrentUser(curUS);
        }

        internal void DeleteAllCurrentUsers()
        {
            sqliteRepository.DeleteAllCurrentUsers();
        }

        public CurrentNetwork GetCurrentNetwork()
        {
            return sqliteRepository.GetCurrentNetwork(1);
        }

        internal void DeleteAllCurrentNetworks()
        {
            sqliteRepository.DeleteAllCurrentNetworks();
        }
    }
}
