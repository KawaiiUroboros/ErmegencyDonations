
using Android.Content;
using Android.Graphics;
using Android.Provider;
using IcelandMoss.Extensions;
using IcelandMoss.ViewModels;
using Java.IO;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IcelandMoss.Controls
{
    public interface IFileService
    {
        void SavePicture(string url);
    }
    /// <summary>
    /// will pop up if you want to tap a panel
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDisplayPopover : ContentView
    {/// <summary>
    /// a public of a product
    /// </summary>
        public ProductDisplayPopover()
        {
            InitializeComponent();
        }
        /// <summary>
        /// animation for popup
        /// </summary>
        /// <returns></returns>
        internal async Task Expand()
        {
            // setup for animation
            this.Opacity = 1;
            ProductPopoverGrid.Opacity = 0;
            DonateButton.ScaleX = 0;
            BackgroundPanel.TranslationY = BackgroundPanel.Height;

            // animate up the white background
            _ = BackgroundPanel.TranslateTo(0, 0, 200);

            // animate in the Details
            await ProductPopoverGrid.FadeTo(1, 1000);

            // animate the button
            Animation animation = new Animation();
            animation.Add(0, 1, new Animation(t => DonateButton.ScaleX = t, 0, 1, Easing.SpringOut));
            animation.Commit(this, "ButtonAnimation", 30, 500);
        }
        /// <summary>
        /// will close a pop up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackArrowButton_Clicked(object sender, EventArgs e)
        {
            // get the parent page
            await ((MainPage)this.GetParentPage()).HidePopover();
        }
        /// <summary>
        /// FUTURE: will be a times to make a lot of donations
        /// </summary>
        int quantityCount = 1;
        /// <summary>
        /// to increases mothes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncreaseQuantity_Clicked(object sender, EventArgs e)
        {
            quantityCount++;
            UpdateDisplay();
        }
        static readonly HttpClient _client = new HttpClient();

        public static Task<byte[]> DownloadImage(string imageUrl)
        {
            if (!imageUrl.Trim().StartsWith("https", StringComparison.OrdinalIgnoreCase))
                throw new Exception("iOS and Android Require Https");

            return _client.GetByteArrayAsync(imageUrl);
        }
        public  void AddImage(object sender, EventArgs e)
        {
            string url = ((MainViewModel)this.BindingContext).SelectedProduct.ImageUrl;
            System.Console.WriteLine(url);
           byte[] i = DownloadImage(url).Result;
            DependencyService.Get<IFileService>().SavePicture(url); 
            System.Console.WriteLine("done");



        }


      public void SavePictureToDisk(string filename, byte[] imageData)  
        {  
            var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);  
            var pictures = dir.AbsolutePath;  
            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name  
            string name = filename + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";  
            string filePath = System.IO.Path.Combine(pictures, name);  
            try  
            {  
                System.IO.File.WriteAllBytes(filePath, imageData);  
                //mediascan adds the saved image into the gallery  
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                SaveToDisk(filePath, imageData);
            }  
            catch(System.Exception e)  
            {  
                System.Console.WriteLine(e.ToString());  
            }  

        }  
        public static void SaveToDisk(string imageFileName, byte[] imageAsBase64String)
        {
            Xamarin.Essentials.Preferences.Set(imageFileName, Convert.ToBase64String(imageAsBase64String));
        }
        /// <summary>
        /// FUTURE:To reduce monthes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecreaseQuantity_Clicked(object sender, EventArgs e)
        {
            quantityCount--;
            if (quantityCount < 1) quantityCount = 1;
            UpdateDisplay();
        }
        /// <summary>
        /// FUTURE:change the price of donation
        /// </summary>
        private void UpdateDisplay()
        {
            QuantityDisplay.Text = quantityCount.ToString();
            var unitPrice = ((MainViewModel)this.BindingContext).SelectedProduct.Price;
            QuantityDisplayValue.Text = (unitPrice * quantityCount).ToString();
        }
        /// <summary>
        /// A a button to proceed to donation page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            Vibration.Vibrate();
            await Navigation.PushAsync(new DonationPage(Url.Text));
        }
    }
}