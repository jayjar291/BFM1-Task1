using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Core
{
    /// <summary>
    /// Public calss Product
    /// holds the data for a product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Alias for mvvm bindings
        /// </summary>
        private int Id;

        /// <summary>
        /// Alias for mvvm bindings
        /// </summary>
        public int ID
        {
            get { return ProductID; }
            set { ProductID = value; }
        }
        public BindingList<Part> AssociatedParts { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        /// <summary>
        /// Product constructor
        /// </summary>
        /// <param name="name">The name of the product</param>
        /// <param name="price">The price of the product</param>
        /// <param name="stock">How may products are in stock</param>
        /// <param name="min">The minumum stock</param>
        /// <param name="max">The maximum stock</param>
        /// <param name="id">ID (Only used for object Cloning)</param>
        /// <param name="Parts">List of Associated Parts</param>
        public Product(string name, decimal price, int stock, int min, int max, int id =-1, BindingList<Part> Parts = null)
        {
            //check if we need to copy the id
            if (id == -1)
            {
                //if the id = -1 generate a new one
                Random tempRandom = new Random((int)DateTime.UtcNow.Ticks);
                ProductID = tempRandom.Next(10000, 99999);
            }
            else
            {
                //copy the id
                ProductID = id;
            }
            Name = name;
            Price = price;
            InStock = stock;
            Min = min;
            Max = max;
            //Check if we need to load new parts
            if (Parts == null)
            {
                //create empty list
                AssociatedParts = new BindingList<Part>();
            }
            else
            {
                //Copy and load parts
                AssociatedParts = Parts;
            }
            
        }

        /// <summary>
        /// adds an associated part to the list of Associated Parts
        /// </summary>
        /// <param name="part">part to add</param>
        void addAssociatedPart(Part part)
        {
            if (AssociatedParts == null)
            {
                AssociatedParts = new BindingList<Part>();
            }
            AssociatedParts.Add(part);
        }

        /// <summary>
        /// removes a part with the matching ID from the list of Associated Parts
        /// </summary>
        /// <param name="ID">ID of the part to remove</param>
        /// <returns></returns>
        bool removedAssociatedPart(int ID)
        {
            return false;
        }
        
        /// <summary>
        /// locates a part with the matching ID
        /// </summary>
        /// <param name="ID">ID of the part to locate</param>
        void lookupdAssociatedPart(int ID)
        {

        }
    }
}
