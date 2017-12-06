using MRBooker.Data.Models.Entities;
using MRBooker.Data.SchedulerModels;

namespace MRBooker.Extensions.MethodMappers
{
    public static class MapToScheduler
    {
        public static SchedulerEventModel ToSchedulerModel(this Reservation model)
        {
            if (model == null) return null;
            var reservation = new SchedulerEventModel
            {
                Id = model.Id,
                Title = model.Title,
                //Description = model.Description,
                //Status = model.Status,
                StartDate = model.Start,
                EndDate = model.End,
                Type = 1
                //Room = model.Room
            };

            return reservation;
        }

    }
}
