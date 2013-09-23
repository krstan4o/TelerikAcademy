using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AsyncAndAwaitHandlingErrors
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OpenFileButtonClick(object sender, RoutedEventArgs e)
        {
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            filePicker.FileTypeFilter.Add("*");

            try
            {
                var file = await filePicker.PickSingleFileAsync();
                FileInfo.Text = file.Name;
            }
            catch (Exception)
            {
                FileInfo.Text = "An error occured";
            }
        }

        private async void DownloadPostsButtonClick(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://posted.apphb.com/api/");

            try
            {
                var response = await client.GetAsync("posts");
                var responseText = await response.Content.ReadAsStringAsync();
                Posts.Text = responseText;
            }
            catch (HttpRequestException)
            {
                Posts.Text = "A network error occured";
            }
            catch (Exception)
            {
                Posts.Text = "An error occured";
            }
        }
    }
}
