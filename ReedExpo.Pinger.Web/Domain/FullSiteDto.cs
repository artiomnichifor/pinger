using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FullSiteDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public double PollingTime { get; set; }
        public double AverageRequestTime { get; set; }
        //???little bars or whatever
        //public ICollection<Ping> Pings { get; set; }
    }
}
