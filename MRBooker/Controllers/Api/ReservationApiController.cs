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

namespace MRBooker.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ReservationApi")]
    public class ReservationApiController : Controller
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly ILogger<ReservationApiController> _logger;

        public ReservationApiController(IRepository<Reservation> reservationRepository,
            ILogger<ReservationApiController> logger)
        {
            _logger = logger;
            _reservationRepository = reservationRepository;
        }

        // GET: api/ReservationApi
        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return _reservationRepository.GetAll();
        }

        // GET: api/ReservationApi/5
        [HttpGet("{id}", Name = "Get")]
        public Reservation Get(long id)
        {
            return _reservationRepository.Get(id);
        }
        
        // POST: api/ReservationApi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Post([FromBody]SchedulerEventModel model)
        {
            var reservation = model.ToReservationModel();
            _reservationRepository.Insert(reservation);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var reservation = _reservationRepository.Get(id);
            _reservationRepository.Delete(reservation);
        }
    }
}
