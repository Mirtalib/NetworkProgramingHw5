using Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient client;
        FileStream fileStream;

        public MainWindow()
        {
            InitializeComponent();

            var ipString = IPAddress.Parse("127.0.0.1");
            var port = 27001;
            IPEndPoint ep = new(ipString, port);
            // fileStream = new FileStream("data.json", FileMode.Create, FileAccess.Write);
            client = new TcpClient(ep);

            cmboxCommandName.Items.Add(HttpMethods.GET.ToString());
            cmboxCommandName.Items.Add(HttpMethods.POST.ToString());
            cmboxCommandName.Items.Add(HttpMethods.PUT.ToString());
            cmboxCommandName.Items.Add(HttpMethods.DELETE.ToString());
        }
        private void cmboxCommandName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmboxCommandName.SelectedIndex == 0)
            {
                txtboxMake.Visibility = Visibility.Collapsed;
                txtboxModel.Visibility = Visibility.Collapsed;
                txtboxVinCod.Visibility = Visibility.Collapsed;
                txtboxYear.Visibility = Visibility.Collapsed;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Car car = new();
            var serverStream = client.GetStream();
            fileStream = new FileStream("data.json", FileMode.Open, FileAccess.Write);

            

            

            
            //fileStream.Flush();
            //serverStream.CopyTo(fileStream);


        }

    }
}
