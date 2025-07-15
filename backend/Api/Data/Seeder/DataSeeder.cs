using Api.Data.Context;
using Api.Data.Entities;
using Api.Models.Address;
using Api.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Seeder;

public class DataSeeder(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    MendalaApiContext context)
{
    private static readonly string[] ProductNames = new[]
    {
        "HydroMax V60",
        "AquaPulse 300",
        "Thermoflow XR2",
        "HeatCore S80",
        "PumpJet Nova",
        "FlowPro T45",
        "AquaForge HZ",
        "Hydronix 500E",
        "CircuStream M8",
        "EcoTherm Vento",
        "VaporLift QX",
        "TempraFlow KX2",
        "HydroPrime L60",
        "ZenithPump A3",
        "AeroHeat Fusion",
        "Voltix H-Drive",
        "NexPump Omega",
        "PulseTherm R90",
        "ClearLift Dual",
        "TranquilHeat P5"
    };
    
    private static readonly Random Random = new Random();

    private const string Description =
        "The unit is exhibiting irregular behavior during normal operation, requiring diagnostic review and potential service." +
        "Reported malfunction affects normal operation; service is required.";
    


    public async Task SeedAsync()
    {
        await SeedRolesAsync();
        await SeedUsersAsync();
        await SeedCustomersAsync();
        await SeedProductsAsync();
        await SeedInvoicesAsync();
        await SeedIssuesAsync();
    }

    private async Task SeedRolesAsync()
    {
        string[] roles = { "Admin", "User", "Customer" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private async Task SeedUsersAsync()
    {
        var users = new[]
        {
            new { UserName = "admin_example", Email = "admin@example.com", Password = "Admin@123", Role = "Admin" },
            new { UserName = "user_example", Email = "user@example.com", Password = "User@123", Role = "User" },
            new { UserName = "customer_example", Email = "customer@example.com", Password = "Customer@123", Role = "Customer" }
        };

        foreach (var userInfo in users)
        {
            if (await userManager.FindByEmailAsync(userInfo.Email) == null)
            {
                var user = new User
                {
                    UserName = userInfo.UserName,
                    Email = userInfo.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, userInfo.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userInfo.Role);
                }
                else
                {
                    throw new Exception(
                        $"Failed to create user {userInfo.UserName}: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }

    private async Task SeedProductsAsync()
    {
        if (!await context.Products.AnyAsync())
        {
            var products = ProductNames.Select(product => new Product { Name = product, Sku = ProductNameToSku(product), })
                .ToList();
            
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }

    private async Task SeedInvoicesAsync()
    {
        if (!await context.Invoices.AnyAsync())
        {
            var user = await context.Customers.FirstAsync(c => c.Email.Equals("customer@example.com"));
            if (user == null) throw new Exception("Customer not found");
                
            var products = await context.Products.ToListAsync();
            if (products == null) throw new Exception("Products not found");
            
            var invoices = new List<Invoice>();
           
            for (var i = 0; i <= 15; i++)
            {
                var tempInv = new Invoice()
                {
                    InvoiceNumber = $"MNDL/{i}",
                    Name = user.Name + " ltd",
                    Address = new Address()
                    {
                        StreetAddress = "42 Obsidian Tower",
                        City = "Aetherport",
                        PostalCode = "49310",
                        Country = "Elaria",
                    },
                    CustomerId = user.Id
                };
                
                var productIndex = Random.Next(0, ProductNames.Length);
                var product = products.Count >= productIndex ? products[productIndex] : products[0];
                var invoicesItem = new InvoiceItem()
                {
                    Invoice = tempInv,
                    Quantity = 1,
                    Product = product,
                    PriceAtPurchase = GetPurchasePrice()
                };
                
                tempInv.InvoiceItems.Add(invoicesItem);
                
                invoices.Add(tempInv);
                
            }

            
            context.Invoices.AddRange(invoices);
            await context.SaveChangesAsync();
        }
    }
    
    private async Task SeedIssuesAsync()
    {
        if (!await context.Issues.AnyAsync())
        {
            var user = await userManager.FindByEmailAsync("admin@example.com");
            if (user == null) throw new Exception("Admin not found");
                
            var invoices = await context.Invoices.ToListAsync();
            if (invoices == null) throw new Exception("Invoices not found");
            
            var issues = new List<Issue>();
            
            foreach (var invoice in invoices)
            {
                var issueType = Random.Next(0, 1) == 0 ? IssueType.WarrantyClaim : IssueType.Repair;
                
                var tempIssue = new Issue()
                {
                    Type = issueType,
                    Title = invoice.Name + "" + issueType,
                    Description = Description,
                    InvoiceId = invoice.Id,
                    CreatorId = user.Id
                };
                
                issues.Add(tempIssue);
            }

            
            context.Issues.AddRange(issues);
            await context.SaveChangesAsync();
        }
    }

    private async Task SeedCustomersAsync()
    {
        if (!await context.Customers.AnyAsync())
        {
            var user = await userManager.FindByEmailAsync("customer@example.com");
            if (user == null) throw new Exception("Admin not found");
            
            var address = new Address()
            {
                StreetAddress = "742 Nimbus Crescent",
                City = "Aetherport",
                PostalCode = "49310",
                Country = "Elaria",
            };

            var customer = new Customer()
            {
                Name = user.UserName ?? "Dummy customer",
                Email = user.Email ?? "customer@example.com",
                Phone = "+36 1 234 5678",
                Address = address
                
            };

            
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }
    }


    private string ProductNameToSku(string productName)
    {
        var cleaned = productName.Replace(" ", "")
            .ToLowerInvariant();
        
        return cleaned.Length > 50 ? cleaned[..50] : cleaned;
    }

    private decimal GetPurchasePrice() => (decimal)(Random.NextDouble() * 900 + 100);

}