using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using Microsoft.Win32;
using Spring.IO;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.Social.OAuth1;

namespace _3.DropboxUploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DropboxAppKey = "6mxv4f298za9eer";
        private const string DropboxAppSecret = "fdv4dmrc56ulx59";
        private IDropbox dropbox;
        private DropboxServiceProvider dropboxServiceProvider;

        public MainWindow()
        {
            InitializeComponent();

            dropboxServiceProvider =
            new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);
        }

        private static OAuthToken LoadOAuthToken()
	    {
		    string[] lines = File.ReadAllLines("../../token.txt");
		    OAuthToken oauthAccessToken = new OAuthToken(lines[0], lines[1]);
		    return oauthAccessToken;
	    }

        private void SendFile(string filePath)
        {
            OAuthToken oauthAccessToken = LoadOAuthToken();

            dropbox = dropboxServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);
            
            string newFolderName = "New_Folder_" + DateTime.Now.Ticks;
         
            Entry uploadFileEntry = dropbox.UploadFileAsync(
                new FileResource(filePath),
                "/" + newFolderName + "/" + new FileInfo(filePath).Name).Result;
            
            DropboxLink sharedUrl = dropbox.GetShareableLinkAsync(uploadFileEntry.Path).Result;
            Process.Start(sharedUrl.Url);

            MessageBox.Show("File sent successfully!");
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if(dlg.ShowDialog() == true)
            {
                this.txtFilePath.Text = dlg.FileName;
            }
        }

        private void btnSendFile_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtFilePath.Text)
                && !string.IsNullOrWhiteSpace(this.txtFilePath.Text))
            {
                try
                {
                    SendFile(this.txtFilePath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
