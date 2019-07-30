using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReedExpo.Pinger.Web.Models
{
    public class SiteModel
    {
        public string Url { get; set; }
        public int ExpectedTime { get; set; }
        public int PollingTime { get; set; }
    }
}
