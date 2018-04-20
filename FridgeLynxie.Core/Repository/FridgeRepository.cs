using FridgeLynxie.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Repository
{
    public class FridgeRepository
    {
        private static List<FoodUnit> foodUnits = new List<FoodUnit>();
        private static List<FoodState> foodStates = new List<FoodState>();
        private static List<FoodTag> foodTags = new List<FoodTag>();

        private static List<FoodItem> fridgeFoodItems = new List<FoodItem>();

        public string GetUnitsById(int unitId)
        {
            return foodUnits.FirstOrDefault(x => x.FoodUnitId == unitId).ShortenedName;
        }

        //{
        //new FoodItem("Chicken", 1, FoodDimension.Pcs, FoodType.Meat),
        //new FoodItem("Meat", 2, FoodDimension.Kilogramms, FoodType.Meat),
        //new FoodItem("Beer", 3, FoodDimension.Bottles, FoodType.Beer),
        //new FoodItem("Tomato", 0.8, FoodDimension.Kilogramms, FoodType.Vegetables),
        //new FoodItem("Milk", .5, FoodDimension.Litres, FoodType.Milky),
        //new FoodItem("Cucumber", .3, FoodDimension.Kilogramms, FoodType.Vegetables),
        //new FoodItem("Eggs", 15, FoodDimension.Pcs, FoodType.Unknown),
        //new FoodItem("Mirinda", 2, FoodDimension.Litres, FoodType.Soda),
        //new FoodItem("Soup", 2, FoodDimension.Litres, FoodType.Other),
        //new FoodItem("Борщ", 2, FoodDimension.Litres, FoodType.Other)
        //};

        public FoodItem GetFoodItemById(int id)
        {
            return null;
            //return fridgeFoodItems.FirstOrDefault(s => s.Id == id);
        }

        public List<FoodItem> GetAllFoodItemsByName(int id)
        {
            return null;
            //return fridgeFoodItems.Where(s => s.Id == id).ToList();
        }

        public List<FoodItem> GetAllFoodItems()
        {
            return null;
            //return fridgeFoodItems;
        }
    }
}
