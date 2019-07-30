using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbAccessLayer;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer;

namespace ReedExpo.Pinger.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteController : Controller
    {
        private readonly IServiceSite _serviceSite;
        private readonly IServicePing _servicePing;
        private readonly ILogger<SiteController> _logger;

        public SiteController(ILogger<SiteController> logger, IServiceSite serviceSite, IServicePing servicePing)
        {
            this._logger = logger;
            this._serviceSite = serviceSite;
            this._servicePing = servicePing;
        }




        [HttpGet("[action]")]
        public async Task Create()
        {
            _serviceSite.CreateSite(new Site { Url = "localhost", PollingTime = 1, ExpectedTime = 1 });
            //await pingerContext.SaveChangesAsync();
        }

        [HttpGet("[action]")]
        public void Creates()
        {
            _serviceSite.CreateSite(new Site { Url = "localhost2", PollingTime = 2, ExpectedTime = 2 });
        }

        [HttpGet("[action]")]
        public void CreateAPing()
        {
            _servicePing.CreatePing(new Ping { UpTime = DateTime.Now, SiteId = 2 });
        }

        [HttpPost("[action]")]
        public void CreateUrl(string url, int pollingTime, int expectedTime)
        {
            _serviceSite.CreateSite(new Site { Url = url, PollingTime = pollingTime, ExpectedTime = expectedTime });
        }

        [HttpGet("[action]")]
        public IEnumerable<SiteDto> GetAllSites()
        {
            var siteDtos = _serviceSite.GetAllSites();
            return siteDtos;
        }

    }
}