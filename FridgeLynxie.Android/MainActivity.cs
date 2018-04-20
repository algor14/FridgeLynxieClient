using Android.App;
using Android.Widget;
using Android.OS;

namespace FridgeLynxie.Android
{
    [Activity(Label = "FridgeLynxie.Android")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

