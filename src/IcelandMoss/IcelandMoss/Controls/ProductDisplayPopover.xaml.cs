
using IcelandMoss.Extensions;
using IcelandMoss.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IcelandMoss.Controls
{
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