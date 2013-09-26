using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventData;

namespace EventCollector.WebSvc
{
    public class EventCollector
    {
        public int ReadCount { get; protected set; }

        protected EventCollector()
        {
            ReadCount = 25;
        }

        public virtual IList<CommonEvent> GetEvents(int ym, string keyword)
        {
            throw new RowNotInTableException();
        }

    }
}
