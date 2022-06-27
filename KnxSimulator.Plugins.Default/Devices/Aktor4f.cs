using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaenx.Konnect.Messages;
using KnxSimulator.Plugins;

namespace KnxSimulator.Plugins.Default
{
    public class Aktor4f : IDevice
    {
        public string Name { get { return "Aktor 4-fach"; } }
        public string Description { get { return "Einfacher Aktor mit 4 Kanälen"; } }
        public int Number { get; set; }
        public LineMiddle Parent { get; set; }

        public event OnTelegramHandler OnTelegramSend;

        public void TelegramReceived(Kaenx.Konnect.Messages.IMessage Telegram)
        {
            
        }

        public void Start(byte[] memory)
        {
            OnTelegramSend?.Invoke(this, new Kaenx.Konnect.Messages.Request.MsgIndividualAddressReadReq());
        }
    }
}