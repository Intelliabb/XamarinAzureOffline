using System;
using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TicketsDemo.Services.Abstractions;
using TicketsDemo.Models;
using TicketsDemo.Views;

namespace TicketsDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        readonly ITicketsService _ticketsService;

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                 IDeviceService deviceService, ITicketsService ticketsService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "Open Tickets";
            _ticketsService = ticketsService;
            AddCommand = new DelegateCommand(OnAddTapped);
        }

        async void OnAddTapped()
        {
            //Tickets.Add(new Ticket { Title = $"Ticket {DateTime.Now.ToShortTimeString()}", Description = "Auto generated", AssignedTo = "habbasi", Priority = 4, IsCompleted = false });
            await _navigationService.NavigateAsync(nameof(AddPage));
        }

        ObservableCollection<Ticket> _tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set { SetProperty(ref _tickets, value); }
        }

        public DelegateCommand AddCommand { get; private set; }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            Tickets = new ObservableCollection<Ticket> {
                new Ticket { Title = "Ticket 1", Description = "Ticket to fix problem 1.", AssignedTo = "habbasi", Priority = 1, IsCompleted = false },
                new Ticket { Title = "Ticket 2", Description = "Ticket to fix problem 2.", AssignedTo = "habbasi", Priority = 1, IsCompleted = false },
                new Ticket { Title = "Ticket 3", Description = "Ticket to fix problem 3.", AssignedTo = "habbasi", Priority = 2, IsCompleted = true },
                new Ticket { Title = "Ticket 4", Description = "Ticket to fix problem 4.", AssignedTo = "habbasi", Priority = 3, IsCompleted = false },
            };
        }
    }
}
