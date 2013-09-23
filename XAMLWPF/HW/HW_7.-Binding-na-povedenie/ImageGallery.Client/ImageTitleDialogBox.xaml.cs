using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ImageGallery.Client
{
    /// <summary>
    /// Interaction logic for ImageTitleDialogBox.xaml
    /// </summary>
    public partial class ImageTitleDialogBox : Window
    {
        public ImageTitleDialogBox()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            // Accept the dialog and return the dialog result 
            this.DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            // Invalidate the dialog and return the dialog result 
            this.DialogResult = false;
        }
    }
}
