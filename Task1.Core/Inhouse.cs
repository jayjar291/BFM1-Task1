using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Core
{
    /// <summary>
    /// public class Inhouse
    /// derived from Part class
    /// holds data for Inhouse parts
    /// </summary>
    public class Inhouse : Part
    {
        /// <summary>
        /// MachineID the machine ID for the Inhouse part.
        /// </summary>
        public int MachineID { get; set; }
        /// <summary>
        /// Special a MVVM alias for MachineID
        /// </summary>
        private int special;

        public int Special
        {
            get { return MachineID; }
            set { MachineID = value; }
        }

        /// <summary>
        /// Inhouse constructor
        /// </summary>
        /// <param name="name">The name of the Part</param>
        /// <param name="price">The price of the part</param>
        /// <param name="stock">The number in stock</param>
        /// <param name="min">The minumum stock</param>
        /// <param name="max">The maximum stock</param>
        /// <param name="machineID">The Machine ID</param>
        /// <param name="id">ID (Only used for object Cloning)</param>
        public Inhouse(string name, decimal price, int stock, int min, int max, int machineID,int id=-1) : base(name,price,stock,min,max,id)
        {
            MachineID = machineID;
        }
    }
}
