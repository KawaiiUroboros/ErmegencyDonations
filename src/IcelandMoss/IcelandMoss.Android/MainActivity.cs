using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.FirebasePushNotification;
using Android.Content;
using Android;

namespace IcelandMoss.Droid
{
    [Activity(Label = "ErmegeDon", Icon = "@drawable/icon_main", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] Permissions =
           {
                Manifest.Permission.WriteExternalStorage,
                Manifest.Permission.AccessNotificationPolicy,
               Manifest.Permission.ReadExternalStorage
            };
        const int RequestedId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //Other initialization stuff
           

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            RequestPermissions(Permissions,RequestedId);
            LoadApplication(new App());
             FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }
    }
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }


            //If debug you should reset the token each time.
            FirebasePushNotificationManager.Initialize(this, true);

            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {


            };


        }
        
    }

}
