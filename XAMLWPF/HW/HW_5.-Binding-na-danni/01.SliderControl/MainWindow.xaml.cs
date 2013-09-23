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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _01.SliderControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToSmall(object sender, RoutedEventArgs e)
        {
            this.FontSizeSlider.Value = 10;
        }

        private void ToMiddle(object sender, RoutedEventArgs e)
        {
            this.FontSizeSlider.Value = 50;
        }

        private void ToLarge(object sender, RoutedEventArgs e)
        {
            this.FontSizeSlider.Value = 90;
        }
    }
}