using FridgeLynxie.Core.Models;
using FridgeLynxie.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.Service
{
    public class FridgeFoodItemsDataService
    {
        private static FridgeRepository fridgeRepository = new FridgeRepository();

        public FoodItem GetFoodItemById(int id)
        {
            FoodItem foodItem;
            try
            {
                foodItem = fridgeRepository.GetFoodItemById(id);
            }
            catch (Exception e)
            {
                return null;
            }
            return foodItem;
        }

        public List<FoodItem> GetAllFoodItemsByName(int id)
        {
            return fridgeRepository.GetAllFoodItemsByName(id);
        }

        public List<FoodItem> GetAllFoodItems()
        {
            return fridgeRepository.GetAllFoodItems();
        }

        public object GetUnitsById(int unitId)
        {
            return fridgeRepository.GetUnitsById(unitId);
        }
    }
}

