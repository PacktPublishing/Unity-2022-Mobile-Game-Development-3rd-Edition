using UnityEngine;
using NotificationSamples; /* GameNotificationManager */
using System; /* DateTime */
using UnityEngine.SocialPlatforms.Impl;

public class NotificationsController : MonoBehaviour
{
    private GameNotificationsManager notificationsManager;

    private static bool addedReminder = false;

    // Start is called before the first frame update
    private void Start()
    {
        /* Get access to the notifications manager */
        notificationsManager =
        GetComponent<GameNotificationsManager>();


        /* Create a channel to use (required for Android) */
        var channel = new GameNotificationChannel("channel0", 
                                                "Default Channel", 
                                         "Generic Notifications");

        /* Initialize the manager so it can be used. */
        notificationsManager.Initialize(channel);

        /* Check if the notification hasn't been added yet */
        if (!addedReminder)
        {

            /* Create sample notification to happen in 5 seconds */
            var notifText = "Come back and try to beat your score!!";

            // After 5 seconds
            var notifTime = DateTime.Now.AddSeconds(5);

            // After 1 day
            //notifTime = DateTime.Now.AddDays(1);
            ShowNotification("Endless Runner", notifText, notifTime);

            // Example of cancelling a notification
            var id = ShowNotification("Test", "Should Not Happen", notifTime);

            if(id.HasValue)
            {
                notificationsManager.CancelNotification(id.Value);
            }

            /* Cannot be added again until the user quits game */
            addedReminder = true;
        }

    }

    public int? ShowNotification(string title, string body,
                                 DateTime deliveryTime)
    {
        IGameNotification notification =
        notificationsManager.CreateNotification();

        if (notification != null)
        {
            notification.Title = title; 
            notification.Body = body; 
            notification.DeliveryTime = deliveryTime;
            notification.SmallIcon = "icon_0";
            notification.LargeIcon = "icon_1";


            var pendingNotif = notificationsManager.ScheduleNotification(notification);

            return pendingNotif.Notification.Id;
        }

        return null;
    }


}

