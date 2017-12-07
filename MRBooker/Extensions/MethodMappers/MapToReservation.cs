using System;
using MRBooker.Data.Models.Entities;
using MRBooker.Data.SchedulerModels;
using System.Globalization;

namespace MRBooker.Extensions.MethodMappers
{
    public static class MapToReservation
    {
        public static Reservation ToReservationModel(this SchedulerEventModel model)
        {
            if (model == null) return null;
            var reservation = new Reservation
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Status = model.Status,
                Start = Convert.ToDateTime(model.StartDate, CultureInfo.InvariantCulture),
                End = Convert.ToDateTime(model.EndDate, CultureInfo.InvariantCulture),
                Room = GetRoomContent(model)
            };

            return reservation;
        }

        private static Room GetRoomContent(this SchedulerEventModel model)
        {
            if (model == null) return null;

            var room = new Room
            {
                Name = model.Room.Name,
                Description = model.Room.Description,
                Capacity = model.Room.Capacity,
                Reservations = model.Room.Reservations,
                Place = model.Room.Place
            };

            return room;
        }
    }
}
