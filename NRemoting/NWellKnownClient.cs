using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRemoting
{
    public class NWellKnownClient<T>
    {
        private string host;
        private int port;

        public NWellKnownClient(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public T GetClient()
        {
            string name = typeof(T).FullName;
            Uri uri = new Uri("tcp://" + host + ":" + port + "/" + name + ".rem");
            return (T)Activator.GetObject(typeof(T), uri.ToString());
        }
    }
}
