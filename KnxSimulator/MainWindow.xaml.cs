using KnxSimulator.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace KnxSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Plugins.IDevice> DeviceCatalog = new List<Plugins.IDevice>();
        ObservableCollection<LineMain> Lines = new ObservableCollection<LineMain>();
        private Dictionary<string, Type> Types = new Dictionary<string, Type>();


        public MainWindow()
        {
            InitializeComponent();


            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            LoadDlls(Path.Combine(basePath, "KnxSimulator.Plugins.Default.dll"));

            if(!Directory.Exists("Plugins"))
                Directory.CreateDirectory("Plugins");

            basePath = Path.Combine(basePath, "Plugins");
            foreach(string file in Directory.GetFiles("Plugins"))
            {
                LoadDlls(Path.Combine(basePath, file));
            }

            ListCatalog.ItemsSource = DeviceCatalog;
            TreeLines.ItemsSource = Lines;

            Lines.Add(new LineMain() { Number = 1 });
        }


        private void LoadDlls(string path)
        {
            Assembly assembly = Assembly.LoadFile(path);

            List<string> temp = new List<string>();
            var q = from t in assembly.GetTypes()
                    where t.IsClass && t.IsNested == false
                    select t;

            foreach (Type t in q.ToList())
            {
                TypeInfo info = t.GetTypeInfo();

                if(info.ImplementedInterfaces.Contains(typeof(IDevice)))
                {
                    IDevice device = (IDevice)Activator.CreateInstance(t);
                    DeviceCatalog.Add(device);
                    Types.Add(device.Name, t);
                }
            }
        }

        private void ClickAddLine(object sender, RoutedEventArgs e)
        {
            LineMain line = (sender as MenuItem).DataContext as LineMain;
            line.Lines.Add(new LineMiddle() { Parent = line });
        }

        private void ClickAddDevice(object sender, RoutedEventArgs e)
        {
            if(ListCatalog.SelectedItem == null) return;

            LineMiddle line = (sender as MenuItem).DataContext as LineMiddle;

            Type t = Types[(ListCatalog.SelectedItem as IDevice).Name];
            IDevice dev = (IDevice)Activator.CreateInstance(t);
            dev.Parent = line;
            line.Devices.Add(dev);
            dev.Start(new byte[0]);
        }
    }
}
