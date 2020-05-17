using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcelandMoss.ViewModels
{
    /// <summary>
    /// Future: a shopping cart item model
    /// </summary>
    public class ShoppingCartItem : ObservableObject, ICartItem
    {/// <summary>
     /// Future: will be a cart for a products
     /// </summary>
        public ProductViewModel Product { get; set; }
        /// <summary>
        /// count for a price
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// the sum of prices
        /// </summary>
        public decimal Total
        {
            get { return Product.Price * Count; }
        }
    }
}
