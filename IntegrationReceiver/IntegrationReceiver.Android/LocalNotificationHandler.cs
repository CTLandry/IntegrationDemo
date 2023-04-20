using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;


namespace IntegrationReceiver.Droid
{
    public class LocalNotificationHandler
    {
        private const string alertsChannelId = "alertsId";
        private const string alertsChannelName = "alerts";
        private const string alertsChannelDescription = "I send alerts";
        private bool alertsChannelCreated = false;
        NotificationManager notificationManager;


        public void NotifyStuffHappened(Context context)
        {
            if (notificationManager == null)
            {
                notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
            }
            
            if (!alertsChannelCreated)
            {
                CreateAlertsNotificationChannel(context);
            }

            // Instantiate the builder and set notification elements:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, alertsChannelId)
                .SetContentTitle("Sample Notification")
                .SetContentText("Going into drive mode")
                .SetSmallIcon(Resource.Id.icon);


            // Build the notification:
            Notification notification = builder.Build();

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }

        private void CreateAlertsNotificationChannel(Context context)
        {
           
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var channelNameJava = new Java.Lang.String(alertsChannelName);
            var channel = new NotificationChannel(alertsChannelId, channelNameJava, NotificationImportance.High)
            {
                Description = alertsChannelDescription
            };

            //custom tone here or default tone for now??

            channel.EnableVibration(true);
            channel.Importance = NotificationImportance.High;

            notificationManager.CreateNotificationChannel(channel);

            alertsChannelCreated = true;
        }
    }
}