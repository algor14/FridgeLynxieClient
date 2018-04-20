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
using FridgeLynxie.Core.Service;
using FridgeLynxie.Android.Utility;

namespace FridgeLynxie.Android
{
    [Activity(Label = "FoodItemDetailsActivity")]
    public class FoodItemDetailsActivity : Activity
    {
        private Button takePictureButton;
        private Button cancelButton;
        private Button saveButton;
        private DatePicker expDateDatePicker;
        private TextView expDateTextView;
        private TextView nameTextView;
        private ImageView iconImageView;
        private FoodItem foodItem;
        private FridgeFoodItemsDataService fridgeFoodItemsDataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FoodItemDetailsView);

            fridgeFoodItemsDataService = new FridgeFoodItemsDataService();

            foodItem = fridgeFoodItemsDataService.GetFoodItemById(Intent.Extras.GetInt("FoodItemId"));

            FindViews();
            BindData();
            HandleEvents();
        }

        private void HandleEvents()
        {
            cancelButton.Click += CancelButtonOnClick;
        }

        private void CancelButtonOnClick(object sender, EventArgs eventArgs)
        {
            this.Finish();
        }

        private void BindData()
        {
            iconImageView.SetImageDrawable(ImageManager.Get(this, foodItem.ImagePath));
            nameTextView.Text = foodItem.Name;
            expDateTextView.Text = foodItem.ExpirationDate.ToShortDateString();
        }

        private void FindViews()
        {
            takePictureButton = FindViewById<Button>(Resource.Id.takePictureButtonDetailsView);
            cancelButton = FindViewById<Button>(Resource.Id.cancelButtonDetailsView);
            saveButton = FindViewById<Button>(Resource.Id.saveButtonDetailsView);
            //expDateDatePicker = FindViewById<DatePicker>(Resource.Id.expDateDatePickerDetailsView);
            iconImageView = FindViewById<ImageView>(Resource.Id.foodItemIconImageViewDetailsView);
            expDateTextView = FindViewById<TextView>(Resource.Id.expDateTextViewDetailsView);
            nameTextView = FindViewById<TextView>(Resource.Id.foodItemNameTextViewDetailsView);
        }
    }
}