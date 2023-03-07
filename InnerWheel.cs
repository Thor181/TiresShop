using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiresShopApplication
{
    internal class InnerWheel : IDbClass
    {
   
        private int _id_SaleGoods;
        private int _id_Goods;
        private int _id_Sale;
        private int _amount;

        public int Id_SaleGoods { get { return _id_SaleGoods; } set { _id_SaleGoods = value; } }
        public int Id_Goods { get { return _id_Goods; } set { _id_Goods = value; } }
        public int id_Sale { get { return _id_Sale; } set { _id_Sale = value; }}
        public int amount { get { return _amount; } set { _amount = value; } }

    

        public InnerWheel(int Id_SaleGoods, int Id_Goods, int id_Sale, int amount)
        {
            _id_SaleGoods = Id_SaleGoods;
            _id_Goods = Id_Goods;
            _id_Sale = id_Sale;
            _amount = amount;

        }
    }
}
