using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        double numberA = 0;
        double numberB = 0;
        char operation = '0';

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HistorySplitView.IsPaneOpen = !HistorySplitView.IsPaneOpen;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextBlockNumber.Text = "";
            TextBlockExpression.Text = "";
            numberA = 0;
            numberB = 0;
            operation = '0';
        }

        private void AddNumber(char symbol)
        {
            TextBlockNumber.Text += symbol; 
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlockNumber.Text != "") 
            {
                TextBlockNumber.Text = TextBlockNumber.Text.Remove(TextBlockNumber.Text.Length -1,  1);
            }
        }

        private void ButtonPosOrNeg_Click(object sender, RoutedEventArgs e)
        {

            if (TextBlockNumber.Text == "")
            {
                AddNumber('-');
            }
            else if(TextBlockNumber.Text[0] != '-') 
            {
                TextBlockNumber.Text = TextBlockNumber.Text.Insert(0, "-");
            }
            else if (TextBlockNumber.Text[0] == '-')
            {
                TextBlockNumber.Text = TextBlockNumber.Text.Replace("-", "");
            }
        }

        private void ButtonPoint_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlockNumber.Text != "" && TextBlockNumber.Text != "-" && !TextBlockNumber.Text.Contains(".")) 
            {
                AddNumber('.');
            }
        }

        #region Numbers Click
        private void ButtonZero_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('0');
        }

        private void ButtonNine_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('9');
        }

        private void ButtonEight_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('8');
        }

        private void ButtonSeven_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('7');
        }

        private void ButtonSix_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('6');
        }

        private void ButtonFive_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('5');
        }

        private void ButtonFour_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('4');
        }

        private void ButtonThree_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('3');
        }

        private void ButtonTwo_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('2');
        }

        private void ButtonOne_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('1');
        }
        #endregion

        public void Calculating(char symbol)
        {
            if (numberA == 0 && TextBlockNumber.Text != "" && TextBlockNumber.Text != "-")
            {
                operation = symbol;
                numberA = Double.Parse(TextBlockNumber.Text);
                TextBlockExpression.Text = numberA + operation.ToString();
                TextBlockNumber.Text = "";
            }
            else if (numberA != 0 && TextBlockNumber.Text == "" && TextBlockNumber.Text != "-")
            {
                operation = symbol;
                TextBlockExpression.Text = numberA + operation.ToString();
            }
            else if (numberA != 0 && TextBlockNumber.Text != "" && TextBlockNumber.Text != "-")
            {
                numberB = Double.Parse(TextBlockNumber.Text);

                if (operation != symbol)
                {
                    TextBlockExpression.Text = numberA + operation.ToString() + numberB + "=";
                    TextBlockNumber.Text = Operation(operation).ToString();
                    HistoryList.Items.Add(new ListViewItem { Content = TextBlockExpression.Text + TextBlockNumber.Text, FontSize = 18 });
                    numberA = Double.Parse(TextBlockNumber.Text);
                    TextBlockNumber.Text = "";
                    Calculating(symbol);
                }
                else if (operation == symbol)
                {
                    TextBlockExpression.Text = numberA + operation.ToString() + numberB + "=";
                    TextBlockNumber.Text = Operation(symbol).ToString();
                    HistoryList.Items.Add(new ListViewItem { Content = TextBlockExpression.Text + TextBlockNumber.Text, FontSize = 18 });
                    numberA = 0;
                    operation = '0';
                }
            }
        }

        private double Operation(char symbol) 
        {
            switch (symbol)
            {
                case '+':
                    return numberA + numberB;
                case '-':
                    return numberA - numberB;
                case '*':
                    return numberA * numberB;
                case '/':
                    return numberA / numberB;
                case 's':
                    return numberB * numberB;
                case 'r':
                    return Math.Sqrt(numberB);
                default:
                    return 0;
            }
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            Calculating('+');
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            Calculating('-');
        }

        private void ButtonMultiplication_Click(object sender, RoutedEventArgs e)
        {
            Calculating('*');
        }

        private void ButtonDivision_Click(object sender, RoutedEventArgs e)
        {
            Calculating('/');
        }

        private void ButtonSquare_Click(object sender, RoutedEventArgs e)
        {
            OperationSquareOrRootSquare('s', "^2");
        }

        private void ButtonRoot_Click(object sender, RoutedEventArgs e)
        {
            OperationSquareOrRootSquare('r', "√");
        }

        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlockExpression.Text != "" && TextBlockNumber.Text != "" && operation != '0' && TextBlockNumber.Text != "-")
            {
                Calculating(operation);
            }
        }

        private void OperationSquareOrRootSquare(char oper, string symbol) 
        {
            if (TextBlockExpression.Text == "" && TextBlockNumber.Text != "" && TextBlockNumber.Text != "-" || TextBlockExpression.Text.Contains("^2") || TextBlockExpression.Text.Contains("√") || TextBlockExpression.Text.Contains("="))
            {
                numberB = Double.Parse(TextBlockNumber.Text);
                TextBlockNumber.Text = Operation(oper).ToString();
                if (symbol == "√") 
                {
                    TextBlockExpression.Text = symbol + numberB + "=";
                    HistoryList.Items.Add(new ListViewItem { Content = symbol + numberB + "=" + TextBlockNumber.Text, FontSize = 18 });
                }
                else if (symbol == "^2")
                {
                    TextBlockExpression.Text = numberB + symbol + "=";
                    HistoryList.Items.Add(new ListViewItem { Content = numberB + symbol + "=" + TextBlockNumber.Text, FontSize = 18 });
                }
            }
            else if (TextBlockExpression.Text != "" && TextBlockNumber.Text != "" && TextBlockNumber.Text != "-")
            {
                numberB = Double.Parse(TextBlockNumber.Text);
                TextBlockNumber.Text = Operation(oper).ToString();
                if (symbol == "√")
                {
                    HistoryList.Items.Add(new ListViewItem { Content = symbol + numberB + "=" + TextBlockNumber.Text, FontSize = 18 });
                }
                else if (symbol == "^2")
                {
                    HistoryList.Items.Add(new ListViewItem { Content = numberB + symbol + "=" + TextBlockNumber.Text, FontSize = 18 });
                }
            }
        }
    }
}
