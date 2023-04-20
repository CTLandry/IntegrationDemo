using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using System.Threading;
using static Xamarin.Essentials.Platform;

namespace IntegrationReceiver.Droid
{
    [Activity(Label = "IntegrationReceiver", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            LaunchIntegration(Intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);

            LaunchIntegration(intent);

            base.OnNewIntent(intent);

        }

       
        private void LaunchIntegration(Android.Content.Intent intent)
        {
            var integrationPartner = intent.Extras?.GetInt("IntegrationPartner");
            if (integrationPartner == null) { return; }

            var VIN = intent.Extras.GetString("VIN");
            PendingIntent pendingSendUserBackToCallingApp = (PendingIntent)intent.Extras.Get("PendingIntent");

            //create some logic for handling integration partners
            //a base class at least for authing and sending to drive mode

            Thread.Sleep(5000);

            pendingSendUserBackToCallingApp.Send();

            System.Diagnostics.Debug.WriteLine("Notification Received");
        }
    }
}