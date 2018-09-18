using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TicketsDemo.Models;

namespace TicketsDemo.Services.Abstractions
{
    public interface ITicketsService
    {
        Task<ObservableCollection<Ticket>> GetTickets();
        Task<Ticket> GetTicket(string id);
        Task<Ticket> AddTicket(Ticket ticket);
        void DeleteTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
    }
}
