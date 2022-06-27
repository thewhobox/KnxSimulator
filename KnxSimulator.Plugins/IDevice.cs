using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnxSimulator.Plugins
{
    public delegate void OnTelegramHandler(IDevice sender, Kaenx.Konnect.Messages.IMessage telegram);


    public interface IDevice
    {
        string Name { get; }
        string Description { get; }
        int Number { get; set; }
        LineMiddle Parent { get; set; }

        event OnTelegramHandler OnTelegramSend;
        void TelegramReceived(Kaenx.Konnect.Messages.IMessage Telegram);

        void Start(byte[] memory);
    }
}