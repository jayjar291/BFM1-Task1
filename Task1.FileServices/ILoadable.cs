using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Core;

namespace Task1.FileServices
{
    public interface ILoadable
    {
        Inventory LoadInventory();
    }
}
