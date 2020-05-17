using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IcelandMoss.ViewModels
{
    public class ProductViewModel : ObservableObject
    {/// <summary>
    /// name of a product
    /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// a link for a fond
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// an image of Fund
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// a discription of fund
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// minimal price for a fund
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// an importance of a fund
        /// </summary>
        public string Importance { get; set; }
        /// <summary>
        /// a popularity of a fund
        /// </summary>
        public string Popularity { get; set; }

        /// <summary>
        /// Have to be added in the future
        /// </summary>
        public string HeroColor { get; set; }
        /// <summary>
        /// Have to be added in the future
        /// </summary>
        public bool IsFeatured { get; set; }
        /// <summary>
        /// Have to be added in the future
        /// </summary>
        public string Temperature { get; set; }
        /// <summary>
        /// Have to be added in the future
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Have to be added in the future
        /// </summary>
        public string Diameter { get; set; }
    }
}
