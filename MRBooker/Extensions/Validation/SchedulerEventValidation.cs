using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MRBooker.Data.SchedulerModels;
using MRBooker.Data.UoW;

namespace MRBooker.Extensions.Validation
{
    public class SchedulerEventValidation
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchedulerEventValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool ValidateSchedulerEventModel(SchedulerEventModel model)
        {
            bool isOverlapped = false;
            if (model == null) return false;

            var reservationsByRoom = _unitOfWork.ReservationRepository.GetAll().Include(x => x.Room).Where(r => r.RoomId == model.RoomId);

            foreach (var reservation in reservationsByRoom)
            {
                isOverlapped = reservation.Start < Convert.ToDateTime(model.EndDate) && Convert.ToDateTime(model.StartDate) < reservation.End;
            }
            return isOverlapped;
        }
    }
}
