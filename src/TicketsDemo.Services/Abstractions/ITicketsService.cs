using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsDemo.Models;

namespace TicketsDemo.Services.Abstractions
{
    public interface ITicketsService
    {
        Task<List<Ticket>> GetTickets();
        Task<Ticket> GetTicket(string id);
        Task AddTicket(Ticket ticket);
        Task DeleteTicket(Ticket ticket);
        Task UpdateTicket(Ticket ticket);
    }
}
