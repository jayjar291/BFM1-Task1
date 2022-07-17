using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Core
{
    /// <summary>
    /// public class Outsourced
    /// derived from Part class
    /// holds data for Outsourced parts
    /// </summary>
    public class Outsourced : Part
    {
        /// <summary>
        /// CompanyName the name of the company that produced the part.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Alias for mvvm bindings
        /// </summary>
        private string special;

        /// <summary>
        /// Alias for mvvm bindings
        /// </summary>
        public string Special
        {
            get { return CompanyName; }
            set { CompanyName = value; }
        }
        /// <summary>
        /// Outsourced constructor
        /// </summary>
        /// <param name="name">The name of the Part</param>
        /// <param name="price">The price of the part</param>
        /// <param name="stock">The number in stock</param>
        /// <param name="min">The minumum stock</param>
        /// <param name="max">The maximum stock</param>
        /// <param name="companyName">The company name</param>
        /// <param name="id">ID (Only used for object Cloning)</param>
        public Outsourced(string name, decimal price, int stock, int min, int max, string companyName,int id= -1) : base(name, price, stock, min, max,id)
        {
            CompanyName = companyName;
        }
    }
}
