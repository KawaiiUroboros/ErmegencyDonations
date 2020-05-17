
using IcelandMoss.Controls;
using Plugin.FirebasePushNotification;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IcelandMoss
{
    
    public partial class App : Application
    {
        public Page _mainPage;
        public static string DoYouSeeMe;
        public App()
        {
           

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            _mainPage = MainPage;
            CrossFirebasePushNotification.Current.RegisterForPushNotifications();
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
            CrossFirebasePushNotification.Current.OnNotificationOpened +=  (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    
                }
               

            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
