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
        public BindingList<Part> AssociatedParts { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public Product()
        {

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
