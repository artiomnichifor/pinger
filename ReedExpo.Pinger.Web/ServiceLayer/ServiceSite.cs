using DbAccessLayer;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer
{
    public class ServiceSite : IServiceSite //IDisposable
    {
        private PingerContext context;
        private readonly ILogger<ServiceSite> logger;
        IList<SiteDto> siteDtos;
        private readonly IDbControlService _dbControlServise;
        //private static bool newSiteTrigger = true;

        public ServiceSite(ILogger<ServiceSite> logger, PingerContext context, IDbControlService dbControlService)
        {
            this.logger = logger;
            this.context = context;
            this._dbControlServise = dbControlService;
        }

        public void CreateSite(Site site)
        {
            context.Sites.Add(site);
            context.SaveChanges();
            _dbControlServise.Trigger = true;

        }

        public void EditSite(Site siteModel, int id)
        {
            var site = context.Sites.SingleOrDefault(s => s.Id == id);

            site.Url = siteModel.Url;
            site.PollingTime = siteModel.PollingTime;
            site.ExpectedTime = siteModel.ExpectedTime;
            site.LastCheckedTime = siteModel.LastCheckedTime;

            context.Update<Site>(site);
            context.SaveChanges();
        }

        public void DeleteSite(Site site)
        {
            context.Sites.Remove(site);
            context.SaveChanges();
        }

        public Site GetSite(long id)
        {
            var result = context.Sites.SingleOrDefault(s => s.Id == id);
            return result;
        }


        public IList<SiteDto> GetAllSites()
        {
            //if (newSiteTrigger || this.siteDtos == null)
            //{
            //    var sites = from s in context.Sites.ToList()
            //                select new SiteDto()
            //                {
            //                    Id = s.Id,
            //                    Url = s.Url,
            //                    PollingTime = s.PollingTime,
            //                    ExpectedTime = s.ExpectedTime,
            //                    LastTimeChecked = s.LastCheckedTime
            //                };
            //    this.siteDtos = sites.ToList();
            //    newSiteTrigger = false;
            //}



            //return siteDtos;

            var sites = from s in context.Sites.ToList()
                        select new SiteDto()
                        {
                            Id = s.Id,
                            Url = s.Url,
                            PollingTime = s.PollingTime,
                            ExpectedTime = s.ExpectedTime,
                            LastTimeChecked = s.LastCheckedTime
                        };


            return sites.ToList();
        }


    }
}
