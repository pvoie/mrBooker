using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MRBooker.Data;
using MRBooker.Wrappers;
using MRBooker.Data.Models.Entities;
using Microsoft.Extensions.Logging;
using MRBooker.Data.ReservationViewModels;
using System.Collections.Generic;
using MRBooker.Data.SchedulerModels;

namespace MRBooker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationUserManager<ApplicationUser> userManager,
            ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new ReservationViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.GetUserWithDataByName(User.Identity.Name);
                model.Reservations = new List<string>();

                foreach (var item in user.Reservations)
                {
                    var schedulerEvent = new SchedulerEventModel
                    {
                        Id = item.Id,
                        Description = item.Description,
                        StartDate = item.Start,
                        EndDate = item.End
                    };

                    model.Reservations.Add(schedulerEvent.ToJson());
                }
                model.JsonList = model.ToJsonList();
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
