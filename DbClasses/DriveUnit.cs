using System;
using System.Collections.Generic;

namespace TiresShopApplication
{
    public partial class DriveUnit : IDbClass
    {
        public DriveUnit()
        {
            AutoTires = new HashSet<AutoTire>();
            AutoWheels = new HashSet<AutoWheel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AutoTire> AutoTires { get; set; }
        public virtual ICollection<AutoWheel> AutoWheels { get; set; }
    }
}
