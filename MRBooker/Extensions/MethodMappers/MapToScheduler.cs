using System;
using System.Globalization;
using MRBooker.Data.Models.Entities;
using MRBooker.Data.SchedulerModels;
using System.Collections.Generic;

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
                StartDate = Convert.ToString(model.Start, CultureInfo.InvariantCulture),
                EndDate = Convert.ToString(model.End, CultureInfo.InvariantCulture),
                Type = 1
            };

            return reservation;
        }

        public static IEnumerable<SchedulerEventModel> ToSchedulerEventModelList(this IEnumerable<Reservation> model)
        {
            if (model == null) return null;

            var result = new List<SchedulerEventModel>();
            foreach (var item in model)
            {
                result.Add(item.ToSchedulerModel());
            }
           
            return result;
        }
    }
}
