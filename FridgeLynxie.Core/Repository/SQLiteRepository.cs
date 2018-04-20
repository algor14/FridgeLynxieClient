using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using FridgeLynxie.Core.SQLite.Models;
using FridgeLynxie.Core.SQLite;
using FridgeLynxie.Core.Models;

namespace FridgeLynxie.Core.Repository
{
    public class SQLiteRepository
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<CurrentUser> CurrentUsersLDB { get; set; }
        public ObservableCollection<CurrentNetwork> CurrentNetworksLDB { get; set; }


        public SQLiteRepository()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<CurrentUser>();
            this.CurrentUsersLDB = new ObservableCollection<CurrentUser>(database.Table<CurrentUser>());
            database.CreateTable<CurrentNetwork>();
            this.CurrentNetworksLDB = new ObservableCollection<CurrentNetwork>(database.Table<CurrentNetwork>());
        }

        // Use LINQ to query and filter data
        public IEnumerable<CurrentUser> GetFilteredCurrentUsers(int id)
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var query = from curUs in database.Table<CurrentUser>()
                            where curUs.Id == id
                            select curUs;
                return query.AsEnumerable();
            }
        }

        // Use SQL queries against data
        public IEnumerable<CurrentUser> GetFilteredCurrentUsers()
        {
            lock (collisionLock)
            {
                return database.Query<CurrentUser>("SELECT * FROM Item WHERE Country = 'Italy'").AsEnumerable();
            }
        }

        public CurrentUser GetCurrentUser(int id)
        {
            lock (collisionLock)
            {
                return database.Table<CurrentUser>().FirstOrDefault(curUs => curUs.Id == id);
            }
        }

        public int SaveCurrentUser(CurrentUser currentUserInstance)
        {
            lock (collisionLock)
            {
                if (currentUserInstance.Id != 0)
                {
                    database.Update(currentUserInstance);
                    return currentUserInstance.Id;
                }
                else
                {
                    database.Insert(currentUserInstance);
                    return currentUserInstance.Id;
                }
            }
        }

        public void SaveAllCurrentUsers()
        {
            lock (collisionLock)
            {
                foreach (var customerInstance in this.CurrentUsersLDB)
                {
                    if (customerInstance.Id != 0)
                    {
                        database.Update(customerInstance);
                    }
                    else
                    {
                        database.Insert(customerInstance);
                    }
                }
            }
        }

        public int DeleteCurrentUser(CurrentUser currentUserInstance)
        {
            var id = currentUserInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<CurrentUser>(id);
                }
            }
            this.CurrentUsersLDB.Remove(currentUserInstance);
            return id;
        }

        public void DeleteAllCurrentUsers()
        {
            lock (collisionLock)
            {
                database.DropTable<CurrentUser>();
                database.CreateTable<CurrentUser>();
            }
            this.CurrentUsersLDB = null;
            this.CurrentUsersLDB = new ObservableCollection<CurrentUser>(database.Table<CurrentUser>());
        }

        //****************************************************************//
        //**                    CURRENT NETWORK PART                    **//
        //****************************************************************//

        public CurrentNetwork GetCurrentNetwork(int id)
        {
            lock (collisionLock)
            {
                return database.Table<CurrentNetwork>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveCurrentNetwork(CurrentNetwork curNet)
        {
            lock (collisionLock)
            {
                if (curNet.Id != 0)
                {
                    database.Update(curNet);
                    return curNet.Id;
                }
                else
                {
                    database.Insert(curNet);
                    return curNet.Id;
                }
            }
        }

        public void DeleteAllCurrentNetworks()
        {
            lock (collisionLock)
            {
                database.DropTable<CurrentNetwork>();
                database.CreateTable<CurrentNetwork>();
            }
            this.CurrentNetworksLDB = null;
            this.CurrentNetworksLDB = new ObservableCollection<CurrentNetwork>(database.Table<CurrentNetwork>());
        }
    }
}
