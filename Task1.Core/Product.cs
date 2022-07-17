using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Core
{
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
        public Product(string name, decimal price, int stock, int min, int max, int id =-1, BindingList<Part> Parts = null)
        {
            if (id == -1)
            {
                Random tempRandom = new Random((int)DateTime.UtcNow.Ticks);
                ProductID = tempRandom.Next(10000, 99999);
            }
            Name = name;
            Price = price;
            InStock = stock;
            Min = min;
            Max = max;
            if (Parts == null)
            {
                AssociatedParts = new BindingList<Part>();
            }
            else
            {
                AssociatedParts = Parts;
            }
            
        }

        /// <summary>
        /// adds an associated part to the list of Associated Parts
        /// </summary>
        /// <param name="part">part to add</param>
        void addAssociatedPart(Part part)
        {

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
