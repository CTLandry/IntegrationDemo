using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Android.Content;



namespace IntegrationDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //Android.Widget.Button sendBroadCast = FindViewById<Android.Widget.Button>(Resource.Id.broadcastbutton);
            //sendBroadCast.Click += SendBroadCastOnClick;

            Android.Widget.Button sendPendingIntent = FindViewById<Android.Widget.Button>(Resource.Id.pendingintentbutton);
            sendPendingIntent.Click += SendPendingIntentOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        /// <summary>
        /// Launch intent with pending intent for pushing the calling app back to the foreground once
        /// PrePass has navigated to driving mode and started all necessary services for the integration.
        /// Intent should be sent upon each vehicle selection so that PrePass can use the applicable vin
        /// </summary>
        private void SendPendingIntentOnClick(object sender, EventArgs eventArgs)
        {
            ///Create an intent for foregrounding the integration partner's application
            Intent foregroundCallingApp = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);
            foregroundCallingApp.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
           
            ///Bundle the launch intent for the integration partner into a pending intent
            ///Pending intent should be Immutable for security purposes
            PendingIntent pi = PendingIntent.GetActivity(Application.Context, 0, foregroundCallingApp, PendingIntentFlags.OneShot | PendingIntentFlags.Immutable);

            ///Create a launch intent for PrePass
            /// <param name="IntegrationPartner">The integration partner ID provided by PrePass</param>
            /// <param name="VIN">The selected vehicle's vin</param>
            /// <param name="PendingIntent">A launch intent for the calling application</param>
            var launchPrepass = Application.Context.PackageManager.GetLaunchIntentForPackage("com.prepass.motion");
            if (launchPrepass != null)
            {
                launchPrepass.SetFlags(ActivityFlags.SingleTop | ActivityFlags.NewTask);
                launchPrepass.PutExtra("IntegrationPartner", 3); //PrePass provides
                launchPrepass.PutExtra("VIN", "selectedVehiclesVin"); 
                launchPrepass.PutExtra("PendingIntent", pi);

                Application.Context.StartActivity(launchPrepass);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
