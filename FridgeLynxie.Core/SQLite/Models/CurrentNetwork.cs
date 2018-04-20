using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.SQLite.Models
{
    public class CurrentNetwork
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // its a local id

        public int NetworkId { get; set; } // this id math with user id in global DB
        public int CreatorUserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        //public virtual ICollection<FoodItem> FoodItem { get; set; }

        //public virtual ICollection<User> User { get; set; }
    }
}
