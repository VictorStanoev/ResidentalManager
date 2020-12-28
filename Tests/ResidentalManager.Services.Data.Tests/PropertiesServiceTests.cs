namespace ResidentalManager.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using ResidentalManager.Data;
    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Data.Repositories;
    using Xunit;

    public class PropertiesServiceTests
    {
        [Fact]
        public async Task CreateShouldCreatePropertyUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Properties").Options;
            using var db = new ApplicationDbContext(options);
            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var estateRepo = new EfRepository<RealEstate>(db);
            var service = new Properties_Service(propRepo, resRepo, petRepo, feeRepo, estateRepo);

            await service.CreateAsync(new Web.ViewModels.Properties_.CreatePropertiesInputModel
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            var count = db.Properties.Select(x => x.Id).Count();
            var number = db.Properties.Select(x => x.Number).Last();

            Assert.Equal(2, count);
            Assert.Equal(22, number);
        }

        [Fact]
        public async Task DeleteShouldRemovePropertyUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Properties2").Options;
            using var db = new ApplicationDbContext(options);
            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            db.Properties.Add(new Property()
            {
                Floor = 2,
                Number = 55,
                Size = 66,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var estateRepo = new EfRepository<RealEstate>(db);
            var service = new Properties_Service(propRepo, resRepo, petRepo, feeRepo, estateRepo);

            var id = db.Properties.Select(x => x.Id).First();

            await service.DeleteAsync(id);

            var count = db.Properties.Select(x => x.Id).Count();
            var number = db.Properties.Select(x => x.Number).FirstOrDefault();

            Assert.Equal(1, count);
            Assert.Equal(55, number);
        }

        [Fact]
        public async Task UpdateShouldUpdatePropertyUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Properties3").Options;
            using var db = new ApplicationDbContext(options);
            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var estateRepo = new EfRepository<RealEstate>(db);
            var service = new Properties_Service(propRepo, resRepo, petRepo, feeRepo, estateRepo);

            var id = db.Properties.Select(x => x.Id).First();

            await service.Update(id, new Web.ViewModels.Properties_.CreatePropertiesInputModel
            {
                Floor = 2,
                Number = 55,
                Size = 66,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });

            var count = db.Properties.Select(x => x.Id).Count();
            var number = db.Properties.Select(x => x.Number).FirstOrDefault();
            var size = db.Properties.Select(x => x.Size).FirstOrDefault();

            Assert.Equal(55, number);
            Assert.Equal(66, size);
        }

        [Fact]
        public async Task GetAllShouldReturnAllPropertiesUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Properties4").Options;
            using var db = new ApplicationDbContext(options);
            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            db.Properties.Add(new Property()
            {
                Floor = 2,
                Number = 23,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.CommonProperty,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Attic,
                RealEstateId = 1,
            });
            db.Properties.Add(new Property()
            {
                Floor = 4,
                Number = 33,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 2,
            });
            await db.SaveChangesAsync();

            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var estateRepo = new EfRepository<RealEstate>(db);
            var service = new Properties_Service(propRepo, resRepo, petRepo, feeRepo, estateRepo);

            var id = db.Properties.Select(x => x.RealEstateId).First();

            var properties = service.GetAll(id);

            Assert.Equal(2, properties.Count());
        }

        [Fact]
        public async Task GetShouldReturnPropertyUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Properties5").Options;
            using var db = new ApplicationDbContext(options);
            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            db.Properties.Add(new Property()
            {
                Floor = 2,
                Number = 23,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.CommonProperty,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Attic,
                RealEstateId = 1,
            });
            db.Properties.Add(new Property()
            {
                Floor = 4,
                Number = 33,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 1,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 2,
            });
            await db.SaveChangesAsync();

            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var estateRepo = new EfRepository<RealEstate>(db);
            var service = new Properties_Service(propRepo, resRepo, petRepo, feeRepo, estateRepo);

            var id = db.Properties.Select(x => x.Id).First();

            var properties = service.Get(id);

            Assert.Equal(22, properties.Number);
        }

        [Fact]
        public async Task AddFeeShouldAddFeeToPropertyInputModelUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Properties6").Options;
            using var db = new ApplicationDbContext(options);
            db.RealEstates.Add(new RealEstate()
            {
                Name = "Бъкстон 35Б",
                Region = "София-Град",
                Municipality = "Витоша",
                Town = "София",
                PostCode = 1618,
                ResidentalArea = "жк.Бъкстон",
                StreetName = "Казбек",
                StreetNumber = null,
                BuildingNumber = "35",
                EntranceNumber = "Б",
                Floors = 8,
                Attics = false,
                Basements = true,
                Elevator = true,
                Garages = false,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Куче",
                Description = "Такса Куче",
                Price = 3,
                RealEstateId = 1,
            });
            db.Fees.Add(new Fee()
            {
                Name = "Котка",
                Description = "Такса Котка",
                Price = 5,
                RealEstateId = 1,
            });
            await db.SaveChangesAsync();

            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            using var estateRepo = new EfRepository<RealEstate>(db);
            var service = new Properties_Service(propRepo, resRepo, petRepo, feeRepo, estateRepo);

            var id = db.Fees.Select(x => x.RealEstateId).First();

            var properties = service.AddFee(id);

            Assert.Equal("Куче", properties.Fee.Select(x => x.Name).FirstOrDefault());
            Assert.Equal(8, properties.RealEstateFloors);
        }
    }
}
