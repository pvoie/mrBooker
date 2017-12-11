using MRBooker.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRBooker.Data.AdministrationViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<Room> Rooms { get; set; }

        public IEnumerable<Place> Places { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
