using FridgeLynxie.Core.Models;
using FridgeLynxie.Core2.SQLite.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core2.Repository
{
    public class SQLiteRepository
    {
        private SQLiteConnection db;
        public SQLiteRepository()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            db = new SQLiteConnection(databasePath);
            db.CreateTable<CurrentUser>();
        }

        public void AddCurrentUser(User user)
        {
            var s = db.Insert(new CurrentUser()
            {
                Name = user.Name,
                NetworkId = user.NetworkId,
                UserId = user.UserId
            });
        }

        //public void UpdateCurrentUser(User user)
        //{
        //    var s = db.Update(new CurrentUser()
        //    {
        //        Id = 1,
        //        Name = user.Name,
        //        NetworkId = user.NetworkId,
        //        UserId = user.UserId
        //    });

        //}

    }
}
