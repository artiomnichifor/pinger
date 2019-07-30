﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Site
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ExpectedTime { get; set; }
        //TimeSpan
        public int PollingTime { get; set; }
         

        public Site()
        {

        }

        public virtual ICollection<Ping> Pings { get; set; }

    }
}
