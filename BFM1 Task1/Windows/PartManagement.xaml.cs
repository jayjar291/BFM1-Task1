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
using System.Windows.Shapes;
using Task1.Core;

namespace Task1.UI.Windows
{
    /// <summary>
    /// Interaction logic for PartManagement.xaml
    /// </summary>
    public partial class PartManagement : Window
    {
        public int State { get; set; }
        public Part part { get; set; }
        public PartManagement(bool type = true)
        {
            InitializeComponent();
            if (!type)
            {
                this.Title = "Modify Part";
                txtName.Content = this.Title;
                rdoOutsourced.IsEnabled = false;
                rdoInhouse.IsEnabled = false;
            }
            State = -1;
        }

        private void rdoOutsourced_IsEnabledChanged(object sender, RoutedEventArgs e)
        {
            if (rdoOutsourced.IsChecked.Value)
            {
                txtSpecial.Content = "Company Name:";
            }
            else
            {
                txtSpecial.Content = "Machine ID";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (DataContext)
            {
                case Inhouse:
                    rdoInhouse.IsChecked = true;
                    break;
                case Outsourced:
                    rdoOutsourced.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //data checks
            try
            {
                if (int.Parse(txtMax.Text) < int.Parse(txtMin.Text))
                {
                    throw new Exception("Max inventory must be must be greater than Min inventory");
                }
                if (int.Parse(txtMax.Text) !> 0)
                {
                    throw new Exception("Max inventory must be greater than 0");
                }
                if (int.Parse(txtMin.Text) !> 0)
                {
                    throw new Exception("Min inventory must be greater than 0");
                }
                if (int.Parse(txtStock.Text) !> int.Parse(txtMin.Text) || int.Parse(txtStock.Text) !< int.Parse(txtMax.Text))
                {
                    throw new Exception("Inventory must be a value between Min and Max inventory");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Data Input error\n{ex.Message}", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
