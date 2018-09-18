using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TicketsDemo.Views
{
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PriorityPicker.ItemsSource = new int[] { 1, 2, 3, 4 };
        }
    }
}
