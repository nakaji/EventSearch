using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventData;

namespace EventCollector.WebSvc
{
    public class BaseEventCollector
    {
        public int ReadCount { get; protected set; }

        protected BaseEventCollector()
        {
            ReadCount = 25;
        }

        public virtual IList<CommonEvent> GetEvents(int ym, string keyword)
        {
            throw new RowNotInTableException();
        }

    }
}
