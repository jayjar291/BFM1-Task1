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
    /// Interaction logic for ProductManagement.xaml
    /// </summary>
    public partial class ProductManagement : Window
    {
        public Inventory inventory { get; set; }
        public ProductManagement()
        {
            InitializeComponent();
            
        }
        void PartSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => PartsRoster.ItemsSource = inventory.Parts.Where(em => em.Name.ToLower().Contains(TxtProductSearch.Text.ToLower()));

    }
}
