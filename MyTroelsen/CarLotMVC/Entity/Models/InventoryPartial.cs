using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarLotMVC.Entity.Models.Metadata;

namespace AutoLotDAL.Models
{
    [MetadataType(typeof(InventoryMetadata))]
    public partial class Inventory
    {
        [NotMapped]
        public string MakeColor => $"{this.Make} + ({this.Color})";

        public override string ToString()
        {
            return $"{this.PetName ?? "**No name**"} is a {this.Color}\n{this.Make} with ID {this.Id}.";
        }
    }
}
