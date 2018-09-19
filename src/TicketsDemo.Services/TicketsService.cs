using System.Threading.Tasks;
using TicketsDemo.Models;
using TicketsDemo.Services.Abstractions;
using Microsoft.WindowsAzure.MobileServices;
using TicketsDemo.Common;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TicketsDemo.Services
{
    public class TicketsService : ITicketsService
    {
        readonly IMobileServiceClient _client;
        IMobileServiceSyncTable<Ticket> _ticketsTable;

        public TicketsService()
        {
            _client = new MobileServiceClient(AppConstants.AppServiceUrl);
            InitializeOfflineStore();
        }

        void InitializeOfflineStore()
        {
            var store = new MobileServiceSQLiteStore(AppConstants.DB_FILENAME);
            store.DefineTable<Ticket>();
            _client.SyncContext.InitializeAsync(store);

            _ticketsTable = _client.GetSyncTable<Ticket>();
        }

        async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await _client.SyncContext.PushAsync();
                await _ticketsTable.PullAsync("all", _ticketsTable.CreateQuery());

            }
            catch (MobileServicePushFailedException ex)
            {
                if(ex.PushResult != null)
                {
                    syncErrors = ex.PushResult.Errors;
                }
            }

            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        // Update failed, revert to server's copy
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
            else
                Debug.WriteLine("Synced successfully.");
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

        public async Task<ObservableCollection<Ticket>> GetTickets(bool sync = true)
        {
            if(sync)
                await SyncAsync();
            
            var results = await _ticketsTable.OrderBy(_=>_.Priority).ThenBy(_=>_.CreatedAt).ToEnumerableAsync();
            return new ObservableCollection<Ticket>(results);
        }

        public Task UpdateTicket(Ticket ticket)
        {
            return _ticketsTable.UpdateAsync(ticket);
        }
    }
}
