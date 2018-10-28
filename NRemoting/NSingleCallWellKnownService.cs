using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace NRemoting
{
    public class NSingleCallWellKnownService<T> : NWellKnownService<T> where T : MarshalByRefObject
    {
        public NSingleCallWellKnownService(int port)
            : base(port)
        {
        }

        protected override WellKnownObjectMode GetWellKnownObjectMode()
        {
            return WellKnownObjectMode.SingleCall;
        }
    }
}
