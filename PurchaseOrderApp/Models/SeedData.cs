using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PurchaseOrderApp.Data;
using PurchaseOrderApp.Models;
using System;
using System.Linq;
using System.Collections;

namespace PurchaseOrderApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new PurchaseOrderAppContext(serviceProvider.GetRequiredService<DbContextOptions<PurchaseOrderAppContext>>());
            if (context.PurchaseOrder.Any())
            {
                return; //DB has been seeded
            }

            context.User.AddRange(

                new User
                {
                    FirstName = "Lola",
                    LastName = "Belinski",
                    RegistrationDate = DateTime.Parse("2005-09-01")
                },

                new User
                {
                    FirstName = "Ella",
                    LastName = "Makowiak",
                    RegistrationDate = DateTime.Parse("2012-12-08")
                },

                new User
                {
                    FirstName = "Artur",
                    LastName = "Pirozhkov",
                    RegistrationDate = DateTime.Parse("2018-01-10")
                },

                new User
                {
                    FirstName = "Igor",
                    LastName = "Chernov",
                    RegistrationDate = DateTime.Parse("2021-05-11")
                }

                );
            context.SaveChanges();


            context.PurchaseOrder.AddRange(
                new PurchaseOrder
                {
                    Name = Guid.NewGuid().ToString(),
                    CreationDate = DateTime.Parse("2005-09-01"),
                    UserID = 1,
                    TotalAmount = 5000,
                    Status = Status.DRAFT
                },

                new PurchaseOrder
                {
                    Name = Guid.NewGuid().ToString(),
                    CreationDate = DateTime.Parse("2012-12-08"),
                    UserID = 2,
                    TotalAmount = 3000,
                    Status = Status.DRAFT
                },

                new PurchaseOrder
                {
                    Name = Guid.NewGuid().ToString(),
                    CreationDate = DateTime.Parse("2018-01-10"),
                    UserID = 3,
                    TotalAmount = 500,
                    Status = Status.DRAFT
                }
                );
            context.SaveChanges();

            context.LineItem.AddRange(
                new LineItem
                {
                    Name = "Iphone",
                    Quantity = 2,
                    Price = 500,
                    PurchaseOrderID = 1
                },

                new LineItem
                {
                    Name = "MacBook",
                    Quantity = 4,
                    Price = 1000,
                    PurchaseOrderID = 1
                },

                new LineItem
                {
                    Name = "Ipad",
                    Quantity = 2,
                    Price = 1500,
                    PurchaseOrderID = 2
                },

                new LineItem
                {
                    Name = "Iphone",
                    Quantity = 1,
                    Price = 500,
                    PurchaseOrderID = 3
                }
                );
            context.SaveChanges();
        }
    }
}
