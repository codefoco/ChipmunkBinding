using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Microsoft.Xna.Framework;
using Android.Views;
using Android.Content.PM;

namespace ChipmunkDemo.Android
{
    [Activity(Label = "@string/app_name",
              Theme = "@style/AppTheme",
              MainLauncher = true,
              AlwaysRetainTaskState = true,
              ConfigurationChanges = ConfigChanges.Orientation,
              ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AndroidGameActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            var game = new ChipmunkDemoGame();
            SetContentView((View)game.Services.GetService(typeof(View)));
            game.Run();
        }
    }
}