using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOpsFeed.Status
{
    public class Status
    {
        public DateTimeOffset LastChecked { get; set; }
        public string System { get; set; }
        public bool Healthy { get; set; }
    }
}
