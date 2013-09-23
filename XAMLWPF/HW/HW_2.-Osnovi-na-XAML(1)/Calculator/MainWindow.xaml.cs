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

namespace Calculator
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

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            String input = btn.Content.ToString();

            switch (input)
            {
                case "0": Calc.CurrentInput += input; break;
                case "1": Calc.CurrentInput += input; break;
                case "2": Calc.CurrentInput += input; break;
                case "3": Calc.CurrentInput += input; break;
                case "4": Calc.CurrentInput += input; break;
                case "5": Calc.CurrentInput += input; break;
                case "6": Calc.CurrentInput += input; break;
                case "7": Calc.CurrentInput += input; break;
                case "8": Calc.CurrentInput += input; break;
                case "9": Calc.CurrentInput += input; break;
                case "±": Calc.ChageSign(); break;
                case "+": Calc.MakeAction(input); break;
                case "-": Calc.MakeAction(input); break;
                case "*": Calc.MakeAction(input); break;
                case "/": Calc.MakeAction(input); break;
                case "=": Calc.Result(); break;
            }

            Print();
        }

        private void Print()
        {
            History.Text = Calc.History;

            if (Calc.CurrentInput == String.Empty)
            {
                Output.Text = "0";
            }
            else
            {
                Output.Text = Calc.CurrentInput;
            }
        }
    }
}
