using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Core
{
    public class Outsourced : Part
    {
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
        public Outsourced(string name, decimal price, int stock, int min, int max, string companyName) : base(name, price, stock, min, max)
        {
            CompanyName = companyName;
        }
    }
}
