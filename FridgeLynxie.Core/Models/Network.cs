using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Models
{
    public class Network
    {
        //public Network()
        //{
        //    FoodItem = new List<FoodItem>();
        //    User = new List<User>();
        //}

        public int NetworkId { get; set; }

        public int CreatorUserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        //public virtual ICollection<FoodItem> FoodItem { get; set; }

        //public virtual ICollection<User> User { get; set; }
    }
}
