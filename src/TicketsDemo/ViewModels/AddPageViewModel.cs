using System;
using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TicketsDemo.Services.Abstractions;
using TicketsDemo.Models;
using TicketsDemo.Common;

namespace TicketsDemo.ViewModels
{
    public class AddPageViewModel : ViewModelBase
    {
        readonly ITicketsService _ticketsService;

        public AddPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                 IDeviceService deviceService, ITicketsService ticketsService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "New Ticket";
            _ticketsService = ticketsService;
            AddCommand = new DelegateCommand(OnAddTapped);
        }

        async void OnAddTapped()
        {
            if (OperationText == AppConstants.UPDATE)
                await _ticketsService.UpdateTicket(Ticket);
            else 
                await _ticketsService.AddTicket(Ticket);
            
            await _navigationService.GoBackAsync();
        }

        Ticket _ticket;
        public Ticket Ticket
        {
            get { return _ticket; }
            set { SetProperty(ref _ticket, value); }
        }

        string _operationText;
        public string OperationText
        {
            get { return _operationText; }
            set { SetProperty(ref _operationText, value); }
        }

        private int[] _priorities = new int[] { 1, 2, 3, 4 };
        public int[] Priorities => _priorities;

        public DelegateCommand AddCommand { get; private set; }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.ContainsKey(AppConstants.TICKET_KEY))
            {
                Ticket = (Ticket)parameters[AppConstants.TICKET_KEY];
                OperationText = AppConstants.UPDATE;
            }
            else
            {
                Ticket = new Ticket();
                OperationText = AppConstants.ADD;
            }
        }
    }
}
