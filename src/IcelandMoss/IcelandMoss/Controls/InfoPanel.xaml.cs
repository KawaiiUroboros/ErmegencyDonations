﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IcelandMoss.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPanel : ContentView
    {
        #region Bindable Properties
        /// <summary>
        /// Bindable property for Title
        /// </summary>
        public static readonly BindableProperty TitleProperty = 
            BindableProperty.Create(
                nameof(Title), 
                typeof(string), 
                typeof(InfoPanel), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as InfoPanel;
            if (newV != null && !(newV is string)) return;
            var oldTitle = (string)old;
            var newTitle = (string)newV;
            me?.TitleChanged(oldTitle, newTitle);
        });

        private void TitleChanged(string oldTitle, string newTitle)
        {
            TitleLabel.Text = newTitle;
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        /// <summary>
        /// Bindable property for icon
        /// </summary>
        public static readonly BindableProperty TitleIconProperty = BindableProperty.Create(nameof(TitleIcon), typeof(string), typeof(InfoPanel), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as InfoPanel;
            if (newV != null && !(newV is string)) return;
            var oldTitleIcon = (string)old;
            var newTitleIcon = (string)newV;
            me?.TitleIconChanged(oldTitleIcon, newTitleIcon);
        });

        private void TitleIconChanged(string oldTitleIcon, string newTitleIcon)
        {
            TitleIconSpan.Text = newTitleIcon;
        }
        /// <summary>
        /// for binding TitleIcon
        /// </summary>
        public string TitleIcon
        {
            get => (string)GetValue(TitleIconProperty);
            set => SetValue(TitleIconProperty, value);
        }
        /// <summary>
        /// price bindable property
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(InfoPanel), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as InfoPanel;
            if (newV != null && !(newV is string)) return;
            var oldValue = (string)old;
            var newValue = (string)newV;
            me?.ValueChanged(oldValue, newValue);
        });

        private void ValueChanged(string oldValue, string newValue)
        {
            ValueLabel.Text = newValue;
        }
        /// <summary>
        /// for binding a value
        /// </summary>
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion

        public InfoPanel()
        {
            InitializeComponent();
        }
    }
}