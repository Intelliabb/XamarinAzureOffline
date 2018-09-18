using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TicketsDemo.Models;
using TicketsDemo.Services.Abstractions;

namespace TicketsDemo.Services
{
    public class TicketsService : ITicketsService
    {
        public TicketsService()
        {
        }

        public Task<Ticket> AddTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void DeleteTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicket(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Ticket>> GetTickets()
        {
            throw new NotImplementedException();
        }

        public void UpdateTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
