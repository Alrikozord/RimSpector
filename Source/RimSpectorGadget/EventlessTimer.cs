using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RimSpectorMod
{
    internal class EventlessTimer : Timer
    {
        public bool IsElapsed { get; private set; }

        public EventlessTimer(double interval) : base(interval)
        {
            AutoReset = false;
            Elapsed += (_, e) => IsElapsed = true;
        }

        public void Reset()
        {
            IsElapsed = false;
            Enabled = true;
        }

    }
}
