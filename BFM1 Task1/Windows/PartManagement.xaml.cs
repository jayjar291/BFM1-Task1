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
        public Part Part { get; set; }
        bool Type;

        private void refresh()
        {
            DataContext = null;
            DataContext = Part;
        }

        public PartManagement(bool type = true)
        {
            InitializeComponent();
            Type = type;
            if (!type)
            {
                this.Title = "Modify Part";
                txtName.Content = this.Title;
            }
            State = -1;
        }

        private void rdoOutsourced_IsEnabledChanged(object sender, RoutedEventArgs e)
        {
            if (Type)
            {
                if (rdoOutsourced.IsChecked.Value)
                {
                    txtSpecial.Content = "Company Name:";
                    Part = new Outsourced("", 0.0M, 1, 1, 2, "Company Name");
                }
                else
                {
                    txtSpecial.Content = "Machine ID";
                    Part = new Inhouse("", 0.0M, 1, 1, 2, 000);
                }
            }
            else
            {
                if (rdoOutsourced.IsChecked.Value)
                {
                    txtSpecial.Content = "Company Name:";
                    switch (Part)
                    {
                        case Inhouse:
                            Part = new Outsourced(Part.Name, Part.Price, Part.InStock, Part.Min, Part.Max, "", Part.PartID);
                            break;
                        case Outsourced:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    txtSpecial.Content = "Machine ID";
                    switch (Part)
                    {
                        case Inhouse:
                            break;
                        case Outsourced:
                            Part = new Inhouse(Part.Name, Part.Price, Part.InStock, Part.Min, Part.Max, 0000, Part.PartID);
                            break;
                        default:
                            break;
                    }
                }

            }
            refresh();
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            State = -1;
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            State = 1;
            try
            {
                //get WPF validation errors
                if (Validation.GetHasError(txtMin))
                {
                    throw new Exception("Minimum " + Validation.GetErrors(txtMin)[0].ErrorContent.ToString().Replace('.', ' ') + "to a number.");
                }
                if (Validation.GetHasError(txtMax))
                {
                    throw new Exception("Maximum " + Validation.GetErrors(txtMax)[0].ErrorContent.ToString().Replace('.',' ') + "to a number.");
                }
                if (Validation.GetHasError(txtPrice))
                {
                    throw new Exception("Price/Cost " + Validation.GetErrors(txtPrice)[0].ErrorContent.ToString().Replace('.', ' ') + "to a decimal.");
                }
                if (Validation.GetHasError(txtStock))
                {
                    throw new Exception("Inventory " + Validation.GetErrors(txtStock)[0].ErrorContent.ToString().Replace('.', ' ') + "to a number.");
                }
                if (Validation.GetHasError(txtSpecial1))
                {
                    throw new Exception(txtSpecial.Content + " " + Validation.GetErrors(txtSpecial1)[0].ErrorContent.ToString().Replace('.', ' ') + "to a number.");
                };
                if (int.Parse(txtMin.Text) < 1)
                {
                    throw new Exception("Minimum must be grater than 0");
                }
                if (int.Parse(txtMax.Text) < int.Parse(txtMin.Text))
                {
                    throw new Exception("Maximum must be grater than Minmum");
                }
                if (int.Parse(txtPrice.Text) < 0)
                {
                    throw new Exception("Price/Cost must be grater than 0");
                }
                if (int.Parse(txtStock.Text) >= int.Parse(txtMin.Text) && int.Parse(txtStock.Text) <= int.Parse(txtMax.Text)) { }
                else
                {
                    throw new Exception("Inventory must be between Minimum and Maximum");
                }              
            }
            catch (Exception ex)
            {
                State = -1;
                MessageBox.Show($"{ex.Message}", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            if (State == 1)
            {
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
            switch (Part)
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
    }
}
