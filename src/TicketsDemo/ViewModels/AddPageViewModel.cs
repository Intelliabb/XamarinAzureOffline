using System;
using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TicketsDemo.Services.Abstractions;
using TicketsDemo.Models;

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
            await _navigationService.GoBackAsync();
        }

        Ticket _ticket = new Ticket();
        public Ticket Ticket
        {
            get { return _ticket; }
            set { SetProperty(ref _ticket, value); }
        }

        public DelegateCommand AddCommand { get; private set; }
    }
}
