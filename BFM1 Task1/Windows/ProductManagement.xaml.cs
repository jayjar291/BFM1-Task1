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
        public Inventory Inventory { get; set; }
        public Product Product { get; set; }
        public int State { get; set; }
        public ProductManagement(Inventory inventory, Product product ,bool type = true)
        {
            InitializeComponent();
            Inventory = inventory;
            Product = product;
            if (!type)
            {
                this.Title = "Modify Part";
                txtName.Content = this.Title;
            }
            State = -1;
            DataContext = this;

        }
        void PartSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => PartsRoster.ItemsSource = Inventory.Parts.Where(em => em.Name.ToLower().Contains(TxtProductSearch.Text.ToLower()));

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Part target = (Part)PartsRoster.SelectedItem;
            if (target != null)
            {
                Product.AssociatedParts.Add(target);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Part target = (Part)SelectedRoster.SelectedItem;
            if (target != null)
            {
                Product.AssociatedParts.Remove(target);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
