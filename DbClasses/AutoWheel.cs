using System;
using System.Collections.Generic;

namespace TiresShopApplication
{
    public partial class AutoWheel : IDbClass
    {
        public int Id { get; set; }
        public int? IdModif { get; set; }
        public int? IdDriveUnit { get; set; }
        public byte[]? Image { get; set; }
        public decimal? Price { get; set; }
        public int Amount { get; set; }
        public string? Load { get; set; }
        public string? Season { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
 

        public virtual DriveUnit? IdDriveUnitNavigation { get; set; }
        public virtual AutoModif? IdModifNavigation { get; set; }
    }
}
