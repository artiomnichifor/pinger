using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Site
    {
        public int Id { get; set; }
        public string Url { get; set; }
        //TimeSpan
        public string RollingTime { get; set; }


        public Site()
        {

        }

        public virtual ICollection<Ping> Pings { get; set; }

    }
}
