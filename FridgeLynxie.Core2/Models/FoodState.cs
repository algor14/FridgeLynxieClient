using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Models
{
    public class FoodState
    {
        //public FoodState()
        //{
        //    FoodItem = new List<FoodItem>();
        //}

        public int FoodStateId { get; set; }

        public string StateName { get; set; }

        //public virtual ICollection<FoodItem> FoodItem { get; set; }
    }
}
