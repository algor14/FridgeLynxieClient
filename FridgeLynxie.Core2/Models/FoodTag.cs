using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Models
{
    public class FoodTag
    {
        //public FoodTag()
        //{
        //    FoodItemFoodTag = new List<FoodItemFoodTag>();
        //}

        public int FoodTagId { get; set; }

        public string TagName { get; set; }

        //public virtual ICollection<FoodItemFoodTag> FoodItemFoodTag { get; set; }
    }
}
