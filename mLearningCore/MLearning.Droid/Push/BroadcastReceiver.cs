using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using ByteSmith.WindowsAzure.Messaging;
using Cirrious.CrossCore;
using Core.Push;
using Gcm.Client;
using MLearning.Droid.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace KunFoodMozo.Droid.Push
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class MyBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = new string[] { MLearning.Core.Configuration.Constants.SenderID };

        public const string TAG = "MyBroadcastReceiver-GCM";
    }

    [Service] //Must use the service tag
    public class GcmService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        private NotificationHub Hub { get; set; }

        public GcmService()
            : base(MLearning.Core.Configuration.Constants.SenderID)
        {
            Log.Info(MyBroadcastReceiver.TAG, "GcmService() constructor");
        }

        protected override void OnError(Context context, string errorId)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info(MyBroadcastReceiver.TAG, "GCM Message Received!");

            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }

            //Store the message
            var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            var edit = prefs.Edit();
            edit.PutString("last_msg", msg.ToString());
            edit.Commit();

            //Handle Notifications types
            string tableName = intent.Extras.GetString("refresh");
            if (!string.IsNullOrEmpty(tableName))
            {

                NotificationHandler.HandleNotification(tableName);
            //    createNotification(username, commentText);
               
                
                return;
            }
      

            createNotification("Unknown message details", msg.ToString());
        }

        protected async override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
            RegistrationID = registrationId;

            // createNotification("GcmService-GCM Registered...", "The device has been Registered, Tap to View!");

            Hub = new NotificationHub(MLearning.Core.Configuration.Constants.NotificationHubPath, MLearning.Core.Configuration.Constants.ConnectionString);
            try
            {
                await Hub.UnregisterAllAsync(registrationId);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                //Debugger.Break();
            }

            var tags = new List<string>() { "mlearningservice" }; // create tags if you want

            try
            {
                var hubRegistration = await Hub.RegisterNativeAsync(registrationId, tags);
             
            }
            catch (Exception ex)
            {
                //  Debug.WriteLine(ex.Message); 
//                Debugger.Break();
            }
        }

        protected async override void OnUnRegistered(Context context, string registrationId)
        {
            Hub = new NotificationHub(MLearning.Core.Configuration.Constants.NotificationHubPath, MLearning.Core.Configuration.Constants.ConnectionString);
            try
            {
                await Hub.UnregisterAllAsync(registrationId);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
              //  Debugger.Break();
            }
        }

        void createNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            //Create an intent to show ui
            string usr;
            int userid;

            Intent    uiIntent = new Intent(this, typeof(MainView));
           


            //Create the notification
            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);

            //Auto cancel will remove the notification once the user touches it
            notification.Flags = NotificationFlags.AutoCancel;

            //Set the notification info
            //we use the pending intent, passing our ui intent over which will get called
            //when the notification is tapped.
            notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));

            //Show the notification
            notificationManager.Notify(1, notification);
        }

    }



  
}