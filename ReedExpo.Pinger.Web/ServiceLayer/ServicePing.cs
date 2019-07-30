using DbAccessLayer;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer
{
    public class ServicePing : IServicePing
    //IDisposable
    {
        private PingerContext context;
        private readonly ILogger<ServicePing> logger;
        public ServicePing(ILogger<ServicePing> logger, PingerContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public void CreatePing(Ping ping)
        {
            context.Pings.Add(ping);
            context.SaveChanges();
        }

        public void EditPing(Ping pingModel, int id)
        {
            var ping = context.Pings.SingleOrDefault(p => p.Id == id);

            ping.UpTime = pingModel.UpTime;

            context.Update<Ping>(ping);
            context.SaveChanges();
        }

        public void DeletePing(Ping ping)
        {
            context.Pings.Remove(ping);
            context.SaveChanges();
        }

        public Ping GetPing(long id)
        {
            var result = context.Pings.SingleOrDefault(p => p.Id == id);
            return result;
        }



    }
}
