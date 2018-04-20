using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Models
{
    public class FoodItemFoodTag
    {
        public int Id { get; set; }

        public int FoodTagId { get; set; }

        public int FoodItemId { get; set; }

        //public virtual FoodItem FoodItem { get; set; }

        //public virtual FoodTag FoodTag { get; set; }
    }
}
