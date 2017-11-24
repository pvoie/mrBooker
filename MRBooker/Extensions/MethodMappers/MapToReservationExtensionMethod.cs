using MRBooker.Data.Models.Entities;
using MRBooker.Data.SchedulerModels;

namespace MRBooker.Extensions.MethodMappers
{
    public static class MapToReservationExtensionMethod
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
                Start = model.StartDate,
                End = model.EndDate,
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
