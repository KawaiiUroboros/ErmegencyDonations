using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IcelandMoss.ViewModels
{/// <summary>
/// model for a starting page
/// </summary>
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// DataBase of Funds
        /// </summary>
        public IList<ProductViewModel> Products { get; set; }
        /// <summary>
        /// injection of selected product
        /// </summary>
        private ProductViewModel _selectedProduct;
        /// <summary>
        /// bindable selected fund
        /// </summary>
        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }
        /// <summary>
        /// Future: a shop cart
        /// </summary>
        public ShoppingCartViewModel ShoppingCart { get; set; }
        /// <summary>
        /// Future:remove from a shopping cart
        /// </summary>
        public ICommand RemoveItemCommand { private set; get; }

        /// <summary>
        /// a standart base of data
        /// </summary>
        public MainViewModel()
        {
            Products = new ObservableRangeCollection<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    HeroColor = "#95C9F7",
                    Name="American Humane",
                    Url ="https://secure.americanhumane.org/site/Donation2;jsessionid=00000000.app278b?df_id=7850&mfc_pref=T&7850.donation=form1&s_src=FY20GETINVPAGE&sub_src=onetime&NONCE_TOKEN=13D53CC5D278F6ECCC04A193B9B7FC0",
                    Price = 25,
                    ImageUrl = "https://i.imgur.com/ITQbt7j.png",
                    IsFeatured = true,
                    Description = "American Humane is an organization founded in 1877, committed to ensuring the safety, welfare and well-being of animals. American Humane's leadership programs are first to serve in promoting and nurturing the bonds between animals and humans.",
                    Importance = "75%",
                    Popularity = "65%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "190 mm"
                },
                new ProductViewModel()
                {
                    HeroColor = "#FFCA81",
                    Name="World Wildlife Fund",
                    Price = 10,
                    ImageUrl = "https://miro.medium.com/max/1200/1*694y5GwOWsqflOtu_4Lj-g.png",
                    Url = "https://wwf.ru/help/form/priroda/",
                    IsFeatured = true,
                    Description = "The World Wide Fund for Nature is an international non-governmental organization founded in 1961, working in the field of wilderness preservation, and the reduction of human impact on the environment. It was formerly named the World Wildlife Fund, which remains its official name in Canada and the United States.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },

                new ProductViewModel()
                {
                    HeroColor = "#A2BAD3",
                    Name="Africa Wildlife Foundation",
                    Price = 5,
                    ImageUrl = "https://www.motherjones.com/wp-content/uploads/2019/12/Getty121719.jpg?w=1200&h=630&crop=1",
                    Url = "https://secure.awf.org/membership",
                    IsFeatured = true,
                    Description = "The Africa Wildlife Foundation strives to protect endangered wildlife and their habitats across Africa so they can continue to thrive. Along with combating animal poaching and trafficking, the organization helps with economic development and community empowerment throughout the continent.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },

                new ProductViewModel()
                {
                    HeroColor = "#e39545",
                    Name="charity: water",
                    Price = 5,
                    ImageUrl = "https://misswood.es/subida/imagenes/Fotos-about-us/water-for-senegal.jpg",
                    Url = "https://www.charitywater.org/donate",
                    IsFeatured = true,
                    Description = "The Africa Wildlife Foundation strives to protect endangered wildlife and their habitats across Africa so they can continue to thrive. Along with combating animal poaching and trafficking, the organization helps with economic development and community empowerment throughout the continent.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },

                 new ProductViewModel()
                {
                    HeroColor = "#95C9F7",
                    Name="Mental Health America",
                    Price = 5,
                    ImageUrl = "https://img1.wsimg.com/isteam/ip/697eb937-26bc-4840-98f4-9bb04b32738e/12314242_1041417075898836_5959065866086447448_.jpg",
                    Url = "https://act.mhanational.org/site/Donation2?idb=763923797&df_id=2780&mfc_pref=T&2780.donation=form1&2780_donation=form1&NONCE_TOKEN=9D893D4CABA9E3F5A3979EBAEEC61544",
                    IsFeatured = true,
                    Description = "Founded in 1909, Mental Health America helps Americans understand, prevent, and treat mental health issues that can interfere with their overall wellness and ability to live a healthy life. They also focus on educating Americans about mental health issues through their online hub of information and tools.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },

                new ProductViewModel()
                {
                    HeroColor = "#fb893c",
                    Name="Action Against Hunger",
                    Price = 5,
                    ImageUrl = "https://www.wfpusa.org/wp-content/uploads/2019/03/SSD_20170923_WFP-Lara_Atanasijevic_0011-1024x683.jpg",
                    Url = "https://donate.hrw.org/page/15328/donate/1?ea.tracking.id=geo",
                    IsFeatured = true,
                    Description = "Defending the rights of people across the globe is no small task, but the organization Human Rights Watch has been doing just this since 1978. About 400 journalists, attorneys, country experts, and many others work to publish non-partisan and independent reports every year on human rights conditions around the world.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },
                new ProductViewModel()
                {
                    HeroColor = "#FB8183",
                    Name="German Red Cross",
                    Price = 3,
                    ImageUrl = "https://p1.pxfuel.com/preview/197/727/779/drc-german-red-cross.jpg",
                    Url = "https://www.drk.de/en/donate/donate-online/",
                    IsFeatured = true,
                    Description = "The German Red Cross is part of the International Red Cross and Red Crescent Movement, the largest humanitarian organisation in the world. It has provided comprehensive aid for more than 140 years for people in conflict situations, disasters and health or social emergency situations, solely based on their level of need.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },
                new ProductViewModel()
                {
                    HeroColor = "#74D69E",
                    Name="Germany's Relief Coalition",
                    Price = 10,
                    ImageUrl = "https://hive.news/upload/news/image_1581177733_38728579.jpg",
                    Url = "https://www.aktion-deutschland-hilft.de/en/donate/donate/",
                    IsFeatured = true,
                    Description = "Natural catastrophes occur ever more frequently, leaving behind devastating damage and costing many people’s lives. Therefore, it is important that people living in regions at risk of catastrophes such as, earthquakes, hurricanes, or tsunamis, are well prepared. Here is where disaster risk reduction programmes of the aid organizations of Aktion Deutschland Hilft step in.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },
                new ProductViewModel()
                {
                    HeroColor = "#000033",
                    Name="sea-watch",
                    Price = 5,
                    ImageUrl = "https://sea-watch.org/wp-content/uploads/2019/01/sea-watch_grodotzki_20190104_5880-1024x684.jpg",
                    Url = "https://sea-watch.org/en/donate/",
                    IsFeatured = true,
                    Description = "Sea-Watch e.V. is a non-profit organization that conducts civil search and rescue operations in the Central Med. In the presence of the humanitarian crisis, Sea-Watch provides emergency relief capacities, demands and pushes for rescue operations by the european institutions and stands up publicly for legal escape routes. Since a political solution in the sense of a #SafePassage is not on the horizon, we have expanded our field of operation and made new plans. We are politically and religiously independent and are financed solely through donations.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },
                new ProductViewModel()
                {
                    HeroColor = "#e34545",
                    Name="DKMS",
                    Price = 1,
                    ImageUrl = "https://wamu.org/wp-content/uploads/2020/04/blood-donation-1500x1000.jpg",
                    Url = "https://www.dkms.de/en/make-a-donation",
                    IsFeatured = true,
                    Description = "Defending the rights of people across the globe is no small task, but the organization Human Rights Watch has been doing just this since 1978. About 400 journalists, attorneys, country experts, and many others work to publish non-partisan and independent reports every year on human rights conditions around the world.",
                    Importance = "85%",
                    Popularity = "91%",
                    Temperature = "18 - 27 ℃",
                    Size = "150x150 mm",
                    Diameter = "200 mm"
                },

            };

            ShoppingCart = new ShoppingCartViewModel();
            ShoppingCart.Items.Add(new FreightItem() { FreightCharge = 15 });

            RemoveItemCommand = new Command<ShoppingCartItem>(i => RemoveItem(i));

        }
        /// <summary>
        /// FUTURE:
        /// will be roving from the cart
        /// </summary>
        /// <param name="i"></param>
        private void RemoveItem(ShoppingCartItem i)
        {
            ShoppingCart.RemoveItem(i);
        }
    }
}
