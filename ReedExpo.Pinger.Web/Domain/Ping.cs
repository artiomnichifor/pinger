using System;

namespace Domain
{
    public class Ping
    {
        public int Id { get; set; }
        public double ResponseTime { get; set; }
        public string Status { get; set; }

        public Ping()
        {

        }

        public int SiteId { get; set; }

        public virtual Site Site { get; set; }
    }
}
