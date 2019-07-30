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
    //[Route("[controller]")]
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
            _serviceSite.CreateSite(new Site { Url = "localhost", RollingTime = "1" });
            //await pingerContext.SaveChangesAsync();
        }

        [HttpGet("[action]")]
        public void Creates()
        {
            _serviceSite.CreateSite(new Site { Url = "localhost2", RollingTime = "2" });
        }

        [HttpGet("[action]")]
        public void CreateAPing()
        {
            _servicePing.CreatePing(new Ping { UpTime = DateTime.Now, SiteId = 2 });
        }

    }
}