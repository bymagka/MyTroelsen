using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL_Core.Models;
using AutoLotDAL_Core.EF;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EF;

namespace AutoLotDAL_Core.Repos
{
    public class InventoryRepo : BaseRepo<Inventory>, IInventoryRepo
    {
        public InventoryRepo(AutoLotEntities context) : base(context)
        {

        }

        public InventoryRepo() : base(new AutoLotEntities())
        {

        }

        public override List<Inventory> GetAll() => GetAll(x => x.PetName, true).ToList();

        public List<Inventory> GetPinkCars() => GetSome(x => x.Color == "Pink");


        public List<Inventory> GetRelatedSource() => context.Cars.FromSqlRaw("SELECT * FROM Inventory").Include(x => x.Orders).ThenInclude(x => x.Customer).ToList();

        public List<Inventory> Search(string searchString)
        {
            return context.Cars.Where(c => Functions.Like(c.PetName, $"%{searchString}%")).ToList();
        }
    }
}
