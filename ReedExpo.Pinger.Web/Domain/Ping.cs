using System;

namespace Domain
{
    public class Ping
    {
        public int Id { get; set; }
        //TimeSpan
        public DateTime UpTime { get; set; }

        public Ping()
        {

        }

        public int SiteId { get; set; }

        public virtual Site Site { get; set; }
    }
}
