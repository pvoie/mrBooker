using Newtonsoft.Json;
using System.Collections.Generic;

namespace MRBooker.Data.ReservationViewModels
{
    public class ReservationViewModel
    {
        public ICollection<string> Reservations { get; set; }

        public string JsonList { get; set; }

        public string ToJsonList()
        {
            return Reservations == null ? string.Empty : JsonConvert.SerializeObject(Reservations, Formatting.Indented);
        }
    }
}
