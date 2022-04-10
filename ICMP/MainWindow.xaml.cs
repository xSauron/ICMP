using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
using MahApps.Metro.Controls;

namespace ICMP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Ping(IPAddress IP, string Message)
        {
            Ping pingSender = new Ping();

            // Create a buffer of X bytes of data to be transmitted.
            byte[] buffer = Encoding.ASCII.GetBytes(Message);

            // Wait 10 seconds for a reply.
            int timeout = 10000;

            PingOptions options = new PingOptions(64, true);

            // Send the request.
            PingReply reply = pingSender.Send(IP, timeout, buffer, options);

            if (reply.Status == IPStatus.Success)
            {
                Status_Label.Content = string.Join(", ", new string[] { "Replied: " + reply.Address.ToString(), "Time: " + reply.RoundtripTime.ToString(), "TTL: " + reply.Options?.Ttl.ToString(), "\nData: " + reply.Buffer.Length.ToString() + " bytes"});
            }
            else
            {
                Console.WriteLine(reply.Status);
            }
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!IPAddress.TryParse(this.IPv4_TextBox.Text, out var address))
            {
                try
                {
                    address = Dns.GetHostEntry(this.IPv4_TextBox.Text).AddressList.First();
                }
                catch(System.Exception ex)
                {
                    Status_Label.Content = ex.Message;
                    return;
                }
                
            }
            Ping(address, this.Message_TextBox.Text);
        }
        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            // Launch the GitHub site
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/k start https://github.com/xSauron/ICMP && exit",
                UseShellExecute = true
            });
        }
    }
}
