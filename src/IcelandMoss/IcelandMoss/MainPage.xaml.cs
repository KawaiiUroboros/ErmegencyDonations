
using IcelandMoss.Controls;
using IcelandMoss.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net.Http;
using Plugin.FirebasePushNotification;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Essentials;
using System.IO;
using System.Net;
using Newtonsoft;
using Newtonsoft.Json;

namespace IcelandMoss
{/// <summary>
/// for getting a header from api and other information
/// </summary>
    public class Response
    {
        /// <summary>
        /// header of an article
        /// </summary>
       public string header { get; set; }
        /// <summary>
        /// link to an article
        /// </summary>
        public string link { get; set; }
    }

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    /// <summary>
    /// The start point of a programm
    /// </summary> 
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string firebase = "https://wwf.ru/";
        public string Mew;
        /// <summary>
        /// action open page with link for observation a global event
        /// </summary>
        public static Action<string> FireBasePushPage;
        /// <summary>
        /// states of a mainpage's search option
        /// </summary>
        enum States
        {
            SearchExpanded,
            SearchHidden
        }
        /// <summary>
        /// http client to ge header and other form api emergedon
        /// </summary>
        private static readonly HttpClient client = new HttpClient();
        /// <summary>
        /// headers for api dictionary
        /// </summary>
        private Dictionary<string, string> Headers = new Dictionary<string, string>();
        /// <summary>
        /// intizializator of the main page
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            SizeChanged += MainPage_SizeChanged;
            InizializeHeaderAsync();
            //Notification opened
            CrossFirebasePushNotification.Current.OnNotificationOpened += async (s, e) =>
            {
                firebase = e.Data["header"].ToString();
                SearchEntry.Text = e.Data["header"].ToString();
                SearchEntry.TextColor = Color.FromHex(e.Data["color"].ToString());
                await Navigation.PushAsync(new PushArticle(this,e.Data["link"].ToString()));
                Headers["header"] = e.Data["header"].ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://emergedonapi.azurewebsites.net/post");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{"+$"\"header\":\"{e.Data["header"]}\", "+$"\"link\":\"{e.Data["link"]}\""+"}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    SearchEntry.Text = JsonConvert.DeserializeObject<Response>(streamReader.ReadToEnd()).header;
                }
            };
        }
        /// <summary>
        /// gets the header of the main news
        /// </summary>
        /// <returns>nothing SearchEntry.text will be our header</returns>
        private async Task InizializeHeaderAsync()
        {
            WebRequest request = WebRequest.Create("https://emergedonapi.azurewebsites.net/get");
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var responseobj = JsonConvert.DeserializeObject<Response>(reader.ReadToEnd());
                    SearchEntry.Text = responseobj.header;
                    SearchLink = responseobj.link;

                }
            }
            response.Close();
        }

        /// <summary>
        /// animation configs
        /// </summary>
        Storyboard _storyboard = new Storyboard();
        /// <summary>
        /// standart param of margin
        /// </summary>
        const int margin = 20;
        /// <summary>
        /// standart param of snimation
        /// </summary>
        const int animationSpeed = 250;
        /// <summary>
        /// natives
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            ScrollContainer.Scrolled += ScrollContainer_Scrolled;
        }

        /// <summary>
        /// Animation for a page as it scrolls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ScrollContainer_Scrolled(object sender, ScrolledEventArgs e)
        {
            if ((e.ScrollY > 0) && (CurrentState != States.SearchHidden))
            {
                _storyboard.Go(States.SearchHidden);
                CurrentState = States.SearchHidden;
                ScrollContainer.IsEnabled = false;
                await Task.Delay(animationSpeed);
                ScrollContainer.IsEnabled = true; 
            }
            else if ((e.ScrollY == 0) && (CurrentState != States.SearchExpanded))
            {
                _storyboard.Go(States.SearchExpanded);
                CurrentState = States.SearchExpanded;
                ScrollContainer.IsEnabled = false;
                await Task.Delay(animationSpeed);
                ScrollContainer.IsEnabled = true;
            }


        }
        /// <summary>
        /// things changed after page started scrolling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_SizeChanged(object sender, EventArgs e)
        {

            _storyboard = new Storyboard();
            var width = this.Width;
            var height = this.Height;

            // shopping cart
            Rectangle basketRect = new Rectangle(
                x: width - (BasketIcon.Width + margin),
                y: margin,
                width: BasketIcon.Width,
                height: BasketIcon.Height
                );
            AbsoluteLayout.SetLayoutBounds(BasketIcon, basketRect);

            // search icon
            Rectangle searchRect = new Rectangle(
                x: margin,
                y: 200,
                width: SearchIcon.Width,
                height: SearchIcon.Height
                );
            AbsoluteLayout.SetLayoutBounds(SearchIcon, searchRect);

            Rectangle searchRectCollapsed = new Rectangle(
                x: BasketIcon.Bounds.Left - (margin + SettingsIcon.Width + margin + SearchIcon.Width),
                y: margin,
                width: SearchIcon.Width,
                height: SearchIcon.Height
            );

            // settings icon
            Rectangle settingsRect = new Rectangle(
                x: width - (SettingsIcon.Width + margin),
                y: 200,
                width: SettingsIcon.Width,
                height: SettingsIcon.Height
                );
            AbsoluteLayout.SetLayoutBounds(SettingsIcon, settingsRect);

            Rectangle settingsRectCollapsed = new Rectangle(
                x: BasketIcon.Bounds.Left - (margin + SettingsIcon.Width),
                y: margin,
                width: SettingsIcon.Width,
                height: SettingsIcon.Height
                );



            Rectangle searchBackgroundRect = new Rectangle(
                x: margin,
                y: 200,
                width: SettingsIcon.Bounds.X - (margin + margin),
                height: SearchBackground.Height
                );
            AbsoluteLayout.SetLayoutBounds(SearchBackground, searchBackgroundRect);

            Rectangle searchBackgroundCollapsedRect = new Rectangle(
                x: BasketIcon.Bounds.Left - (margin + SettingsIcon.Width + margin + SearchIcon.Width),
                y: margin,
                width: SettingsIcon.Width,
                height: SettingsIcon.Height
            );


            // ScrollContainer
            Rectangle scrollContainerRect = new Rectangle(
                x: margin,
                y: SearchIcon.Bounds.Bottom + margin,
                width: width - (2 * margin),
                height: height - (SearchIcon.Bounds.Bottom + margin)
                );
            AbsoluteLayout.SetLayoutBounds(ScrollContainer, scrollContainerRect);

            Rectangle scrollContainerRectCollapsed = new Rectangle(
                x: margin,
                y: margin + BasketIcon.Height + margin,
                width: width - (2 * margin),
                height: height - (margin + BasketIcon.Height + margin)
                );

            // add the positions to the state machine
            _storyboard.Add(States.SearchExpanded, new[]
            {
                new ViewTransition(Header, AnimationType.Opacity, 1, animationSpeed),
                new ViewTransition(SearchEntry, AnimationType.Opacity, 1, animationSpeed),
                new ViewTransition(SettingsIcon, AnimationType.Layout, settingsRect, animationSpeed),
                new ViewTransition(SearchIcon, AnimationType.Layout, searchRect, animationSpeed),
                new ViewTransition(SearchBackground, AnimationType.Layout, searchBackgroundRect, animationSpeed),
                new ViewTransition(ScrollContainer, AnimationType.Layout, scrollContainerRect, animationSpeed)
            });

            _storyboard.Add(States.SearchHidden, new[]
            {
                new ViewTransition(Header, AnimationType.Opacity, 0.01, animationSpeed),
                new ViewTransition(SearchEntry, AnimationType.Opacity, 0.01),
                new ViewTransition(SettingsIcon, AnimationType.Layout, settingsRectCollapsed, animationSpeed),
                new ViewTransition(SearchIcon, AnimationType.Layout, searchRectCollapsed, animationSpeed),
                new ViewTransition(SearchBackground, AnimationType.Layout, searchBackgroundCollapsedRect, animationSpeed),
                new ViewTransition(ScrollContainer, AnimationType.Layout, scrollContainerRectCollapsed, animationSpeed)
            });
            Vibration.Vibrate(20);


        }
        /// <summary>
        /// ntives after closing app
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SizeChanged -= MainPage_SizeChanged;
           
        }

       /// <summary>
       /// when size changed after hamburger button initialized second time
       /// </summary>
       /// <param name="width"></param>
       /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Vibration.Vibrate(20);

        }
        /// <summary>
        /// displayas a current state of main page panels
        /// </summary>
        States CurrentState = States.SearchExpanded;
        /// <summary>
        /// store link of an article
        /// </summary>
        public string SearchLink { get; private set; }

        /// <summary>
        /// animation on a menu hamburger button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(20);
            States newState;
            if (CurrentState == States.SearchExpanded)
                newState = States.SearchHidden;
            else
                newState = States.SearchExpanded;

            _storyboard.Go(newState);
            CurrentState = newState;
            
        }
        /// <summary>
        /// animation activated by pressing on a product panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">never used, do not try</param>
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // the user has tapped on an element
            ProductDisplay element = sender as ProductDisplay;

            // set the binding context to the selected cell
            FakeProductCell.BindingContext = element.BindingContext;
            FakeProductCell.ImageOffsetX = element.ImageOffsetX;
            FakeProductCell.ImageOffsetY = element.ImageOffsetY;
            FakeProductCell.Opacity = 1;
            FakeProductCell.IsVisible = true;

            // set the selected item
            ((MainViewModel)this.BindingContext).SelectedProduct = element.BindingContext as ProductViewModel;

            // set the layout to the same postion
            var yScroll = ScrollContainer.ScrollY;
            Rectangle rect = new Rectangle(
                x: ScrollContainer.X + element.X,
                y: ScrollContainer.Y + element.Y - yScroll,
                width: element.Width,
            height: element.Height);
            AbsoluteLayout.SetLayoutBounds(FakeProductCell, rect);

            // hide the cell we clicked on
            element.Opacity = 0.01;
            await FakeProductCell.ExpandToFill(this.Bounds);
            element.Opacity = 1;

            // display the page popover
            PagePopover.Opacity = 0;
            PagePopover.IsVisible = true;
            await PagePopover.Expand();
            Vibration.Vibrate(20);
        }
        /// <summary>
        /// closing a pop up animation
        /// </summary>
        /// <returns></returns>
        internal async Task HidePopover()
        {

            // fade out the elements
            await Task.WhenAll(
                new Task[]
                {
                    FakeProductCell.FadeTo(0),
                    PagePopover.FadeTo(0)
                });

            // hide our fake product cell
            FakeProductCell.IsVisible = false;

            // hide our Page poper
            PagePopover.IsVisible = false;
            Vibration.Vibrate(20);
        }


        /// <summary>
        /// challenged a tapping mechaning, closing a panel
        /// </summary>
        /// <param name="sender">something that challenging a tap gesture</param>
        /// <param name="e"></param>
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            ((View)sender).IsVisible = false;
            Vibration.Vibrate(20);
        }
        /// <summary>
        /// FUTURE:animation adding to cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductDisplay_AddToCartClicked(object sender, EventArgs e)
        {
            // selected product
            ProductDisplay element = sender as ProductDisplay;
            ProductViewModel item = element.BindingContext as ProductViewModel;

            // add a shopping card item
            ((MainViewModel)this.BindingContext).ShoppingCart.IncrementOrder(item);
            Vibration.Vibrate(20);
        }
        /// <summary>
        /// FUTURE: showing a cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasketIcon_Clicked(object sender, EventArgs e)
        {
            CartPopover.IsVisible = true;
        }
        /// <summary>
        /// search for the lastest news
        /// </summary>
        /// <param name="sender">search icon</param>
        /// <param name="e">data</param>
        private async void SearchIcon_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PushArticle(this,SearchLink ?? "wwf.ru"));
        }
    }
}
