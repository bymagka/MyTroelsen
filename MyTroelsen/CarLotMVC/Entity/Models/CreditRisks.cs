namespace AutoLotDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Models.Base;
    public partial class CreditRisks : EntityBase
    {
       
        [StringLength(50)]
        [Index("IDX_CreditRiskName",IsUnique = true, Order=2)]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Index("IDX_CreditRiskName", IsUnique =true,Order =1)]
        public string LastName { get; set; }
    }
}
