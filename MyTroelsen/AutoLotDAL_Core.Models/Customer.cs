
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AutoLotDAL_Core.Models
{


    public partial class Customer : EntityBase
    {
        [NotMapped]
        public string FullName => FirstName + " " + LastName;



        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public  List<Order> Orders { get; set; } = new List<Order>();
    }
}
