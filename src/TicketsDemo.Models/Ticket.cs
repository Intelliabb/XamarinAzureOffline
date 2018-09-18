using Newtonsoft.Json;

namespace TicketsDemo.Models
{
    public class Ticket
    {
        public string ID
        {
            get;
            set;
        }

        public int Priority
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        [JsonProperty("Assigned_To")]
        public string AssignedTo
        {
            get;
            set;
        }

        [JsonProperty("Completed")]
        public bool IsCompleted
        {
            get;
            set;
        }
    }
}
