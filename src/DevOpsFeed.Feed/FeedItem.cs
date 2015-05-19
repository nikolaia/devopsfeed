using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOpsFeed.Feed
{
    public struct FeedItem
    {
        public DateTimeOffset Time { get; set; }
        public string System { get; set; }
        public string Message { get; set; }
    }
}
