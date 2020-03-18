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
        public IActionResult Index(ManagerModel model)
        {
            model.EmailContent = "";
            model.SelectedSubscriptions = new List<int>();
            model.Subscriptions = new SelectList(new List<Subscription>(), "Id", "RSSlink");
            return View(model);
        }


        [HttpPost]
        public ActionResult SaveRSS(ManagerModel model)
        {
            Subscription subscription = new Subscription
            {
                Email = model.Email,
                RSSlink = model.RSSLink
            };

            RepositoryService repositoryService = new RepositoryService();
            repositoryService.SaveSubscription(subscription);
            model.SelectedSubscriptions = new List<int>();
            model.Subscriptions = new SelectList(new List<Subscription>(), "Id", "RSSlink");
            return View("Index", model);
        }
        [HttpPost]
        public ActionResult DeleteRSSLink(ManagerModel model)
        {
            RepositoryService repositoryService = new RepositoryService();
            foreach (int id in model.SelectedSubscriptions)
            {
                Subscription subscription = repositoryService.GetSubscription(id);
                repositoryService.RemoveSubscription(subscription);
            }
            var userEmail = model.Email;
            List<Subscription> subscriptions = repositoryService.GetSubscriptions(userEmail);
            model.Subscriptions = new SelectList(subscriptions, "Id", "RSSlink");
            return View("Index", model);
        }

        [HttpPost]
        public async Task<ActionResult> DownloadRSS(ManagerModel model)
        {
            var userEmail = model.Email;
            var repositoryService = new RepositoryService();
            var subscriptions = repositoryService.GetSubscriptions(userEmail);
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
            model.SelectedSubscriptions = new List<int>();
            model.Subscriptions = new SelectList(subscriptions, "Id", "RSSlink");
            return View("Index", model);
        }
    }
}
