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
        public Part(string name, decimal price, int stock, int min, int max,int id =-1)
        {
            if (id == -1)
            {
                Random tempRandom = new Random((int)DateTime.UtcNow.Ticks);
                PartID = tempRandom.Next(10000, 99999);
            }
            else
            {
                PartID = id;
            }
            Name = name;
            Price = price;
            InStock = stock;
            Min = min;
            Max = max;
        }
    }
}
