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
using IronMQ;
using IronMQ.Data;

namespace _1.ChatMessageQueueGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string token = "blQMDOtjkW1vi5ZejX9Bp4XjZlE";
        private const string projectId = "520dd5b77ab0310005000003";
        private Client client;
        private Queue queue;

        public MainWindow()
        {
            InitializeComponent();

            client = new Client(projectId, token);
            queue = new Queue(client, "ChatQueue");


            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (sender, ev) =>
            {
                while (true)
                {
                    Message msg = queue.Get();
                    if (msg != null)
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.txtChatHistory.Text += "Message Received: " + msg.Body + "\n";
                        }));

                        //queue.DeleteMessage(msg);
                    }

                    Thread.Sleep(100);
                }
            };

            worker.RunWorkerAsync();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string messageContent = this.txtMessage.Text;
            if(!string.IsNullOrEmpty(messageContent))
            {
                queue.Push(messageContent);
                this.txtMessage.Text = "";
            }
        }
    }
}
