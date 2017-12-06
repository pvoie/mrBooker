﻿using System.Collections.Generic;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReservationApiController> _logger;

        public ReservationApiController(IUnitOfWork unitOfWork, IRepository<Reservation> reservationRepository,
            ILogger<ReservationApiController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: api/ReservationApi
        [HttpGet]
        public IActionResult Get()
        {
            var data = new SchdulerDataViewModel
            {
                data = new List<SchedulerEventModel>()
            };

            var dbData = _unitOfWork.ReservationRepository.GetAll();

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
            return _unitOfWork.ReservationRepository.Get(id);
        }

        // POST: api/ReservationApi
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public void Post([FromBody]SchedulerEventModel model)
        {
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