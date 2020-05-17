using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IcelandMoss.Controls
{/// <summary>
/// a donation page api
/// </summary>
    public class DonationPage:ContentPage
    {
        WebView webView;
        /// <summary>
        /// a CONSTRUCTER page with donation api with url
        /// </summary>
        /// <param name="url"></param>
        public DonationPage(string url)
        { 
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = url },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            
            this.Content = new StackLayout { Children = { webView } };
        }
    }
}
