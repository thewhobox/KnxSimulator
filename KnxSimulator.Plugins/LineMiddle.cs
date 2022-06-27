using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnxSimulator.Plugins
{
    public class LineMiddle : ILine
    {
        public int Number { get; set; }

        public LineMain Parent { get; set; }
        
        public ObservableCollection<IDevice> Devices { get; } = new ObservableCollection<IDevice>();

        public LineMiddle()
        {
            Devices.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach(IDevice dev in e.NewItems)
                {
                    dev.OnTelegramSend += SendTelegram;
                }
            }
        }

        private void SendTelegram(IDevice sender, Kaenx.Konnect.Messages.IMessage Telegram)
        {
            System.Diagnostics.Debug.WriteLine($"Sending Telegram from: {sender.Parent.Parent.Number}.{sender.Parent.Number}.{sender.Number} {sender.Name}");

            foreach(IDevice dev in Devices)
            {
                if(dev != sender)
                    dev.TelegramReceived(Telegram);
            }
        }
    }
}