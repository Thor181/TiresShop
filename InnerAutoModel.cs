using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiresShopApplication
{
    internal class InnerAutoModel
    {
        private int _id;
        private string _name;

        public string Name { get { return _name; } set { _name = value; } }

        public InnerAutoModel(int id, string name)
        {
            _id = id;
            _name = name;
        }
        public int GetId()
        {
            return _id;
            
        }
    }
}
