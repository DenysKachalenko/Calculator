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
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerSplitView.IsPaneOpen = !HamburgerSplitView.IsPaneOpen;
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HistorySplitView.IsPaneOpen = !HistorySplitView.IsPaneOpen;
            //HistoryList.Items.Add(new ListViewItem { Content = "1023 - 133 = 1212" });
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
            if (TextBlockNumber.Text != "") 
            {
                AddNumber('0');
            }
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

        private void ButtonSquare_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
