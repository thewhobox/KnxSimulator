using Kaenx.Konnect.Connections;

namespace KnxSimulator.Plugins.Default
{
    public class Interface : IDevice
    {
        public string Name { get { return "Interface"; } }
        public string Description { get { return "Einfache KNX Schnittstelle"; } }
        public int Number { get; set; }
        public LineMiddle Parent { get; set; }
        private Kaenx.Konnect.Connections.KnxIpRouting _conn;

        public event OnTelegramHandler OnTelegramSend;
        
        public void TelegramReceived(Kaenx.Konnect.Messages.IMessage Telegram)
        {

        }

        public void Start(byte[] memory)
        {
            _conn = new KnxIpRouting();
            _conn.OnTunnelRequest += OnTunnelRequest;
            _conn.Connect();
        }

        private void OnTunnelRequest(Kaenx.Konnect.Messages.Request.IMessageRequest message)
        {

        }
    }
}