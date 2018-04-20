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
using FridgeLynxie.Core;
using FridgeLynxie.Core.Models;
using FridgeLynxie.Core.Service;
using FridgeLynxie.Core.SQLite.Models;

namespace FridgeLynxie.Android
{
    [Activity(Label = "NetworkActivity")]
    public class NetworkActivity : Activity
    {
        private Button CreateNetworkButton;
        private Button JoinNetworkButton;
        private Button BackButton;
        private Button DeleteProfileButton;
        private Button CreateNewUserButton;
        private EditText NetworkNameEditText;
        private EditText NetworkPasswordEditText;
        private EditText UserNameEditText;
        private TextView CurrentUserInfoText1;
        private TextView CurrentNetworkInfoText1;
        private TextView CurrentNetworkInfoTextName;
        private TextView CurrentNetworkInfoTextPassword;

        private ServerConnection serverConnection;
        private SQLiteDataService localDb;
        private CurrentUser currentUser;
        private CurrentNetwork currentNetwork;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.NetworkView);

            FindViews();
            HandleEvents();
            serverConnection = new ServerConnection();
            localDb = new SQLiteDataService();


            SetupCurrentUserElementGroup();
            SetupCurrentNetworkElementGroup();

            //Xamarin.Forms.MessagingCenter.Subscribe<ServerConnection, string>(this, "UpdateUserName_NetworkActivity", (sender, user) => {
            //    CurrentUserText.Text = user;
            //});

        }

        private void SetupCurrentUserElementGroup()
        {
            currentUser = localDb.GetCurrentUser();
            if (currentUser != null)
            {
                CurrentUserInfoText1.Text = $"{currentUser.Name} - {currentUser.UserId}";
                CurrentNetworkInfoText1.Text = "Join to existing network or create new one";
                UserNameEditText.Visibility = ViewStates.Invisible;
                CreateNewUserButton.Visibility = ViewStates.Invisible;
                DeleteProfileButton.Visibility = ViewStates.Visible;
                NetworkNameEditText.Enabled = true;
                NetworkPasswordEditText.Enabled = true;
            }
            else
            {
                CurrentUserInfoText1.Text = "User was not found. Please create a new one.";
                CurrentNetworkInfoText1.Text = "Create new user before cteating or join to the network";
                UserNameEditText.Visibility = ViewStates.Visible;
                CreateNewUserButton.Visibility = ViewStates.Visible;
                DeleteProfileButton.Visibility = ViewStates.Invisible;
                NetworkNameEditText.Enabled = false;
                NetworkPasswordEditText.Enabled = false;
            }
            DeleteProfileButton.Visibility = ViewStates.Visible;
        }

        private void SetupCurrentNetworkElementGroup()
        {
            currentNetwork = localDb.GetCurrentNetwork();
            if (currentNetwork != null)
            {
                CurrentNetworkInfoText1.Text = $"{currentNetwork.Name} - {currentNetwork.Password}; NetworkId: {currentNetwork.NetworkId}";
                CreateNetworkButton.Visibility = ViewStates.Invisible;
                JoinNetworkButton.Visibility = ViewStates.Invisible;
                NetworkNameEditText.Visibility = ViewStates.Invisible;
                NetworkPasswordEditText.Visibility = ViewStates.Invisible;
                CurrentNetworkInfoTextName.Visibility = ViewStates.Invisible;
                CurrentNetworkInfoTextPassword.Visibility = ViewStates.Invisible;
            }
            else
            {
                CreateNetworkButton.Visibility = ViewStates.Visible;
                JoinNetworkButton.Visibility = ViewStates.Visible;
                NetworkNameEditText.Visibility = ViewStates.Visible;
                NetworkPasswordEditText.Visibility = ViewStates.Visible;
                CurrentNetworkInfoTextName.Visibility = ViewStates.Visible;
                CurrentNetworkInfoTextPassword.Visibility = ViewStates.Visible;
            }
        }

        private void FindViews()
        {
            CreateNetworkButton = FindViewById<Button>(Resource.Id.CreateNetworkButton);
            JoinNetworkButton = FindViewById<Button>(Resource.Id.JoinNetworkButton);
            BackButton = FindViewById<Button>(Resource.Id.BackButton);
            DeleteProfileButton = FindViewById<Button>(Resource.Id.DeleteProfileButton);
            CreateNewUserButton = FindViewById<Button>(Resource.Id.CreateNewUserButton);
            NetworkNameEditText = FindViewById<EditText>(Resource.Id.NetworkNameEditText);
            NetworkPasswordEditText = FindViewById<EditText>(Resource.Id.NetworkPasswordEditText);
            UserNameEditText = FindViewById<EditText>(Resource.Id.UserNameEditText);
            CurrentUserInfoText1 = FindViewById<TextView>(Resource.Id.CurrentUserInfoText1);
            CurrentNetworkInfoText1 = FindViewById<TextView>(Resource.Id.CurrentNetworkInfoText1);
            CurrentNetworkInfoTextName = FindViewById<TextView>(Resource.Id.CurrentNetworkInfoTextName);
            CurrentNetworkInfoTextPassword = FindViewById<TextView>(Resource.Id.CurrentNetworkInfoTextPassword);
        }

        private void HandleEvents()
        {
            //JoinButton.Click += JoinButtonOnClick;
            CreateNetworkButton.Click += CreateNetworkButtonButtonOnClick;
            JoinNetworkButton.Click += JoinNetworkButtonOnClick;
            //BackButton.Click += BackButtonOnClick;
            CreateNewUserButton.Click += CreateNewUserButtonOnClick;
            DeleteProfileButton.Click += DeleteProfileButtonOnClick;
        }

        private void CreateNewUserButtonOnClick(object sender, EventArgs e)
        {
            if (ValidateString(UserNameEditText.Text))
            {
                serverConnection.RegisterNewUser(UserNameEditText.Text);
                currentUser = localDb.GetCurrentUser();
                if (currentUser != null)
                {
                    CurrentUserInfoText1.Text = $"{currentUser.Name} - {currentUser.UserId}";
                }
                else
                {
                    CurrentUserInfoText1.Text = "You didn't create user yet";
                    CurrentNetworkInfoText1.Text = "Create new user before cteating or join to the network";
                }
                UserNameEditText.Text = "";
                SetupCurrentUserElementGroup();
            }
            else
                CurrentUserInfoText1.Text = "User name can not be empty";
        }

        private void BackButtonOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void JoinNetworkButtonOnClick(object sender, EventArgs e)
        {
            if (ValidateString(NetworkNameEditText.Text) && ValidateString(NetworkPasswordEditText.Text))
            {
                if (serverConnection.JoinToNetwork(NetworkNameEditText.Text, NetworkPasswordEditText.Text))
                {
                    currentNetwork = localDb.GetCurrentNetwork();
                    if (currentNetwork != null)
                        CurrentNetworkInfoText1.Text = $"{currentNetwork.Name} - {currentNetwork.Password}; NetworkId: {currentNetwork.NetworkId}; Network creator: {currentNetwork.CreatorUserId}";
                    else
                        CurrentNetworkInfoText1.Text = "Some error has been occured. Please delete your profile and try to resync your account";
                }
                else
                    CurrentNetworkInfoText1.Text = "Could'n find a network witn such credentials. Please try with another ones";
                NetworkNameEditText.Text = "";
                NetworkPasswordEditText.Text = "";
                SetupCurrentNetworkElementGroup();
            }
            else
                CurrentNetworkInfoText1.Text = "Network name and password can not be empty";
        }

        private void CreateNetworkButtonButtonOnClick(object sender, EventArgs e)
        {
            if (ValidateString(NetworkNameEditText.Text) && ValidateString(NetworkPasswordEditText.Text))
            {
                serverConnection.RegisterNewNetwork(NetworkNameEditText.Text, NetworkPasswordEditText.Text);
                currentNetwork = localDb.GetCurrentNetwork();
                if (currentNetwork != null)
                    CurrentNetworkInfoText1.Text = $"{currentNetwork.Name} - {currentNetwork.Password}; NetworkId: {currentNetwork.NetworkId}";                  
                else
                    CurrentNetworkInfoText1.Text = "Some error has been occured. Please try again with different credentials";
                NetworkNameEditText.Text = "";
                NetworkPasswordEditText.Text = "";
                SetupCurrentNetworkElementGroup();
            }
            else
                CurrentNetworkInfoText1.Text = "Network name and password can not be empty";
        }

        private void JoinButtonOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteProfileButtonOnClick(object sender, EventArgs e)
        {
            serverConnection = new ServerConnection();
            serverConnection.DeleteProfile();
            SetupCurrentUserElementGroup();
            SetupCurrentNetworkElementGroup();
        }

        private bool ValidateString(string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return false;
            return true;
        }
    }
}