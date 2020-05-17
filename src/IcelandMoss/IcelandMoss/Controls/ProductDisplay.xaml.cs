using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IcelandMoss.Controls
{/// <summary>
/// a dispaing a product
/// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDisplay : ContentView
    {
        public ProductDisplay()
        {
            InitializeComponent();
        }
        /// <summary>
        /// gets a coordinates as bindable property at Y coordinates IMage
        /// </summary>
        public static readonly BindableProperty ImageOffsetYProperty =
            BindableProperty.Create(nameof(ImageOffsetY), typeof(int), typeof(ProductDisplay), 0);

        /// <summary>
        /// gets a coordinates as bindable property at X coordinates IMage
        /// </summary>
        public static readonly BindableProperty ImageOffsetXProperty =
            BindableProperty.Create(nameof(ImageOffsetX), typeof(int), typeof(ProductDisplay), 0);

        /// <summary>
        /// sets an offset for imeges and animations
        /// </summary>
        public int ImageOffsetY
        {
            get => (int)GetValue(ImageOffsetYProperty);
            set => SetValue(ImageOffsetYProperty, value);
        }
        /// <summary>
        /// sets an offset for imeges and animations
        /// </summary>
        public int ImageOffsetX
        {
            get => (int)GetValue(ImageOffsetXProperty);
            set => SetValue(ImageOffsetXProperty, value);
        }
        /// <summary>
        /// gets a possition after animation
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ImageOffsetYProperty.PropertyName)
            {
                ProductImage.TranslationY = ImageOffsetY;
            }

            if (propertyName == ImageOffsetXProperty.PropertyName)
            {
                ProductImage.TranslationX = ImageOffsetX;
            }
        }

        /// <summary>
        /// FUTURE: a cart hendler
        /// </summary>
        public event EventHandler AddToCartClicked;
        /// <summary>
        /// a popup hendler
        /// </summary>
        public event EventHandler ProductClicked;

        /// <summary>
        /// a standart time for all animations
        /// </summary>
        const int animationSpeed = 500;
        /// <summary>
        /// animation of popUp panel
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        internal async Task ExpandToFill(Rectangle bounds)
        {
            // set the intial state
            AddBackground.Opacity = .5;
            AddButton.Opacity = 1;
            NameLabel.Opacity = 1;
            PriceLabel.Opacity = 1;
            ProductImage.Scale = 1;
            ProductImage.TranslationX = ImageOffsetX;
            ProductImage.TranslationY = ImageOffsetY;

            // destination rect
            var destRect = new Rectangle(
                x: (bounds.Width / 2) - (this.Width / 2),
                y: 40,
                width: this.Width,
                height: this.Height
                );

            _ = AddBackground.FadeTo(0, animationSpeed / 2);
            _ = AddButton.FadeTo(0, animationSpeed / 2);
            _ = NameLabel.FadeTo(0, animationSpeed / 2);
            _ = PriceLabel.FadeTo(0, animationSpeed / 2);

           
            _ = ProductImage.TranslateTo(0, ProductImage.TranslationY, animationSpeed * 2);
            await this.LayoutTo(destRect, animationSpeed * 2, Easing.SinInOut);


            _ = ProductImage.ScaleTo(1.1, animationSpeed /2);
            _ = ProductImage.TranslateTo(0, 100, animationSpeed /2 );
            Rectangle expandedBounds = bounds.Inflate(50, 50);
            await this.LayoutTo(expandedBounds, animationSpeed /2, Easing.SinInOut);
            AbsoluteLayout.SetLayoutBounds(this, expandedBounds);
        }
        /// <summary>
        /// a hendler for tapped gesture for a clicked product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TapProductGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            EventHandler handler = ProductClicked;
            handler?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// FUTURE: a hendler for a tap gesture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TapAddProductGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           
            EventHandler handler = AddToCartClicked;
            handler?.Invoke(this, new EventArgs());
        }
    }
}