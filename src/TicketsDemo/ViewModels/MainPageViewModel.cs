﻿using System;
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
using System.Threading.Tasks;

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
            EditCommand = new DelegateCommand<Ticket>(OnEditTapped);
            RefreshCommand = new DelegateCommand(OnRefreshTapped);
        }

        #region Properties

        ObservableCollection<Ticket> _tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set { SetProperty(ref _tickets, value); }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand<Ticket> TicketTappedCommand { get; private set; }
        public DelegateCommand<Ticket> EditCommand { get; private set; }
        public DelegateCommand RefreshCommand { get; private set; }

        #endregion

        #region Methods

        async void OnRefreshTapped()
        {
            await Refresh();
        }

        async void OnTicketTapped(Ticket obj)
        {
            if (obj == null) return;
            IsBusy = true;
            obj.IsCompleted = !obj.IsCompleted;
            await _ticketsService.UpdateTicket(obj);
            await Refresh(false);
            IsBusy = false;
        }

        async void OnEditTapped(Ticket obj)
        {
            await _navigationService.NavigateAsync(nameof(AddPage), new NavigationParameters {
            {AppConstants.TICKET_KEY, obj}});
        }

        async void OnAddTapped()
        {
            await _navigationService.NavigateAsync(nameof(AddPage));
        }

        async Task Refresh(bool sync = true)
        {
            if (!sync)
                Tickets = await _ticketsService.GetTickets(sync);
            else
            {
                IsRefreshing = true;
                Tickets = await _ticketsService.GetTickets(sync);
                IsRefreshing = false;
            }
        }

        #endregion

        #region Overrides
        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            await Refresh();
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            if(parameters.GetNavigationMode() == NavigationMode.Back) {
                await Refresh();
            }
        }

        #endregion

    }
}
