using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
            State = 1;
            Close();
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
