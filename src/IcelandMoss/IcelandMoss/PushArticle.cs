
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IcelandMoss
{/// <summary>
/// a page that shows a web article of donation
/// </summary>
    class PushArticle : ContentPage
    {
        WebView webView;
        /// <summary>
        /// a CONSTRUCTER page with donation api with url
        /// </summary>
        /// <param name="url"></param>
        public PushArticle(Page before, string url)
        {
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = url },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            this.Content = new StackLayout { Children = { webView } };
            NavigationPage.SetHasNavigationBar(this, false);
        }
        
        
    }
}
