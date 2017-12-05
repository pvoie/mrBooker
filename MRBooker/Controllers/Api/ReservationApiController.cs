using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MRBooker.Data.Models.Entities;
using MRBooker.Data.Repository;
using MRBooker.Data.SchedulerModels;
using MRBooker.Extensions.MethodMappers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        public IActionResult Get()
        {
            var data = new SchdulerDataViewModel
            {
                data = new List<SchedulerEventModel>()
            };
            
            var dbData = _reservationRepository.GetAll();
            foreach (var res in dbData)
            {
                data.data.Add(res.ToSchedulerModel());
            }

            return Json(data);
        }

        // GET: api/ReservationApi/5
        [HttpGet("{id}", Name = "Get")]
        public Reservation Get(long id)
        {
            return _reservationRepository.Get(id);
        }

        // POST: api/ReservationApi
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public void Post([FromBody]SchedulerEventModel model)
        {
            //var newReservation = new Reservation();
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
