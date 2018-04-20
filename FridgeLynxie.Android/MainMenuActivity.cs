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
using FridgeLynxie.Android.Utility;
using Serilog;
using FridgeLynxie.Core;

namespace FridgeLynxie.Android
{
    [Activity(Label = "MainMenuActivity", MainLauncher = true
        )]
    public class MainMenuActivity : Activity
    {
        private Button gotoFridgeButton;
        private Button HomeNetworkSettingsButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainMenuView);

            LocalLogger.Init();
            Log.Information("Logger was initialized successfully");
            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            gotoFridgeButton = FindViewById<Button>(Resource.Id.gotoFridgeButton);
            HomeNetworkSettingsButton = FindViewById<Button>(Resource.Id.HomeNetworkSettingsButton);
        }

        private void HandleEvents()
        {
            gotoFridgeButton.Click += GotoFridgeButtonOnClick;
            HomeNetworkSettingsButton.Click += HomeNetworkSettingsButtonOnClick;
        }

        private void HomeNetworkSettingsButtonOnClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NetworkActivity));
            StartActivity(intent);
        }

        private void GotoFridgeButtonOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(FrigeActivity));
            StartActivity(intent);
        }
    }
}