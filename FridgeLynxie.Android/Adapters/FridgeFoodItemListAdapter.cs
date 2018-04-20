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
using FridgeLynxie.Core.Models;
using FridgeLynxie.Android.Utility;
using FridgeLynxie.Core.Service;

namespace FridgeLynxie.Android.Adapters
{
    public class FridgeFoodItemListAdapter : BaseAdapter<FoodItem>
    {
        private Activity context;
        private List<FoodItem> items;
        private FridgeFoodItemsDataService fridgeFoodItemsDataService;

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public FridgeFoodItemListAdapter(Activity context, List<FoodItem> items)
        {
            this.items = items;
            this.context = context;
            fridgeFoodItemsDataService = new FridgeFoodItemsDataService();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override FoodItem this[int position]
        {
            get { return items[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.FridgeFoodItemRowView, null);
            }
            convertView.FindViewById<TextView>(Resource.Id.foodItemNameTextView).Text = item.Name;
            convertView.FindViewById<ImageView>(Resource.Id.foodItemIconImageView).SetImageDrawable(ImageManager.Get(parent.Context, item.ImagePath));
            convertView.FindViewById<TextView>(Resource.Id.foodItemExpDateTextView).Text = item.ExpirationDate.ToShortDateString();
            convertView.FindViewById<TextView>(Resource.Id.foodItemQuantityTextView).Text = $"{item.Amount} {fridgeFoodItemsDataService.GetUnitsById(item.UnitId)}";
            //convertView.FindViewById<TextView>(Resource.Id.foodItemTypeTextView).Text = FoodTypeContractions.GetContractionFor(item.Type);
            //System.Diagnostics.Debug.WriteLine("jkfghdfkljhgldfg;ldg");
            return convertView;
        }


    }
}