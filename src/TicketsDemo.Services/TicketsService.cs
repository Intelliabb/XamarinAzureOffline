using System.Threading.Tasks;
using TicketsDemo.Models;
using TicketsDemo.Services.Abstractions;
using Microsoft.WindowsAzure.MobileServices;
using TicketsDemo.Common;
using System.Collections.Generic;

namespace TicketsDemo.Services
{
    public class TicketsService : ITicketsService
    {
        readonly IMobileServiceClient _client;
        readonly IMobileServiceTable<Ticket> _ticketsTable;

        public TicketsService()
        {
            _client = new MobileServiceClient(AppConstants.AppServiceUrl);
            _ticketsTable = _client.GetTable<Ticket>();
        }

        public Task AddTicket(Ticket ticket)
        {
            return _ticketsTable.InsertAsync(ticket);
        }

        public Task DeleteTicket(Ticket ticket)
        {
            return _ticketsTable.DeleteAsync(ticket);
        }

        public Task<Ticket> GetTicket(string id)
        {
            return _ticketsTable.LookupAsync(id);
        }

        public Task<List<Ticket>> GetTickets()
        {
            return _ticketsTable.ToListAsync();
        }

        public Task UpdateTicket(Ticket ticket)
        {
            return _ticketsTable.UpdateAsync(ticket);
        }
    }
}
