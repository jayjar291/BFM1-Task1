using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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
            //test data
            Inhouse test1 = new Inhouse("hydrocoptic marzelvanes", 36.55M, 25, 1, 25, 35);
            Inhouse test2 = new Inhouse("lunar waneshaft", 36.55M, 25, 1, 25, 35);
            Inhouse test3 = new Inhouse("tremie pipe", 36.55M, 25, 1, 25, 35);
            Inhouse test4 = new Inhouse("grammeters", 36.55M, 25, 1, 25, 35);
            Product test5 = new Product("Turbo Encabulator", 36.55M, 25, 1, 25);
            //add test data
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
        /// <summary>
        /// event handler for the button "Add" part.
        /// creates a PartManagement window for the user create a new part.
        /// if the user cancels the Add action then no part will be added.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPartAdd_Click(object sender, RoutedEventArgs e)
        {
            PartManagement addPart = new PartManagement();
            addPart.ShowDialog();
        }
        /// <summary>
        /// event handler for the modify part button
        /// creates a backup of the target part then creates a PartManagement window for the user 
        /// to make changes to the selected part. if the user cancels the modify action the modified 
        /// part will be replaced with the backup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// event handler for the delete part button
        /// deletes a selected part from the part list and confirms the action with the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// event handler for the add product button
        /// creates a ProductManagement window for the user create a new product.
        /// if the user cancels the Add action then no product will be added.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProductAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductManagement productManagement = new ProductManagement(Inventory);
            productManagement.ShowDialog();
            Product tempProduct = productManagement.Product;
            Inventory.Products.Add(tempProduct);
        }
        /// <summary>
        /// event handler for the modify button
        /// creates a backup of the target product then creates a ProductManagement window for the user 
        /// to make changes to the selected product. if the user cancels the modify action the modified 
        /// product will be replaced with the backup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// event handler for the delete product button
        /// deletes a selected product from the products list and confirms the action with the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// event handler for the part search box
        /// sets the item source for the part ListBox to a filtered list that matches the search box string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PartSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => PartsRoster.ItemsSource = Inventory.Parts.Where(em => em.Name.ToLower().Contains(PartSearch.Text.ToLower()));
        /// <summary>
        /// event handeler for the product search box
        /// sets the item source for the product ListBox to a filtered list that matches the search box string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProductSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => ProductsRoster.ItemsSource = Inventory.Products.Where(em => em.Name.ToLower().Contains(ProductSearch.Text.ToLower()));
        /// <summary>
        /// event handler for the exit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //close the program
            Close();
        }
    }
}
