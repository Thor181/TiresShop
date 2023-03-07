using System;
using System.Collections.Generic;
using TiresShopApplication.DbClasses;

namespace TiresShopApplication
{
    public partial class AutoTire : IDbClass
    {
        public AutoTire()
        {
            SaleGoods = new HashSet<SaleGoods>();

        }
        public int Id { get; set; }
        public int? IdModif { get; set; }
        public int? IdDriveUnit { get; set; }
        public int idType { get; set; }
        public string? Season { get; set; }
        public string? Load { get; set; }
        public byte[]? Image { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public string? Status { get; set; }
        public virtual DriveUnit? IdDriveUnitNavigation { get; set; }
        public virtual AutoModif? IdModifNavigation { get; set; }

        public virtual ICollection<SaleGoods> SaleGoods { get; set; }
        public virtual ICollection<SaleGood> SaleGood { get; set; }
    }
}
