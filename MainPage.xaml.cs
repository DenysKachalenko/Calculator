using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
    public sealed partial class MainPage : Page
    {
        private double FirstNumber { get; set; } = 0;
        private double SecondNumber { get; set; } = 0;
        private char Operation { get; set; } = '0';

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
            FirstNumber = 0;
            SecondNumber = 0;
            Operation = '0';
        }

        private void AddNumber(char Symbol)
        {
            TextBlockNumber.Text += Symbol; 
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlockNumber.Text != string.Empty) 
            {
                TextBlockNumber.Text = TextBlockNumber.Text.Remove(TextBlockNumber.Text.Length -1,  1);
            }
        }

        private void ButtonPosOrNeg_Click(object sender, RoutedEventArgs e)
        {

            if (TextBlockNumber.Text == string.Empty)
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
            if (TextBlockNumber.Text != string.Empty && TextBlockNumber.Text != "-" && !TextBlockNumber.Text.Contains(".")) 
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

        private void Calculating(char Symbol)
        {
            if (FirstNumber == 0 && TextBlockNumber.Text != string.Empty && TextBlockNumber.Text != "-")
            {
                Operation = Symbol;
                FirstNumber = Double.Parse(TextBlockNumber.Text);
                TextBlockExpression.Text = FirstNumber + Operation.ToString();
                TextBlockNumber.Text = string.Empty;
            }
            else if (FirstNumber != 0 && TextBlockNumber.Text == string.Empty && TextBlockNumber.Text != "-")
            {
                Operation = Symbol;
                TextBlockExpression.Text = FirstNumber + Operation.ToString();
            }
            else if (FirstNumber != 0 && TextBlockNumber.Text != string.Empty && TextBlockNumber.Text != "-")
            {
                SecondNumber = Double.Parse(TextBlockNumber.Text);

                if (Operation != Symbol)
                {
                    TextBlockExpression.Text = FirstNumber + Operation.ToString() + SecondNumber + "=";
                    TextBlockNumber.Text = PerformAnOperation(Operation).ToString(CultureInfo.InvariantCulture);
                    HistoryList.Items?.Add(new ListViewItem { Content = TextBlockExpression.Text + TextBlockNumber.Text, FontSize = 18 });
                    FirstNumber = Double.Parse(TextBlockNumber.Text);
                    TextBlockNumber.Text = string.Empty;
                    Calculating(Symbol);
                }
                else if (Operation == Symbol)
                {
                    TextBlockExpression.Text = FirstNumber + Operation.ToString() + SecondNumber + "=";
                    TextBlockNumber.Text = PerformAnOperation(Symbol).ToString(CultureInfo.InvariantCulture);
                    HistoryList.Items?.Add(new ListViewItem { Content = TextBlockExpression.Text + TextBlockNumber.Text, FontSize = 18 });
                    FirstNumber = 0;
                    Operation = '0';
                }
            }
        }

        private double PerformAnOperation(char Symbol) 
        {
            switch (Symbol)
            {
                case '+':
                    return FirstNumber + SecondNumber;
                case '-':
                    return FirstNumber - SecondNumber;
                case '*':
                    return FirstNumber * SecondNumber;
                case '/':
                    return FirstNumber / SecondNumber;
                case 's':
                    return SecondNumber * SecondNumber;
                case 'r':
                    return Math.Sqrt(SecondNumber);
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
            if (TextBlockExpression.Text != string.Empty && TextBlockNumber.Text != string.Empty && Operation != '0' && TextBlockNumber.Text != "-")
            {
                Calculating(Operation);
            }
        }

        private void OperationSquareOrRootSquare(char OperationSymbol, string Symbol) 
        {
            if (TextBlockExpression.Text == "" && TextBlockNumber.Text != string.Empty && TextBlockNumber.Text != "-" || TextBlockExpression.Text.Contains("^2") || TextBlockExpression.Text.Contains("√") || TextBlockExpression.Text.Contains("="))
            {
                if (!Double.TryParse(TextBlockNumber.Text, out var SecondNumberTemp))
                {
                    return;
                }

                SecondNumber = SecondNumberTemp;

                TextBlockNumber.Text = PerformAnOperation(OperationSymbol).ToString(CultureInfo.InvariantCulture);
                if (Symbol == "√")
                {
                    TextBlockExpression.Text = Symbol + SecondNumber + "=";
                    HistoryList.Items?.Add(new ListViewItem { Content = Symbol + SecondNumber + "=" + TextBlockNumber.Text, FontSize = 18 });
                }
                else if (Symbol == "^2")
                {
                    TextBlockExpression.Text = SecondNumber + Symbol + "=";
                    HistoryList.Items?.Add(new ListViewItem { Content = SecondNumber + Symbol + "=" + TextBlockNumber.Text, FontSize = 18 });
                }
            }
            else if (TextBlockExpression.Text != string.Empty && TextBlockNumber.Text != string.Empty && TextBlockNumber.Text != "-")
            {
                if (!Double.TryParse(TextBlockNumber.Text, out var SecondNumberTemp))
                {
                    return;
                }

                SecondNumber = SecondNumberTemp;

                TextBlockNumber.Text = PerformAnOperation(OperationSymbol).ToString(CultureInfo.InvariantCulture);
                if (Symbol == "√")
                {
                    HistoryList.Items?.Add(new ListViewItem { Content = Symbol + SecondNumber + "=" + TextBlockNumber.Text, FontSize = 18 });
                }
                else if (Symbol == "^2")
                {
                    HistoryList.Items?.Add(new ListViewItem { Content = SecondNumber + Symbol + "=" + TextBlockNumber.Text, FontSize = 18 });
                }
            }
        }
    }
}
