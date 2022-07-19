using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Core
{
    /*
     * Public class Inventory
     * 
     * This class is the root of the program it stores all the parts and products and includes
     * functions and methods to manage, edit, remove, and add objects.
     * 
     */
    public class Inventory
    {
        //observable collection of products
        public ObservableCollection<Product> Products { get; set; }
        //observable collection of parts
        public ObservableCollection<Part> Parts { get; set; }

        // class constructor
        public Inventory()
        {
            
        }

        /// <summary>
        /// adds a product to the list of products
        /// </summary>
        /// <param name="product">product object to add</param>
        public void addProduct(Product product)
        {
            if (Products == null)
            {
                Products = new ObservableCollection<Product>();
            }
            Products.Add(product);
        }

        /// <summary>
        /// removes a product with the matching ID
        /// returns 'True' if successful  
        /// </summary>
        /// <param name="ID">interger ID of the object to remove</param>
        /// <returns></returns>
        public bool removeProduct(int ID)
        {
            try
            {
                Products.Remove(Products.Where(em => em.ID == ID).FirstOrDefault());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        /// <summary>
        /// finds and returns a product with the matching ID
        /// </summary>
        /// <param name="ID">interger ID of the object to locate</param>
        /// <returns></returns>
        public Product lookupProduct(int ID)
        {
            return Products.Where(em => em.ID == ID).FirstOrDefault();
        }

        /// <summary>
        /// updates a product object with the matching ID with a new product
        /// </summary>
        /// <param name="ID">interger ID of the object to update</param>
        /// <param name="product">updated object</param>
        public void updateProduct(int ID, Product product)
        {
            Product Target = Products.Where(em => em.ID == ID).FirstOrDefault();
            Target = product;
        }

        /// <summary>
        /// adds a part to the list of parts
        /// </summary>
        /// <param name="part">part object to add</param>
        public void addPart(Part part)
        {
            if (Parts == null)
            {
                Parts = new ObservableCollection<Part>();
            }
            Parts.Add(part);
        }

        /// <summary>
        /// removes a part from the list of parts
        /// returns 'True' if successful  
        /// </summary>
        /// <param name="part">part object to remove</param>
        /// <returns></returns>
        public bool deletePart(Part part)
        {
            try
            {
                Parts.Remove(part);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        /// <summary>
        /// finds and returns a part with the matching ID
        /// </summary>
        /// <param name="ID">interger ID of the object to locate</param>
        /// <returns></returns>
        public Part lookupPart(int ID)
        {
            return Parts.Where(em => em.ID == ID).FirstOrDefault() ;
        }

        /// <summary>
        /// updates a part object with the matching ID with a new part
        /// </summary>
        /// <param name="ID">interger ID of the object to update</param>
        /// <param name="part">updated object</param>
        public void updatePart(int ID, Part part)
        {
            Part Target = Parts.Where(em => em.ID == ID).FirstOrDefault();
            Target = part;
        }

    }
}
