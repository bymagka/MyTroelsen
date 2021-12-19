﻿using System.Collections.Generic;
using AutoLotDAL_Core.EF;
using AutoLotDAL_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoLotDAL_Core.DataInitialization
{
    public class MyDataInitializer 
    {
        public static void InitializeData(AutoLotEntities context)
        {
            var customers = new List<Customer>()
            {
                new Customer(){FirstName = "Dave",LastName = "Brenner"},
                new Customer(){FirstName = "Matt",LastName = "Walton"},
                new Customer(){FirstName = "Steve",LastName = "Hagen" },
                new Customer(){FirstName = "Pat",LastName = "Walton" },
                new Customer(){FirstName = "Bad",LastName = "Customer" }
            };

            customers.ForEach(x => context.Customers.Add(x));
            context.SaveChanges();

            var cars = new List<Inventory>()
            {
                new Inventory(){Make = "VW",Color="Black",PetName = "Zippy"},
                new Inventory(){Make = "Ford",Color="Rust",PetName = "Rusty"},
                new Inventory(){Make = "Saab",Color="Black",PetName = "Mel"},
                new Inventory(){Make = "Yugo",Color="Yellow",PetName = "Clunker"},
                new Inventory(){Make = "BMW",Color="Black",PetName = "Bimmer"},
                new Inventory(){Make = "BMW",Color="Green",PetName = "Hank"},
                new Inventory(){Make = "BMW",Color="Pink",PetName = "Pinky"},
                new Inventory(){Make = "Pinto",Color="Black",PetName = "Pete"},
                new Inventory(){Make = "Yugo",Color="Brown",PetName = "Brownie"}
            };

            context.Cars.AddRange(cars);
            context.SaveChanges();

            var orders = new List<Order>()
            {
                new Order(){Car = cars[0],Customer = customers[0]},
                new Order(){Car = cars[1],Customer = customers[1]},
                new Order(){Car = cars[2],Customer = customers[2]},
                new Order(){Car = cars[3],Customer = customers[3]}
            };

            orders.ForEach(x => context.Orders.Add(x));
            context.SaveChanges();

            context.CreditRisks.Add(new CreditRisks
            {
                Id = customers[4].Id,
                FirstName = customers[4].FirstName,
                LastName = customers[4].LastName
            });

            context.Database.OpenConnection();

            try
            {
                var tableName = context.GetTableName(typeof(CreditRisks));

                var rawSqlString = $"SET IDENTITY_INSERT dbo.{tableName} ON";

                context.Database.ExecuteSqlRaw(rawSqlString);

                context.SaveChanges();

                rawSqlString = $"SET IDENTITY_INSERT dbo.{tableName} OFF";
                context.Database.ExecuteSqlRaw(rawSqlString);
            }
            catch (System.Exception)
            {

                context.Database.CloseConnection();
            }

        }

        public static void RecreateDatabase(AutoLotEntities context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public static void ClearData(AutoLotEntities context)
        {
            ExecuteDeleteSql(context, "Orders");
            ExecuteDeleteSql(context, "Customers");
            ExecuteDeleteSql(context, "Inventory");
            ExecuteDeleteSql(context, "CreditRisks");
            ResetIdentity(context);   
        }

        private static void ExecuteDeleteSql(AutoLotEntities context, string tableName)
        {
            var rawSqlString = $"Delete from dbo.{tableName}";
            context.Database.ExecuteSqlRaw(rawSqlString);
        }

        private static void ResetIdentity(AutoLotEntities context)
        {
            var tables = new[] { "Inventory", "Orders", "Customers", "CreditRisks" };

            foreach(var item in tables)
            {
                var rawSqlString = $"DBCC CHECKIDENT (\"dbo.{item}\",RESEED,-1";
                context.Database.ExecuteSqlRaw(rawSqlString);
            }
        }
    }
}
