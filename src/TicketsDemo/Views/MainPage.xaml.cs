using TicketsDemo.Models;
using TicketsDemo.ViewModels;
using Xamarin.Forms;

namespace TicketsDemo.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(sender is ListView lv)) return;
            lv.SelectedItem = null;
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            // HACK: Command not working right.
            var x = e as TappedEventArgs;
            if (!(BindingContext is MainPageViewModel vm)) return;
            vm.TicketTappedCommand?.Execute((Ticket)x?.Parameter);
        }
    }
}
