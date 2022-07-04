using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using Task1.Core;

namespace Task1.FileServices
{
    public class FileFunctions : ILoadable, ISaveable
    {
        private string fileName;
        public FileFunctions(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            file.Directory.Create();
            this.fileName = fileName;
        }
        public Inventory LoadInventory()
        {
            Inventory tempInventory = new Inventory();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            throw new NotImplementedException();
        }

        public void SaveInventory(Inventory inventory)
        {
            
            throw new NotImplementedException();
        }
    }
}
