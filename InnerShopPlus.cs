using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiresShopApplication
{
    internal class InnerShopPlus
    {
        private int _id;
        private int _idmodif;
        private string _tireName;
        private string _diameter;
        private string _driveUnit;
        private string _season;
        private string _load;
        private bool _oem;
        private bool _opt;
        private bool _tunning;
        private decimal _price;
        private bool _wholesale;
        private bool _original;
        private byte[] _image;
        private int _amount;

        public int Id { get { return _id; } set { _id = value; } }
        public bool Oem { get { return _oem; } set { _oem = value; } }
        public bool Opt { get { return _opt; } set { _opt = value; } }
        public bool Tunning { get { return _tunning; } set { _tunning = value; } }
        public int IdModif { get { return _idmodif; } set { _idmodif = value; } }
        public string TireName { get { return _tireName; } set { _tireName = value; } }
        public string Diameter { get { return _diameter; } set { _diameter = value; } }
        public string DriveUnit { get { return _driveUnit; } set { _driveUnit = value; } }
        public decimal Price { get { return _price; } set { _price = value; } }
        public bool Wholesale { get { return _wholesale; } set { _wholesale = value; } }

        public bool Original { get { return _original; } set { _original = value; } }
        public byte[] Image { get { return _image; } set { _image = value; } }
        public int Amount { get { return _amount; } set { _amount = value; } }
        public string Season { get { return _season; } set { _season = value; } }
        public string Load { get { return _load; } set { _load = value; } }


        public string WholesaleXaml { get { return _wholesale == true ? "Да" : "Нет"; } }

        public string OriginalXaml { get { return _original == true ? "Да" : "Нет"; } }

        public InnerShopPlus(int id, string tireName, string driveUnit, string diameter, decimal price, byte[] image, int amount)
        {
            _id = id;
            _diameter = diameter;
            _driveUnit = driveUnit;
            _price = price;
            _image = image;
            Amount = amount;
           



        }
    }
}

