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

namespace BmiRechner
{
    /// <summary>
    /// interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        double gewichtDouble, groesseDouble, bmi;
        string gewichtText, groesseText;

        public string GewichtText
        {
            get
            {
                return gewichtText;
            }

            set
            {
                double doubleValue;

                if (IsDouble(value, out doubleValue))
                {
                    gewichtText = value;

                    if (doubleValue != gewichtDouble)
                    {
                        gewichtDouble = doubleValue;
                        SetBMI();
                    }
                }

                UpadateUI();
            }
        }

        public string GroesseText
        {
            get
            {
                return groesseText;
            }

            set
            {
                double doubleValue;

                if (IsDouble(value, out doubleValue))
                {
                    groesseText = value;

                    if (doubleValue != groesseDouble)
                    {
                        groesseDouble = doubleValue;
                        SetBMI();
                    }
                }

                UpadateUI();
            }
        }

        public string BmiText
        {
            get
            {
                if (bmi == 0) return "";
                return Math.Round(bmi, 2).ToString();
            }

            set
            { }
        }

        public Thickness ArrorPosition
        {
            get
            {
                if (bmi < 10 || bmi > 50) return new Thickness(0);

                double left = bmi * 20.42 - 204.2 + 5;
                return new Thickness(left, 0, 0, 0);
            }
        }

        public Visibility ArrorVisibility
        {
            get
            {
                return bmi > 10 && bmi < 60 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public ViewModel()
        {
            gewichtDouble = 70;
            groesseDouble = 1.7;

            gewichtText = gewichtDouble.ToString();
            groesseText = groesseDouble.ToString();

            SetBMI();
        }

        private void SetBMI()
        {
            if (groesseDouble == 0)
            {
                bmi = 0;
                return;
            }

            bmi = gewichtDouble / (groesseDouble * groesseDouble);
        }

        private bool IsDouble(string s, out double value)
        {
            if (s == "")
            {
                value = 0;
                return true;
            }

            return double.TryParse(s, out value);
        }

        private void UpadateUI()
        {
            NotifyPropertyChanged("GewichtText");
            NotifyPropertyChanged("GroesseText");
            NotifyPropertyChanged("BmiText");
            NotifyPropertyChanged("ArrorPosition");
            NotifyPropertyChanged("ArrorVisibility");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
