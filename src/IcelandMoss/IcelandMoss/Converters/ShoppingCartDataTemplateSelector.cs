using IcelandMoss.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IcelandMoss.Converters
{/// <summary>
/// FUTURE: For a cart
/// </summary>
    public class ShoppingCartDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ProductItem { get; set; }
        public DataTemplate FreightItem { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ShoppingCartItem)
                return ProductItem;
            else
                return FreightItem;
        }
    }
}
