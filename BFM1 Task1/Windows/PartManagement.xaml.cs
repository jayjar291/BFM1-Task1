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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            State = -1;
            Close();
        }
        /// <summary>
        /// event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            State = 1;
            Close();
        }
    }
}
