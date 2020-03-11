using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SaveRSS(ManagerModel model)
        {
            var subscription = new Subscription
            {
                Email = model.Email,
                RSSlink = model.RSSlink
            };

            var repository = new RepositoryService();
            repository.SaveSubscription(subscription);
            return View("Index");
        }
        [HttpPost]
        public ActionResult LoadRSS(ManagerModel model)
        {
            var repository = new RepositoryService();
            var rssList = repository.GetSubscription(model.Email);
            model.RSSSelectList = new SelectList(rssList, "Id", "RSSlink");
            return View("Index",model);
        }
    }
}
