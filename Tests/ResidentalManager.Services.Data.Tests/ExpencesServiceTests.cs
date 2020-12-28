namespace ResidentalManager.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using ResidentalManager.Data;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Data.Repositories;
    using Xunit;

    public class ExpencesServiceTests
    {
        [Fact]
        public async Task AddAsynkShouldAddExpenceUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Expences").Options;
            using var db = new ApplicationDbContext(options);
            db.RealEstateExpences.Add(new RealEstateExpence()
            {
                Year = 2020,
                Month = ResidentalManager.Data.Models.Enum.Month.April,
                Amount = 100.30M,
                Description = "fasdfasd",
                ExpenceType = ResidentalManager.Data.Models.Enum.ExpenceType.CashierBill,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var expenceRepo = new EfDeletableEntityRepository<RealEstateExpence>(db);
            var expenceService = new ExpencesService(expenceRepo);

            await expenceService.CreateAsync(1, new Web.ViewModels.Expences.CreateExpencesInputModel
            {
                Year = 2021,
                Month = ResidentalManager.Data.Models.Enum.Month.May,
                Amount = 220M,
                Description = "aaaaa",
                ExpenceType = ResidentalManager.Data.Models.Enum.ExpenceType.CleanerBill,
                RealEstateId = 1,
            });

            var ex = db.RealEstateExpences.Select(x => x).Last();

            Assert.Equal(2021, ex.Year);
            Assert.Equal(2, db.RealEstateExpences.Count());
        }

        [Fact]
        public async Task GetAllShouldGetAllExpencesForCurrentRealEstateUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Expences2").Options;
            using var db = new ApplicationDbContext(options);
            db.RealEstateExpences.Add(new RealEstateExpence()
            {
                Year = 2020,
                Month = ResidentalManager.Data.Models.Enum.Month.April,
                Amount = 100.30M,
                Description = "fasdfasd",
                ExpenceType = ResidentalManager.Data.Models.Enum.ExpenceType.CashierBill,
                RealEstateId = 1,
            });
            db.RealEstateExpences.Add(new RealEstateExpence()
            {
                Year = 2021,
                Month = ResidentalManager.Data.Models.Enum.Month.May,
                Amount = 220M,
                Description = "aaaaa",
                ExpenceType = ResidentalManager.Data.Models.Enum.ExpenceType.CleanerBill,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var expenceRepo = new EfDeletableEntityRepository<RealEstateExpence>(db);
            var expenceService = new ExpencesService(expenceRepo);

            var expences = expenceService.GetAll(1, 1);

            var ex = db.RealEstateExpences.Select(x => x).Last();

            Assert.Equal(2, expences.Expences.Count());
            Assert.Equal(2021, ex.Year);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteExpenceUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Expences3").Options;
            using var db = new ApplicationDbContext(options);
            db.RealEstateExpences.Add(new RealEstateExpence()
            {
                Year = 2020,
                Month = ResidentalManager.Data.Models.Enum.Month.April,
                Amount = 100.30M,
                Description = "fasdfasd",
                ExpenceType = ResidentalManager.Data.Models.Enum.ExpenceType.CashierBill,
                RealEstateId = 1,
            });
            db.RealEstateExpences.Add(new RealEstateExpence()
            {
                Year = 2021,
                Month = ResidentalManager.Data.Models.Enum.Month.May,
                Amount = 220M,
                Description = "aaaaa",
                ExpenceType = ResidentalManager.Data.Models.Enum.ExpenceType.CleanerBill,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var expenceRepo = new EfDeletableEntityRepository<RealEstateExpence>(db);
            var expenceService = new ExpencesService(expenceRepo);

            await expenceService.DeleteAsync(1);

            Assert.Single(db.RealEstateExpences);
        }
    }
}
