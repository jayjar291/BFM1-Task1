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
        public int PartID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public Part(string name, decimal price, int stock, int min, int max)
        {
            Random tempRandom = new Random((int)DateTime.UtcNow.Ticks);
            PartID = tempRandom.Next(10000, 99999);
            Name = name;
            Price = price;
            InStock = stock;
            Min = min;
            Max = max;
        }
    }
}
