using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Core
{
    /// <summary>
    /// Abstract Part class
    /// stores the bulk info for a Part object
    /// </summary>
    public abstract class Part
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
            get { return PartID; }
            set { PartID = value; }
        }
        public int PartID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        /// <summary>
        /// The constructor for the part class
        /// </summary>
        /// <param name="name">The name of the Part</param>
        /// <param name="price">The price of the part</param>
        /// <param name="stock">The number in stock</param>
        /// <param name="min">The minumum stock</param>
        /// <param name="max">The maximum stock</param>
        /// <param name="id">ID (Only used for object Cloning)</param>
        public Part(string name, decimal price, int stock, int min, int max,int id =-1)
        {
            //check if we need to copy the id
            if (id == -1)
            {
                //if the id = -1 generate a new one
                Random tempRandom = new Random((int)DateTime.UtcNow.Ticks);
                PartID = tempRandom.Next(10000, 99999);
            }
            else
            {
                //copy the id
                PartID = id;
            }
            //Assign values
            Name = name;
            Price = price;
            InStock = stock;
            Min = min;
            Max = max;
        }
    }
}
