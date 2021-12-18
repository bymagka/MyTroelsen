using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace AutoLotDAL_Core.Models
{
   

    public partial class Order : EntityBase
    {

        public int CustomerId { get; set; }

        public int CarId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [ForeignKey(nameof(CarId))]
        public Inventory Car { get; set; }
    }
}
