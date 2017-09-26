using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MRBooker.Data.ReservationViewModels
{
    [DataContract]
    public class DisplaySchedulerReservationViewModel
    {
        [Display(Name = "id")]
        public long Id { get; set; }

        [Display(Name = "text")]
        public string Text { get; set; }

        [Display(Name = "start_date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "end_date")]
        public DateTime EndDate { get; set; }
    }
}
