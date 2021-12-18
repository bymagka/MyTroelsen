using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using AutoLotDAL_Core.Models.MetaData;

namespace AutoLotDAL_Core.Models
{
    [ModelMetadataType(typeof(InventoryMetaData))]
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
