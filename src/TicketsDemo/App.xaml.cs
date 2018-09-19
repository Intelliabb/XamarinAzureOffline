using System.Threading.Tasks;
using TicketsDemo.Views;
using Prism.Ioc;
using Unity;
using Prism.Unity;
using Prism.Logging;
using Xamarin.Forms;
using Prism;
using TicketsDemo.Services.Abstractions;
using TicketsDemo.Services;
using Plugin.FileSystem;
using Plugin.FileSystem.Abstractions;

namespace TicketsDemo
{
    public partial class App : PrismApplication
    {
        /* 
         * NOTE: 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App()
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(Views.MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<AddPage>();

            // Services
            containerRegistry.Register<ITicketsService, TicketsService>();
        }
    }
}
