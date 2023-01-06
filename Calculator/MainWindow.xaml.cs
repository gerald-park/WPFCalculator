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
        double lastNumber, result;
        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();
            resultLabel.Content = "0";

            acButton.Click += acButton_Click; // 코드레벨에서 이벤트를 등록하는 방법
        }

        private void equalbutton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch(selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.Substration(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Devide(lastNumber, newNumber);
                        break;
                }
                resultLabel.Content = result.ToString();
            }

        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if(resultLabel.Content.ToString().Contains("."))
            {
                //do nothing
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
            
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }
            if (sender == multiplicationButton)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == divisionButton)
                selectedOperator = SelectedOperator.Division;
            if (sender == plueButton)
                selectedOperator = SelectedOperator.Addition;
            if (sender == minusButton)
                selectedOperator = SelectedOperator.Substraction;
        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                tempNumber = lastNumber / 100;
                if (lastNumber != 0)
                    tempNumber *= lastNumber;
                resultLabel.Content = tempNumber.ToString();
            }
        }

        // 50 + 5% = (52.5)
        // 80 + 10% (8) = (88)

        private void negativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
            
        }

        private void acButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }
    public class SimpleMath
    {
        public static double Add(double n1, double n2)
        {
            return n1 + n2;
        }
        public static double Substration(double n1, double n2)
        {
            return n1 - n2;
        }
        public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }
        public static double Devide(double n1, double n2)
        {
            if(n2 == 0 )
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return n1 / n2;
        }
    }
}
