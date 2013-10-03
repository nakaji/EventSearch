﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EventData;

namespace EventCollector.WebSvc
{
    public class AllEventCollector : BaseEventCollector
    {
        private List<BaseEventCollector> _collectors = new List<BaseEventCollector>();

        public AllEventCollector()
        {
            _collectors.Add(new AtndEventCollector());
            _collectors.Add(new DoorkeeperEventCollector());
        }

        public AllEventCollector(IEnumerable<BaseEventCollector> collectors)
        {
            _collectors.AddRange( collectors);
        }

        public override IList<CommonEvent> GetEvents(int ym, string keyword)
        {
            var events = new List<CommonEvent>();

            _collectors.AsParallel().ForAll(x=>events.AddRange(x.GetEvents(ym, keyword)));

            events.Sort();
            return events;
        }
    }
}
