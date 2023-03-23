using Client.Models;
using Client.User_Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
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
    public partial class MainWindow : Window
    {
        TcpClient client;
        Car car;
        public MainWindow()
        {
            InitializeComponent();
            client = new TcpClient("127.0.0.1" ,2702);
            
            var enumType = Enum.GetNames(typeof(HttpMethods));

            foreach (var item in enumType)
                cmboxCommandName.Items.Add(item);

            //for (int i = 0; i < 100; i++)
            //{
            //    UC_CarInfo carInfo = new();
            //    carInfo.Height = 200;
            //    carInfo.Width = 250;
            //    carInfo.Margin = new Thickness(10 , 10 ,10 ,10);
            //    CarListWrapPanel.Children.Add(carInfo);
            //}
        }
        private void cmboxCommandName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmboxCommandName.SelectedIndex == 0)
            {
                txtboxMake.Visibility = Visibility.Collapsed;
                txtboxModel.Visibility = Visibility.Collapsed;
                txtboxVinCod.Visibility = Visibility.Collapsed;
                txtboxYear.Visibility = Visibility.Collapsed;
                txtboxColor.Visibility = Visibility.Collapsed;
            }
            else if (cmboxCommandName.SelectedIndex == 1)
            {
                txtboxId.Visibility = Visibility.Visible;
                txtboxMake.Visibility = Visibility.Visible;
                txtboxModel.Visibility = Visibility.Visible;
                txtboxVinCod.Visibility = Visibility.Visible;
                txtboxYear.Visibility = Visibility.Visible;
                txtboxColor.Visibility = Visibility.Visible;
            }
            else if (cmboxCommandName.SelectedIndex == 2)
            {
                txtboxMake.Visibility = Visibility.Visible;
                txtboxModel.Visibility = Visibility.Visible;
                txtboxVinCod.Visibility = Visibility.Visible;
                txtboxYear.Visibility = Visibility.Visible;
                txtboxColor.Visibility = Visibility.Visible;
            }
            else if (cmboxCommandName.SelectedIndex == 3)
            {
                txtboxMake.Visibility = Visibility.Collapsed;
                txtboxModel.Visibility = Visibility.Collapsed;
                txtboxVinCod.Visibility = Visibility.Collapsed;
                txtboxYear.Visibility = Visibility.Collapsed;
                txtboxColor.Visibility = Visibility.Collapsed;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtboxMake.Text == null || txtboxModel.Text == null || txtboxVinCod.Text == null 
                || txtboxYear.Text == null || txtboxColor.Text == null)
            {
                MessageBox.Show("Wrong Data");
                return;
            }


            var serverStream = client.GetStream();

            var binaryRead = new BinaryReader(serverStream);
            var binaryWrite = new BinaryWriter(serverStream);



            if (cmboxCommandName.SelectedIndex == 0)
            {
                try
                {
                    car = new();
                    car.Id = Convert.ToInt32(txtboxId.Text);

                    Command? commandWrite = new()
                    {
                        Car = car,
                        Method = (HttpMethods)cmboxCommandName.SelectedIndex
                    };

                    var jsonStr = System.Text.Json.JsonSerializer.Serialize(commandWrite);
                    binaryWrite.Write(jsonStr);
                    await Task.Delay(100);
                    var response = binaryRead.ReadString();
                    var commandRead = System.Text.Json.JsonSerializer.Deserialize<Car>(response);

                    if (commandRead is null)
                        return;
    

                    UC_CarInfo carInfo = new();
                    carInfo.Height = 200;
                    carInfo.Width = 250;
                    carInfo.Margin = new Thickness(10, 10, 10, 10);
                    carInfo.txtBlockId.Text = commandRead.Id.ToString();
                    carInfo.txtBlockMake.Text = commandRead?.Make?.ToString();
                    carInfo.txtBlockModel.Text = commandRead?.Model?.ToString();
                    carInfo.txtBlockVIN.Text = commandRead?.VIN?.ToString();
                    carInfo.txtBlockYear.Text = commandRead.Year.ToString();
                    carInfo.txtBlockColor.Text = commandRead?.Color?.ToString();
                    CarListWrapPanel.Children.Add(carInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if (cmboxCommandName.SelectedIndex == 1)
            {

            }
            else if (cmboxCommandName.SelectedIndex == 2)
            {

            }
            else if (cmboxCommandName.SelectedIndex == 3)
            {

            }
 





        }

    }
}
