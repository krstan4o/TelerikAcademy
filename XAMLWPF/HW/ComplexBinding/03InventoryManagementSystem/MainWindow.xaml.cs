using System;
using System.Linq;
using InventoryViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _03InventoryManagementSystem
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

        private void OnGetProductButtonClick(object sender, RoutedEventArgs e)
        {
            string productIdStr=ProductId.Text;
            if (productIdStr!="")
            {
                int productId = int.Parse(productIdStr);
                var product = Persister.GetProductById(productId);
                if (product==null)
                {
                    MessageBox.Show(String.Format("No product with Id {0} was found in the inventory`s database!", productId));
                }
                this.DataContext = product;
            }
            else
            {
                MessageBox.Show("No product id was entered!");
            }
        }
    }
}
