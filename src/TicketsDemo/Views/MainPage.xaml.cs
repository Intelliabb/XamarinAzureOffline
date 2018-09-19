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
    }
}
