using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnxSimulator.Plugins
{
    public class LineMain : ILine
    {
        public int Number { get; set; }

        public ObservableCollection<LineMiddle> Lines { get; } = new ObservableCollection<LineMiddle>();
    }
}