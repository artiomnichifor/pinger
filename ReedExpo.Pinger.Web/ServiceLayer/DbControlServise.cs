using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class DbControlServise : IDbControlService
    {
        private bool trigger = true;
        public bool Trigger { get { return trigger; } set { trigger = value; } }
    }
}
