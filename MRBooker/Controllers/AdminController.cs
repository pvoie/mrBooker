using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MRBooker.Data.UoW;
using Microsoft.Extensions.Logging;
using MRBooker.Data.AdministrationViewModels;

namespace MRBooker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IUnitOfWork unitOfWork, ILogger<AdminController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var model = new AdminViewModel();
                model.Rooms = _unitOfWork.RoomRepository.GetAll();
                model.Places = _unitOfWork.PlaceRepository.GetAll();
                model.Reservations = _unitOfWork.ReservationRepository.GetAll();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return View();
        }

        public IActionResult GetPartialView(int id)
        {
            if (id <= 0) return View("Index");

            var rooms = _unitOfWork.RoomRepository.GetAll();
            var places = _unitOfWork.PlaceRepository.GetAll();
            var reservations = _unitOfWork.ReservationRepository.GetAll();

            switch (id)
            {
                case 1: return PartialView("/Views/Shared/Tables/_roomsTable.cshtml", rooms);
                case 2: return PartialView("/Views/Shared/Tables/_placesTable.cshtml", places);
                case 3: return PartialView("/Views/Shared/Tables/_reservationsTable.cshtml", reservations);

                default:
                    return View("Index");
            }
        }

        public IActionResult Rooms()
        {
            var rooms = _unitOfWork.RoomRepository.GetAll();

            return View("_roomsTable", rooms);
        }

        public IActionResult Places()
        {
            var places = _unitOfWork.PlaceRepository.GetAll();

            return View("_placesTable", places);
        }

        public IActionResult Reservations()
        {
            var reservations = _unitOfWork.ReservationRepository.GetAll();

            return View("_reservationsTable", reservations);
        }
    }
}