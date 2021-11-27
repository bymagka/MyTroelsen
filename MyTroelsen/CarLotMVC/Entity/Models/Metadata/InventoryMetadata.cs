using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CarLotMVC.Entity.Models.Metadata
{
    public class InventoryMetadata
    {
        [Display(Name = "Pet name")]
        public string PetName;

        [StringLength(50,ErrorMessage = "Less then 50")]
        public string Make;
    }
}