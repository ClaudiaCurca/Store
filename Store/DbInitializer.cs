﻿using Microsoft.Extensions.DependencyInjection;
using Store.Authentication;
using Store.Data;
using Store.Models;

namespace Store
{
    public class DbInitializer
    {


public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<StoreContext>();
                context.Database.EnsureCreated();
                if (!context.Products.Any()) 
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Id = 1,
                            Name = "Test",
                            Price = 1
                        },
                        new Product()
                        {
                            Id= 2, 
                            Name = "Test",
                            Price = 1
                        },
                        new Product()
                        {
                            Id=3,
                            Name = "Test",
                            Price = 1
                        }
                    });
                    context.SaveChanges();
                }


                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();

                if (!dbContext.Users.Any())
                {
                    dbContext.Users.AddRange(new List<AppUser>()
                    {
                        new AppUser()
                        {
                            FirstName = "Claudia",
                            LastName = "Curca",
                 
                        }
                    });
                }
            }
            
              
            

        }
    }
}
