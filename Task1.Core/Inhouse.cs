using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Core
{
    public class Inhouse : Part
    {
        public int MachineID { get; set; }
        private int special;

        public int Special
        {
            get { return MachineID; }
            set { MachineID = value; }
        }


        public Inhouse(string name, decimal price, int stock, int min, int max, int machineID) : base(name,price,stock,min,max)
        {
            MachineID = machineID;
        }
    }
}
