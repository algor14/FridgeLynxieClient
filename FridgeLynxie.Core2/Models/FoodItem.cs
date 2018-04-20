using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Models
{
    public  class FoodItem
    {
        //public FoodItem()
        //{
        //    FoodItemFoodTag = new List<FoodItemFoodTag>();
        //}

        public int FoodItemId { get; set; }

        public int FoodItemSecondaryId { get; set; }

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime PurchasedDate { get; set; }

        public int ApproximateStorageTime { get; set; }

        public int UnitId { get; set; }

        public float Amount { get; set; }

        public int IsFavorite { get; set; }

        public int FoodStateId { get; set; }

        public int NetworkId { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        //public virtual FoodUnit FoodUnit { get; set; }

        //public virtual FoodState FoodState { get; set; }

        //public virtual Network Network { get; set; }

        //public virtual ICollection<FoodItemFoodTag> FoodItemFoodTag { get; set; }
    }
}
