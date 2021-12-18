using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AutoLotDAL_Core.EF
{
    public class AutoLotContextFactory : IDesignTimeDbContextFactory<AutoLotEntities>
    {
        public AutoLotEntities CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AutoLotEntities>();

            var connectionString = @"server=(localdb)\MSSQLLocalDB;database=AutoLotCore2;integrated security=True; MultipleActiveResultSets=True; App=EntityFramework;";
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                          .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning));

            return new AutoLotEntities(optionsBuilder.Options);
        }
    }
}
