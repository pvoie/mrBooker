using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRBooker.Data.Models.Entities;
using MRBooker.Data.Repository;
using MRBooker.Data.SchedulerModels;
using MRBooker.Extensions.MethodMappers;
using Microsoft.Extensions.Logging;
using MRBooker.Data.UoW;

namespace MRBooker.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ReservationApi")]
    public class ReservationApiController : Controller
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReservationApiController> _logger;

        public ReservationApiController(IUnitOfWork unitOfWork, IRepository<Reservation> reservationRepository,
            ILogger<ReservationApiController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _reservationRepository = reservationRepository;
        }

        // GET: api/ReservationApi
        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return _unitOfWork.ReservationRepository.GetAll();
        }

        // GET: api/ReservationApi/5
        [HttpGet("{id}", Name = "Get")]
        public Reservation Get(long id)
        {
            return _unitOfWork.ReservationRepository.Get(id);
        }
        
        // POST: api/ReservationApi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Post([FromBody]SchedulerEventModel model)
        {
            var reservation = model.ToReservationModel();
            _unitOfWork.ReservationRepository.Insert(reservation);
            _unitOfWork.Save();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var reservation = _unitOfWork.ReservationRepository.Get(id);
            _unitOfWork.ReservationRepository.Delete(reservation);
            _unitOfWork.Save();
        }
    }
}
