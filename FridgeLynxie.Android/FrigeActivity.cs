using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FridgeLynxie.Core.Service;
using FridgeLynxie.Android.Adapters;
using FridgeLynxie.Core.Models;

namespace FridgeLynxie.Android
{
    [Activity(Label = "FrigeActivity")]
    public class FrigeActivity : Activity
    {
        private ListView fridgeListView;
        private FridgeFoodItemsDataService fridgeFoodItemsDataService;
        private List<FoodItem> allFridgeFoodItems;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FridgeView);
            fridgeListView = new ListView(this);
            fridgeListView = FindViewById<ListView>(Resource.Id.FridgeListView);

            fridgeFoodItemsDataService = new FridgeFoodItemsDataService();
            allFridgeFoodItems = fridgeFoodItemsDataService.GetAllFoodItems();
            fridgeListView.Adapter = new FridgeFoodItemListAdapter(this, allFridgeFoodItems);
            HandleEvents();
        }

        private void HandleEvents()
        {
            fridgeListView.ItemLongClick += FridgeListViewOnItemLongClick;
        }

        private void FridgeListViewOnItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var foodItem = allFridgeFoodItems[e.Position];

            var intent = new Intent(this, typeof(FoodItemDetailsActivity));
            intent.PutExtra("FoodItemId", foodItem.FoodItemId);
            //StartActivityForResult(intent, 10);
            StartActivity(intent);
        }
    }
}