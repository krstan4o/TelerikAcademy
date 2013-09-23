using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    public static class Calc
    {
        private static string currentInput = null;
        private static string currentInputSign = null;
        private static double previousResult = 0;
        private static bool startNewInput = false;
        public static string History = null;
        private static string lastAction = null;

        public static string CurrentInput
        {
            get
            {
                return currentInputSign + currentInput;
            }
            set
            {
                if (startNewInput)
                {
                    currentInput = null;
                    startNewInput = false;
                    value = value[value.Length - 1].ToString();
                }

                if (!(currentInput == null && value == "0"))
                {
                    currentInput = value;
                }
            }
        }

        public static void ChageSign()
        {
            if (currentInput != null)
            {
                if (currentInputSign == null)
                {
                    currentInputSign = "-";
                }
                else
                {
                    currentInputSign = null;
                }
            }
        }

        public static void MakeAction(string action)
        {
            History += CurrentInput + " " + action + " ";
            startNewInput = true;
            lastAction = action;

            if (previousResult == 0)
            {
                previousResult = Convert.ToDouble(CurrentInput);
                return;
            }

            switch (action)
            {
                case "+": previousResult += Convert.ToDouble(CurrentInput); break;
                case "-": previousResult -= Convert.ToDouble(CurrentInput); break;
                case "/": previousResult /= Convert.ToDouble(CurrentInput); break;
                case "*": previousResult *= Convert.ToDouble(CurrentInput); break;
            }
        }

        public static void Result()
        {
            MakeAction(lastAction);
            History = null;
            currentInput = previousResult.ToString();
            previousResult = 0;
            startNewInput = true;
            lastAction = null;
        }
    }
}
