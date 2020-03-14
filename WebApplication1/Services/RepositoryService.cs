using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class RepositoryService
    {
        public bool SaveSubscription(Subscription subscription)
        {
            try
            {
                using (var context = new RSSContext())
                {
                    context.Subscriptions.Add(subscription);
                    context.SaveChanges();
                }
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveSubscription(Subscription subscription)
        {
            try
            {
                using (var context = new RSSContext())
                {
                    context.Remove(subscription);
                    context.SaveChanges();
                }
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Subscription> GetSubscription(String email)
        {
            try
            {
                using (var context = new RSSContext())
                {
                    return context.Subscriptions.Where(x => x.Email == email).ToList();
                }

            }

            catch (Exception ex)
            {

                return new List<Subscription>();
            }
        }
    }
}
