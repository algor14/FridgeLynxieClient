using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Models
{
    public class FoodUnit
    {
        //public FoodUnit()
        //{
        //    FoodItem = new List<FoodItem>();
        //}

        public int FoodUnitId { get; set; }

        public string FullName { get; set; }

        public string ShortenedName { get; set; }

        //public virtual ICollection<FoodItem> FoodItem { get; set; }
    }
}
