using Kaenx.Konnect.Addresses;
using Kaenx.Konnect.Connections;
using Kaenx.Konnect.Messages.Request;
using Kaenx.Konnect.Messages.Response;

namespace KnxSimulator.Plugins.Default
{
    public class Interface : IDevice
    {
        public string Name { get { return "Interface"; } }
        public string Description { get { return "Einfache KNX Schnittstelle"; } }
        public int Number { get; set; }
        public LineMiddle Parent { get; set; }
        private KnxIpRouting _conn;

        public event OnTelegramHandler OnTelegramSend;
        
        public void TelegramReceived(Kaenx.Konnect.Messages.IMessage Telegram)
        {

        }

        public void Start(byte[] memory)
        {
            _conn = new KnxIpRouting();
            _conn.OnTunnelRequest += OnTunnelRequest;
            _conn.OnSearchRequest += _conn_OnSearchRequest;
        }

        private void _conn_OnSearchRequest(MsgSearchReq message)
        {
            System.Diagnostics.Debug.WriteLine($"SearchReq: {message.Endpoint.Address}:{message.Endpoint.Port} {message.Endpoint.AddressFamily}");
            //if (!message.Endpoint.Address.ToString().StartsWith("192.")) return;

            //return;
            KnxIpTunneling tunnel = new KnxIpTunneling(message.Endpoint, true);
            tunnel.Send(new MsgSearchRes() { FriendlyName = "KNX Virtual", Endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("192.168.178.221"), 8745) }, true);
        }

        private void OnTunnelRequest(Kaenx.Konnect.Messages.Request.IMessageRequest message)
        {

        }
    }
}