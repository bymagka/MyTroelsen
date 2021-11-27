using AutoLotDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.Repos
{
    public class InventoryRepo : BaseRepo<Inventory>
    {
        public override List<Inventory> GetAll()
        {
            return context.Cars.OrderBy(x => x.PetName).ToList();
        }
    }
}
