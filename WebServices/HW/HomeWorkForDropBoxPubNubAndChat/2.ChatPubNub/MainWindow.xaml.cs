using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace _2.ChatPubNub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PUBLISH_KEY = "pub-c-58b4cd72-3acd-4e76-aa28-59e993e23287";
        private const string SUBSCRIBE_KEY = "sub-c-923ac69e-064c-11e3-a5e8-02ee2ddab7fe";
        private const string SECRET_KEY = "sec-c-OTM2YTcyMzgtMzgwYS00ZmExLWFmMzItM2Y1YTZlOGNhYWY1";
        private PubnubAPI pubnub;

        private const string channel = "chat-channel";

        public MainWindow()
        {
            InitializeComponent();

            pubnub = new PubnubAPI(PUBLISH_KEY, SUBSCRIBE_KEY, SECRET_KEY, true);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (sender, ev) => pubnub.Subscribe(
                channel,
                delegate(object message)
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                                                              {
                                                                  this.txtChatHistory.Text +=
                                                                      "Message Received: " +
                                                                      message + "\n";
                                                              }));
                        return true;
                    }
                                                 );

            worker.RunWorkerAsync();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string messageContent = this.txtMessage.Text;
            if (!string.IsNullOrEmpty(messageContent))
            {
                pubnub.Publish(channel, messageContent);
                this.txtMessage.Text = "";
            }
        }
    }
}
