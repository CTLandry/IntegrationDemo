using Android.App;
using Android.Content;
using Android.Media;
using Android.Mtp;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IntegrationReceiver.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "com.erode.VEHICLE_SELECTED" })]
    public class TestBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // Do stuff here.

            String vin = intent.GetStringExtra("VIN");

            //var notification = BuildNotification(context);
            //var notificationManager = NotificationManagerCompat.From(context);
            //notificationManager.Notify(8008135, notification);


        }

        //public Notification BuildNotification(Context context)
        //{
        //    var launchPrepass = context.PackageManager.GetLaunchIntentForPackage("com.prepass.motion");
        //    PendingIntent fullScreenPendingIntent = PendingIntent.GetActivity(context, 0,
        //    launchPrepass, PendingIntentFlags.UpdateCurrent);

        //    //var notificationBuilder = new NotificationCompat.Builder(context, AppConstants.LocalNotifications.AlertsChannelId);
        //    notificationBuilder
        //        .SetSmallIcon(Resource.Mipmap.ic_stat_logo_prepass_p)
        //        .SetContentTitle("Prepass driving mode")
        //        .SetContentText("Click to open Prepass and start driving mode")
        //        .SetPriority(NotificationCompat.PriorityHigh)
        //        .SetCategory(NotificationCompat.CategoryCall)
        //        // Use a full-screen intent only for the highest-priority alerts where you
        //        // have an associated activity that you would like to launch after the user
        //        // interacts with the notification. Also, if your app targets Android 10
        //        // or higher, you need to request the USE_FULL_SCREEN_INTENT permission in
        //        // order for the platform to invoke this notification.
        //        .SetFullScreenIntent(fullScreenPendingIntent, true);

        //    return notificationBuilder.Build();
        //}


    }
}