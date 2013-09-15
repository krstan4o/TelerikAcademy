using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public Calculator calculator = new Calculator();
        //private static bool isReadyForResult = false;
        //private string lastOperation = "None";
        bool isReadyForResult = false;
        bool isFirstNumber = true;
        Queue<string> operations;
        Queue<decimal> parameters;

        public MainWindow()
        {
            InitializeComponent();
            ResultTextBox.DataContext = calculator;
            OperationsTextBox.DataContext = calculator;
            this.operations = new Queue<string>();
            this.parameters = new Queue<decimal>();
        }

        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            switch (clickedButton.Content.ToString())
            {
                case "0":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "0";
                        isFirstNumber = true;
                    }
                    else
                    {
                        this.calculator.Result += "0";
                    }
                    break;
                case "1":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "1";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "1";
                    }
                    break;
                case "2":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "2";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "2";
                    }
                    break;
                case "3":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "3";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "3";
                    }
                    break;
                case "4":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "4";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "4";
                    }
                    break;
                case "5":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "5";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "5";
                    }
                    break;
                case "6":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "6";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "6";
                    }
                    break;
                case "7":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "7";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "7";
                    }
                    break;
                case "8":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "8";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "8";
                    }
                    break;
                case "9":
                    if (isFirstNumber)
                    {
                        this.calculator.Result = "9";
                        isFirstNumber = false;
                    }
                    else
                    {
                        this.calculator.Result += "9";
                    }
                    break;
                default:
                    MessageBox.Show("Not implemented");
                    break;
            }
        }

        private void OperationButtonClick(object sender, RoutedEventArgs e) 
        {
            var button = sender as Button;
            if (operations.Count != 2)
            {
                operations.Enqueue(button.Content.ToString());
                parameters.Enqueue(decimal.Parse(this.calculator.Result));
            }
            else
            {
                string operationCommand = operations.Dequeue();
                ExecuteOperationCommand(operationCommand);
            }
        }

        private void ExecuteOperationCommand(string operationCommand)
        {
            switch (operationCommand)
            {
                case "+":
                    break;
                default:
                    break;
            }
        }

        private void Sum()
        {
            decimal firstParam = parameters.Dequeue();
            decimal secondParam = parameters.Dequeue();
            operations.Dequeue();
            decimal result = firstParam + secondParam;
            parameters.Enqueue(result);
        }
        //private void ShowInfo(object sender, RoutedEventArgs e) 
        //{
        //    MessageBox.Show("&copy 2013 krstan4o");
        //}
    }

    public class Calculator : INotifyPropertyChanged
    {
        private string result = "0";
        private StringBuilder operations;
       
        public string Result 
        {
            get
            {
                return this.result;
            }
            set 
            {
                if (value != this.result)
                {
                    this.result = value;
                    Notify("Result");
                }
            }
        }

        public string Operations 
        {
            get
            {
                return this.operations.ToString();
            }
            set 
            {
                if (value == "Clear")
                {
                    this.operations.Clear();
                }
                else
                {
                    this.operations.Append(value);
                }
                Notify("Operations");
            }
        }

        public decimal TempResult { get; set; }

        public Calculator()
        {
            this.Result = "0";
            this.TempResult = 0;
            this.operations = new StringBuilder();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string result)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(result));
            }
        }
    }
}

