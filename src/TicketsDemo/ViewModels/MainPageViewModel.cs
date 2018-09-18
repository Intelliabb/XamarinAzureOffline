using System;
using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TicketsDemo.Services.Abstractions;
using TicketsDemo.Models;
using TicketsDemo.Views;
using System.Collections.Generic;
using TicketsDemo.Common;

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
            TicketTappedCommand = new DelegateCommand<Ticket>(OnTicketTapped);
        }

        async void OnTicketTapped(Ticket obj)
        {
            await _navigationService.NavigateAsync(nameof(AddPage), new NavigationParameters {
                {AppConstants.TICKET_KEY, obj}});
        }

        async void OnAddTapped()
        {
            await _navigationService.NavigateAsync(nameof(AddPage));
        }

        List<Ticket> _tickets;
        public List<Ticket> Tickets
        {
            get { return _tickets; }
            set { SetProperty(ref _tickets, value); }
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand<Ticket> TicketTappedCommand { get; private set; }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            Tickets = await _ticketsService.GetTickets();
        }
    }
}
