
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace AutoLotDAL.Models
{
    public partial class Orders
    {
        [Key]
        public int OrderId { get; set; }

        public int CustId { get; set; }

        public int CarId { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Car Car { get; set; }
    }
}
