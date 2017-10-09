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
            if (Reservations == null)
            {
                return string.Empty;
            }
            else
            {
                return JsonConvert.SerializeObject(Reservations);
            }
        }
    }
}
