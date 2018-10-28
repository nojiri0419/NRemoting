using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace NRemoting
{
    public abstract class NWellKnownService<T> where T : MarshalByRefObject
    {
        private int port;
        private IChannel channel;

        protected abstract WellKnownObjectMode GetWellKnownObjectMode();

        public NWellKnownService(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            string name = typeof(T).FullName;

            System.Collections.IDictionary props = new System.Collections.Hashtable();
            props["port"] = port;
            props["name"] = name;

            IClientChannelSinkProvider clientChannelSinkProvider = null;

            BinaryServerFormatterSinkProvider serverChannelSinkProvider = new BinaryServerFormatterSinkProvider();
            serverChannelSinkProvider.TypeFilterLevel = TypeFilterLevel.Full;

            TcpChannel tcpChannel = new TcpChannel(props, clientChannelSinkProvider, serverChannelSinkProvider);
            ChannelServices.RegisterChannel(tcpChannel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(T), name + ".rem", GetWellKnownObjectMode());

            channel = tcpChannel;
        }

        public void Stop()
        {
            if (channel != null)
            {
                ChannelServices.UnregisterChannel(channel);
            }
        }
    }
}
