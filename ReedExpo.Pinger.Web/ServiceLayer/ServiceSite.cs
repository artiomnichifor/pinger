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
        private PingerContext _context;
        private readonly ILogger<ServiceSite> _logger;
        //private readonly IDbControlService _dbControlServise;
        //IList<SiteDto> siteDtos;
        //private static bool newSiteTrigger = true;

        public ServiceSite(ILogger<ServiceSite> logger, PingerContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        public void CreateSite(Site site)
        {
            _context.Sites.Add(site);
            _context.SaveChanges();
            //newSiteTrigger = true;

        }

        public void EditSite(Site siteModel, int id)
        {
            var site = _context.Sites.SingleOrDefault(s => s.Id == id);

            site.Url = siteModel.Url;
            site.PollingTime = siteModel.PollingTime;
            site.ExpectedTime = siteModel.ExpectedTime;
            site.LastCheckedTime = siteModel.LastCheckedTime;

            _context.Update<Site>(site);
            _context.SaveChanges();
        }

        public void DeleteSite(Site site)
        {
            _context.Sites.Remove(site);
            _context.SaveChanges();
        }

        public Site GetSite(long id)
        {
            var result = _context.Sites.SingleOrDefault(s => s.Id == id);
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

            var sites = from s in _context.Sites.ToList()
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

        public IList<FullSiteDto> GetAllFullSites()
        {
            var sites = from s in _context.Sites.ToList()
                        select new FullSiteDto()
                        {
                            Id = s.Id,
                            Url = s.Url,
                            UpTime = (from p in _context.Pings.ToList()
                                      where s.Id == p.SiteId
                                      select p).Count() / 
                                                (from p in _context.Pings.ToList()
                                                 where s.Id == p.SiteId &&
                                                 p.Status == "success"
                                                 select p).Count() * 100,
                            AverageRequestTime = 
                                    (from p in _context.Pings.ToList()
                                     where s.Id == p.SiteId
                                     select p.ResponseTime).Average()

                            
                        };

            return sites.ToList();
        }


    }
}
