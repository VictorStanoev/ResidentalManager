namespace ResidentalManager.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using ResidentalManager.Data;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Data.Repositories;
    using Xunit;

    public class FeesServiceTests
    {
        [Fact]
        public async Task AddAsynkShouldAddFeeUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Expences").Options;
            using var db = new ApplicationDbContext(options);
            db.Fees.Add(new Fee()
            {
                Name = "Животно",
                Description = "Такса Животно",
                Price = 3,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Апартамент",
                Description = "Такса Апартамент",
                Price = 2,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            var feeService = new FeesService(feeRepo, propRepo, petRepo, resRepo);

            await feeService.CreateAsync(new Web.ViewModels.Fees.CreateFeesInputModel
            {
                Name = "Живущ",
                Description = "Такса Живущ",
                Price = 5,
                RealEstateId = 1,
            });

            Assert.Equal(3, db.Fees.Count());
        }

        [Fact]
        public async Task DeleteAsyncShuldDeleteFeeUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Expences2").Options;
            using var db = new ApplicationDbContext(options);
            db.Fees.Add(new Fee()
            {
                Name = "Животно",
                Description = "Такса Животно",
                Price = 3,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Апартамент",
                Description = "Такса Апартамент",
                Price = 2,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Живущ",
                Description = "Такса Живущ",
                Price = 2,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            var feeService = new FeesService(feeRepo, propRepo, petRepo, resRepo);

            await feeService.DeleteAsync(1);

            Assert.Equal(2, db.Fees.Count());
        }

        [Fact]
        public async Task GetShouldGetFeeByIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Expences3").Options;
            using var db = new ApplicationDbContext(options);
            db.Fees.Add(new Fee()
            {
                Name = "Животно",
                Description = "Такса Животно",
                Price = 3,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Апартамент",
                Description = "Такса Апартамент",
                Price = 2,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Живущ",
                Description = "Такса Живущ",
                Price = 2,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            var feeService = new FeesService(feeRepo, propRepo, petRepo, resRepo);

            var fee = feeService.Get(2);

            Assert.Equal("Такса Апартамент", fee.Description);
        }

        [Fact]
        public async Task GetAllShouldGetAllFeeByIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Expences4").Options;
            using var db = new ApplicationDbContext(options);
            db.Fees.Add(new Fee()
            {
                Name = "Животно",
                Description = "Такса Животно",
                Price = 3,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Апартамент",
                Description = "Такса Апартамент",
                Price = 2,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Живущ",
                Description = "Такса Живущ",
                Price = 2,
                RealEstateId = 2,
            });
            await db.SaveChangesAsync();

            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            var feeService = new FeesService(feeRepo, propRepo, petRepo, resRepo);

            var fee = feeService.GetAll(1);

            Assert.Equal(2, fee.Count());
        }

        [Fact]
        public async Task UpdateShouldUpdateFeeByIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Expences5").Options;
            using var db = new ApplicationDbContext(options);
            db.Fees.Add(new Fee()
            {
                Name = "Животно",
                Description = "Такса Животно",
                Price = 3,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Апартамент",
                Description = "Такса Апартамент",
                Price = 2,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Живущ",
                Description = "Такса Живущ",
                Price = 2,
                RealEstateId = 2,
            });
            await db.SaveChangesAsync();

            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            var feeService = new FeesService(feeRepo, propRepo, petRepo, resRepo);

            feeService.Update(1, new Web.ViewModels.Fees.CreateFeesInputModel
            {
                Name = "Животно",
                Description = "Такса Животно",
                Price = 10,
                RealEstateId = 1,
            });

            var singleFee = db.Fees.Where(x => x.Id == 1).FirstOrDefault();
            Assert.Equal(10, singleFee.Price);
        }
    }
}
