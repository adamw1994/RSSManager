using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(ManagerModel model)
        {
            model.EmailContent = "";
            model.SelectedSubscriptions = new List<int>();
            return View(model);
        }


        [HttpPost]
        public ActionResult SaveRSS(ManagerModel model)
        {
            var subscription = new Subscription
            {
                Email = model.Email,
                RSSlink = model.RSSLink
            };

            var repository = new RepositoryService();
            repository.SaveSubscription(subscription);
            return View("Index", model);
        }

        [HttpPost]
        public async Task<ActionResult> DownloadRSS(ManagerModel model)
        {
            var userEmail = model.Email;
            var repositoryService = new RepositoryService();
            var subscriptions = repositoryService.GetSubscription(userEmail);
            var rssService = new RSSService();
            var feedItems = new List<FeedItem>();
            foreach (var subscription in subscriptions)
            {
                var rssLink = subscription.RSSlink;

                feedItems.AddRange(await rssService.GetRSSFromUrl(rssLink));

            }
            string rssContent = "";
            foreach(var feedItem in feedItems)
            {
                rssContent += feedItem.Title + "\n" + feedItem.PublishDate + "\n" + "\n" + feedItem.Content + "\n" + "\n" + "\n";
            }
            model.EmailContent = rssContent;

            return View("Index", model);
        }
    }
}
