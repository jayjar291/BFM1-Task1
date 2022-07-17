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
using Task1.Core;
using Task1.UI.Windows;

namespace Task1.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Inventory Inventory = new Inventory();
        public MainWindow()
        {
            //Startup
            InitializeComponent();
            DataContext = Inventory;
            Inhouse test1 = new Inhouse("hydrocoptic marzelvanes", 36.55M, 25, 1, 25, 35);
            Inhouse test2 = new Inhouse("lunar waneshaft", 36.55M, 25, 1, 25, 35);
            Inhouse test3 = new Inhouse("tremie pipe", 36.55M, 25, 1, 25, 35);
            Inhouse test4 = new Inhouse("grammeters", 36.55M, 25, 1, 25, 35);
            Product test5 = new Product("Turbo Encabulator", 36.55M, 25, 1, 25);

            Inventory.addPart(test1);
            Inventory.addPart(test2);
            Inventory.addPart(test3);
            Inventory.addPart(test4);
            Inventory.addProduct(test5);
            //refresh datacontext
            refresh();
        }

        /// <summary>
        /// refreshes the datacontext
        /// </summary>
        private void refresh()
        {
            DataContext = null;
            DataContext = Inventory;
        }

        private void btnPartAdd_Click(object sender, RoutedEventArgs e)
        {
            PartManagement addPart = new PartManagement();
            addPart.ShowDialog();
        }

        private void btnPartModify_Click(object sender, RoutedEventArgs e)
        {
            Part target = (Part)PartsRoster.SelectedItem;
            if (target != null)
            {
               
                Part backup = null;
                switch (target)
                {
                    case Inhouse:
                        backup = new Inhouse(target.Name, target.Price, target.InStock, target.Min, target.Max, ((Inhouse)target).Special, target.PartID);
                        break;
                    case Outsourced:
                        backup = new Outsourced(target.Name, target.Price, target.InStock, target.Min, target.Max, ((Outsourced)target).Special, target.PartID);
                        break;
                    default:
                        break;
                }
                PartManagement modifyPart = new PartManagement(false);
                modifyPart.DataContext = target;
                modifyPart.ShowDialog();
                if (modifyPart.State == -1)
                {
                    Inventory.Parts.Remove(target);
                    Inventory.Parts.Add(backup);
                }
            }
            else
            {
                MessageBox.Show($"Please select a Part.", "Select Part", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnPartDelete_Click(object sender, RoutedEventArgs e)
        {
            Part target = (Part)PartsRoster.SelectedItem;
            if (target != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {target.Name} part?.", "Delete Part", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Inventory.Parts.Remove(target);
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show($"Please select a Part.", "Select Part", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnProductAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductManagement productManagement = new ProductManagement(Inventory);
            productManagement.ShowDialog();
            Product tempProduct = productManagement.Product;
            Inventory.Products.Add(tempProduct);
        }

        private void btnProductModify_Click(object sender, RoutedEventArgs e)
        {
            Product target = (Product)ProductsRoster.SelectedItem;
            if (target != null)
            {
                BindingList<Part> backupParts = new BindingList<Part>();
                foreach (Part part in target.AssociatedParts)
                {
                    Part tempPart = null;
                    switch (part)
                    {
                        case Inhouse:
                            tempPart = new Inhouse(part.Name, part.Price, part.InStock, part.Min, part.Max, ((Inhouse)part).Special, part.PartID);
                            break;
                        case Outsourced:
                            tempPart = new Outsourced(part.Name, part.Price, part.InStock, part.Min, part.Max, ((Outsourced)part).Special, part.PartID);
                            break;
                        default:
                            break;
                    }
                    backupParts.Add(tempPart);
                }
                Product backup = new Product(target.Name, target.Price, target.InStock, target.Min, target.Max, target.ID, backupParts);
                ProductManagement modifyProduct = new ProductManagement(Inventory,target,false);
                modifyProduct.ShowDialog();
                if (modifyProduct.State == -1)
                {
                    Inventory.Products.Remove(target);
                    Inventory.Products.Add(backup);
                }
            }
            else
            {
                MessageBox.Show($"Please select a Product.", "Select Product", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnProductDelete_Click(object sender, RoutedEventArgs e)
        {
            Product target = (Product)ProductsRoster.SelectedItem;
            if (target != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {target.Name} Product?.", "Delete Product", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Inventory.Products.Remove(target);
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show($"Please select a Product.", "Select Product", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void PartSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => PartsRoster.ItemsSource = Inventory.Parts.Where(em => em.Name.ToLower().Contains(PartSearch.Text.ToLower()));
        void ProductSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => ProductsRoster.ItemsSource = Inventory.Products.Where(em => em.Name.ToLower().Contains(ProductSearch.Text.ToLower()));



    }
}
