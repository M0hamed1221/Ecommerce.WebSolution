using Domain.Contracts;
using Domain.Models.Identity;
using Domain.Models.Order;
using Domain.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
   public class DbIntializer(StoreDbContext _storeDbContext
                             ,UserManager<ApplicationUser> _userManager
                             ,RoleManager<IdentityRole> _roleManager) : IDbIntitializer
    {
         
        public async Task IntializeAsync()
        {
            /*InDeployment*/
            //if( (await _storeDbContext.Database.GetPendingMigrationsAsync()).Any())
            //{
            //    await _storeDbContext.Database.MigrateAsync();
            //}
            /*In Develpment*/
            try
            {
                if (!_storeDbContext.Set<ProductBrand>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                    if (brands is not null && brands.Any())
                    {
                        _storeDbContext.Set<ProductBrand>().AddRange(brands);
                        await _storeDbContext.SaveChangesAsync();
                    }
                }
                if (!_storeDbContext.Set<DeliveryMethod>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\DeliveryMethods.json");
                    var DeliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);
                    if (DeliveryMethod is not null && DeliveryMethod.Any())
                    {
                        _storeDbContext.Set<DeliveryMethod>().AddRange(DeliveryMethod);
                        await _storeDbContext.SaveChangesAsync();
                    }
                }
                if (!_storeDbContext.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(data);
                    if (types is not null && types.Any())
                    {
                        _storeDbContext.Set<ProductType>().AddRange(types);
                        await _storeDbContext.SaveChangesAsync();
                    }
                }
                if (!_storeDbContext.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(data);
                    if (product is not null && product.Any())
                    {
                        _storeDbContext.Set<Product>().AddRange(product);
                        await _storeDbContext.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
        }

        public async Task IntializeIdentityAsync()
        {
            /*InDeployment*/
            //if( (await _storeDbContext.Database.GetPendingMigrationsAsync()).Any())
            //{
            //    await _storeDbContext.Database.MigrateAsync();
            //}
            /*In Develpment*/
            try
            {
                if (!_roleManager.Roles .Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));


                }
                if (!_userManager.Users.Any())
                {
                    var superAdminUser = new ApplicationUser()
                    {
                        DisplyName = "Super Admin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",

                    };
                    var AdminUser = new ApplicationUser()
                    {
                        DisplyName = "Admin",
                        UserName = "admin",
                        Email = "Admin@gmail.com",

                    };
                    await _userManager.CreateAsync(superAdminUser, "P@swword");
                    await _userManager.CreateAsync(AdminUser, "password");

                    await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                    await _userManager.AddToRoleAsync(superAdminUser, "Admin");

                }

            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
        }
    }
}
