using CrudTestApplication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudTestApplication.Infrastructure.Data
{
    public class CrudTestApplicationContextSeed
    {
        public static async Task SeedAsync(CrudTestApplicationContext aspnetrunContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // TODO: Only run this if using a real database
                // aspnetrunContext.Database.Migrate();
                // aspnetrunContext.Database.EnsureCreated();

                if (!aspnetrunContext.Customers.Any())
                {
                    aspnetrunContext.Customers.AddRange(GetPreconfiguredCustomers());
                    await aspnetrunContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CrudTestApplicationContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(aspnetrunContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }


        private static IEnumerable<Customer> GetPreconfiguredCustomers()
        {
            return new List<Customer>()
            {
                new Customer() { FirstName = "robert", LastName = "kiosaki" , DateOfBirth = DateTime.Now , PhoneNumber = "00446565236", Email = "robert@gmail.com", BankAccountNumber = "15679769771654" },
                new Customer() { FirstName = "justin", LastName = "justin" , DateOfBirth = DateTime.Now , PhoneNumber = "0044652335556", Email = "justin@gmail.com", BankAccountNumber = "2352367976723" },
                new Customer() { FirstName = "leonard", LastName = "leonard", DateOfBirth = DateTime.Now , PhoneNumber = "004463623656", Email = "leonard@gmail.com", BankAccountNumber = "156659691654" }
            };
        }
    }
}
