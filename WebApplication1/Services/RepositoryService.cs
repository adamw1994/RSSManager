﻿using System;
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

        public Subscription GetSubscription(int id)
        {
            try
            {
                using (var context = new RSSContext())
                {
                    Subscription a = context.Subscriptions.Single(x => x.Id == id);
                    return a;
                }
                
            }

            catch (Exception ex)
            {
                return null;
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
        public List<Subscription> GetSubscriptions(String email)
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

                return null;
            }
        }
    }
}
