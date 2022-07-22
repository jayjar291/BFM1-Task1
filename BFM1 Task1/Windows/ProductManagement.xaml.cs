/*
 * Dev Jared Stapley
 * student ID 002401013
 * Class Software I – C# - C968
 */


using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        /// <summary>
        /// Constructor for ProductManagement
        /// </summary>
        /// <param name="inventory">The inventory object to get the list of parts</param>
        /// <param name="product">The product that will be modified. leave blank if creating a new product</param>
        /// <param name="type">Type of window leave blank (True) for Adding a product, False for Modifying a product</param>
        public ProductManagement(Inventory inventory, Product product = null, bool type = true)
        {
            InitializeComponent();
            Inventory = inventory;
            Product = product;
            if (!type)
            {
                //set title and mode
                this.Title = "Modify Product";
                txtName.Content = this.Title;
            }
            else
            {
                //create a new product
                if (Product == null)
                {
                    Product = new Product("", 0.0M, 0, 0, 0);
                }
            }
            State = -1;
            DataContext = this;

        }
        /// <summary>
        /// event handler for the part search box
        /// sets the item source for the part ListBox to a filtered list that matches the search box string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PartSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Inventory.Parts.Where(em => em.Name.ToLower().Contains(TxtProductSearch.Text.ToLower()) || em.ID.ToString().Contains(TxtProductSearch.Text.ToLower())).Count() >= 1)
            {
                PartsRoster.ItemsSource = Inventory.Parts.Where(em => em.Name.ToLower().Contains(TxtProductSearch.Text.ToLower()) || em.ID.ToString().Contains(TxtProductSearch.Text.ToLower()));
            }
            else
            {
                MessageBox.Show($"No matching parts were found.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// Event handler for the add part button
        /// adds the selected part to the list of Associated Parts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Part target = (Part)PartsRoster.SelectedItem;
            if (target != null)
            {
                Product.AssociatedParts.Add(target);
            }
        }
        /// <summary>
        /// event handler for the remove part button
        /// removes the selected part form the list of Associated Parts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Part target = (Part)SelectedRoster.SelectedItem;
            if (target != null)
            {
                Product.AssociatedParts.Remove(target);
            }
        }
        /// <summary>
        /// event handler for the save button
        /// sets the state to 1 to indicate to the mainwindow that this needs to be saved. then closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {   
            //set state
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
                    throw new Exception("Maximum " + Validation.GetErrors(txtMax)[0].ErrorContent.ToString().Replace('.', ' ') + "to a number.");
                }
                if (Validation.GetHasError(txtPrice))
                {
                    throw new Exception("Price/Cost " + Validation.GetErrors(txtPrice)[0].ErrorContent.ToString().Replace('.', ' ') + "to a decimal.");
                }
                if (Validation.GetHasError(txtStock))
                {
                    throw new Exception("Inventory " + Validation.GetErrors(txtStock)[0].ErrorContent.ToString().Replace('.', ' ') + "to a number.");
                }
                //valadate inputs
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
                if (Product.AssociatedParts.Count < 0)
                {
                    throw new Exception("A minimum of 1 part must be selected");
                }
                if (int.Parse(txtStock.Text) >= int.Parse(txtMin.Text) && int.Parse(txtStock.Text) <= int.Parse(txtMax.Text)) { }
                else
                {
                    throw new Exception("Inventory must be between Minimum and Maximum");
                }
            }
            //reset state and display input error message
            catch (Exception ex)
            {
                State = -1;
                MessageBox.Show($"{ex.Message}", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //check that the state has not changed and close
            if (State == 1)
            {
                Close();
            }
        }
        /// <summary>
        /// event handler for the cancle button
        /// sets the state to -1 to indicate to the mainwindow that changes need to be reverted. then closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            State = -1;
            Close();
        }
    }
}
