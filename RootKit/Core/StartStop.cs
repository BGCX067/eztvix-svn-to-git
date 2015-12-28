using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace RootKit.Core
{
    public class StartStop : Stopwatch
    {
        public long Seconds
        {
            get
            {
                return this.ElapsedMilliseconds / 1000;
            }
        }
        
        public long Milliseconds
        {
            get
            {
                return this.ElapsedMilliseconds - (this.Seconds * 1000);
            }
        }

    }
}
