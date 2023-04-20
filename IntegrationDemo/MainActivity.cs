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

       
        private void SendBroadCastOnClick(object sender, EventArgs eventArgs)
        {
            var prepasspackageId = "com.prepass.motion"; //real use 
            //var prepasspackageId = "com.prepass.integrationreceiver"; //integration test

            Intent foregroundEroad = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);
            foregroundEroad.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            
            PendingIntent pendingForegroundEroad = PendingIntent.GetActivity(Application.Context, 0, foregroundEroad, PendingIntentFlags.OneShot | PendingIntentFlags.Immutable);

            var launchPrepass = new Intent("com.erode.VEHICLE_SELECTED");
            if (launchPrepass != null)
            {
                launchPrepass.SetPackage(prepasspackageId);
                launchPrepass.PutExtra("VIN", "send.vin.please");
                launchPrepass.PutExtra("PendingIntent", pendingForegroundEroad);

                SendBroadcast(launchPrepass);
            }
            else
            {
                //send them to the store? or just assume no integration?
            }
        }

     



        private void SendPendingIntentOnClick(object sender, EventArgs eventArgs)
        {
            Intent foregroundEroad = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);
            foregroundEroad.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
           

            PendingIntent pendingForegroundEroad = PendingIntent.GetActivity(Application.Context, 0, foregroundEroad, PendingIntentFlags.OneShot | PendingIntentFlags.Immutable);
            var launchPrepass = Application.Context.PackageManager.GetLaunchIntentForPackage("com.prepass.motion");
            if (launchPrepass != null)
            {
                launchPrepass.SetFlags(ActivityFlags.SingleTop | ActivityFlags.NewTask);
                launchPrepass.PutExtra("IntegrationPartner", 3); //TSP Integration Partner
                launchPrepass.PutExtra("VIN", "4V4NC9TJ96N434360");
                launchPrepass.PutExtra("PendingIntent", pendingForegroundEroad);

                Application.Context.StartActivity(launchPrepass);
            }
            else
            {
                //send them to the store?
            }
          
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
