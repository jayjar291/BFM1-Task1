/*
 * Dev Jared Stapley
 * student ID 002401013
 * Class Software I – C# - C968
 */


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
            PartManagement partManagement = new PartManagement();
            partManagement.ShowDialog();
            //check if the new part should be saved.
            if (partManagement.State != -1)
            {
                Inventory.Parts.Add(partManagement.Part);
            }
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
            //find the selected part
            Part target = (Part)PartsRoster.SelectedItem;
            if (target != null)
            {
               //create a backup of the part
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
                //create the PartManagement in the modify mode
                PartManagement modifyPart = new PartManagement(false);
                //set the dataContext to the target part
                modifyPart.Part = target;
                modifyPart.ShowDialog();
                //check if we need to restore the backup part
                if (modifyPart.State == -1)
                {
                    Inventory.Parts.Remove(target);
                    Inventory.Parts.Add(backup);
                }
                else if (modifyPart.State == 1)
                {
                    Inventory.Parts.Remove(target);
                    Inventory.Parts.Add(modifyPart.Part);
                }
            }
            else // if no part is selected notify the user
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
            //get target part
            Part target = (Part)PartsRoster.SelectedItem;
            if (target != null)
            {
                //confirm with the user
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
            //no part selected
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
            //create productManagement window
            ProductManagement productManagement = new ProductManagement(Inventory);
            productManagement.ShowDialog();
            //check if the new product should be saved.
            if (productManagement.State != -1)
            {
                Inventory.Products.Add(productManagement.Product);
            }
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
            //get target part
            Product target = (Product)ProductsRoster.SelectedItem;
            if (target != null)
            {
                //create backup part
                BindingList<Part> backupParts = new BindingList<Part>();
                //backup Associated Parts
                foreach (Part part in target.AssociatedParts)
                {
                    //create backup of each part
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
                //create product backup with the list of backup parts
                Product backup = new Product(target.Name, target.Price, target.InStock, target.Min, target.Max, target.ID, backupParts);
                //create the modifyProduct window
                ProductManagement modifyProduct = new ProductManagement(Inventory,target,false);
                modifyProduct.ShowDialog();
                //check if we need to load the backup
                if (modifyProduct.State == -1)
                {
                    Inventory.Products.Remove(target);
                    Inventory.Products.Add(backup);
                }
            }
            //no product selected
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
            //get target product
            Product target = (Product)ProductsRoster.SelectedItem;
            if (target != null)
            {
                //confirm with user
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {target.Name} Product?.", "Delete Product", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (target.AssociatedParts.Count > 0)
                        {
                            //inform the user that we cant remove a product that has parts
                            MessageBox.Show($"Unable to delete {target.Name}.\nThis Product can’t be deleted because a part is assigned to it.", "Delete Product", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            break;
                        }
                        else
                        {
                            Inventory.Products.Remove(target);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
            //no product selected
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
        void PartSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Inventory.Parts.Where(em => em.Name.ToLower().Contains(PartSearch.Text.ToLower()) || em.ID.ToString().Contains(PartSearch.Text.ToLower())).Count() >= 1)
            {
                PartsRoster.ItemsSource = Inventory.Parts.Where(em => em.Name.ToLower().Contains(PartSearch.Text.ToLower()) || em.ID.ToString().Contains(PartSearch.Text.ToLower()));
            }
            else
            {
                MessageBox.Show($"No matching parts were found.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// event handeler for the product search box
        /// sets the item source for the product ListBox to a filtered list that matches the search box string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProductSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Inventory.Products.Where(em => em.Name.ToLower().Contains(ProductSearch.Text.ToLower()) || em.ID.ToString().Contains(ProductSearch.Text.ToLower())).Count() >= 1)
            {
                ProductsRoster.ItemsSource = Inventory.Products.Where(em => em.Name.ToLower().Contains(ProductSearch.Text.ToLower()) || em.ID.ToString().Contains(ProductSearch.Text.ToLower()));
            }
            else
            {
                MessageBox.Show($"No matching products were found.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
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
