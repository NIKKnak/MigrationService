using FinanceService.Controllers;
using FinanceService.Data;
using FinanceService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Tests
{
    public class CurrencyControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var context = new AppDbContext(options);

            context.Currency.AddRange(
                new Currency { Id = 1, Name = "USD", Rate = 90.5m },
                new Currency { Id = 2, Name = "EUR", Rate = 99.9m }
            );
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void GetCurrencies()
        {
            var context = GetInMemoryDbContext();
            var controller = new CurrencyController(context);

            var result = controller.GetCurrencies();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var currencies = Assert.IsAssignableFrom<IEnumerable<Currency>>(okResult.Value);

            Assert.Equal(2, currencies.Count());
        }
    }
}
