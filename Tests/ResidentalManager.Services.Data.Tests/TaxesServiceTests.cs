using Microsoft.EntityFrameworkCore;
using ResidentalManager.Data;
using ResidentalManager.Data.Models;
using ResidentalManager.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ResidentalManager.Services.Data.Tests
{
    public class TaxesServiceTests
    {
        [Fact]
        public async Task GenerateTaxesShouldCreateTaxesForAllPropertiesUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Taxes").Options;
            using var db = new ApplicationDbContext(options);
            db.Fees.Add(new Fee() { Name = "Животно", Description = "Такса Животно", Price = 2, RealEstateId = 1, });
            db.Fees.Add(new Fee() { Name = "Апартамент", Description = "Такса Апартамент", Price = 1, RealEstateId = 1, });
            db.Fees.Add(new Fee() { Name = "Живущ", Description = "Такса Живущ", Price = 4, RealEstateId = 1, });

            db.Properties.Add(new Property() { Floor = 1,  Number = 22, Size = 55, PercentageCommonParts = 34, FeeId = 2,  PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private, PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment, RealEstateId = 1,
            });
            db.Pets.Add(new Pet() { Breed = "Dog", Comment = "auauau", FeeId = 1, PropertyId = 1, });
            db.Pets.Add(new Pet() { Breed = "Dog", Comment = "aaa", FeeId = 1, PropertyId = 1, });

            db.Residents.Add(new Resident()
            {
                FirstName = "Pesho",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 3,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });

            db.Residents.Add(new Resident()
            {
                FirstName = "Pesha",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 3,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });
            await db.SaveChangesAsync();

            using var taxRepo = new EfDeletableEntityRepository<Tax>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var realEstateRepo = new EfRepository<RealEstate>(db);
            var taxService = new TaxesService(taxRepo, propRepo, realEstateRepo);

            await taxService.GenerateTaxes(1, new Web.ViewModels.Taxes.TaxesGenerateInputModel
            {
                Month = ResidentalManager.Data.Models.Enum.Month.December,
                Year = 2020,
            });

            var singleTax = db.Taxes.Where(x => x.Id == 1).FirstOrDefault();
            Assert.Equal(13, singleTax.Total);
        }

        [Fact]
        public async Task PayShouldMakeTaxesPayedUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Taxes2").Options;
            using var db = new ApplicationDbContext(options);

            db.Taxes.Add(new Tax()
            {
                PropertyId = 1,
                RealEstateId = 1,
                Month = ResidentalManager.Data.Models.Enum.Month.January,
                Year = 2020,
                IsPaid = false,
                PetTax = 2,
                ResidentsTax = 2,
                PropertyTax = 2,
                Total = 6,
            });
            await db.SaveChangesAsync();

            using var taxRepo = new EfDeletableEntityRepository<Tax>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var realEstateRepo = new EfRepository<RealEstate>(db);
            var taxService = new TaxesService(taxRepo, propRepo, realEstateRepo);

            await taxService.Pay(1);

            var singleTax = db.Taxes.Where(x => x.Id == 1).FirstOrDefault();
            Assert.True(singleTax.IsPaid);
        }

        [Fact]
        public async Task ReversePaymentMakeTaxesUnpayedUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Taxes3").Options;
            using var db = new ApplicationDbContext(options);

            db.Taxes.Add(new Tax()
            {
                PropertyId = 1,
                RealEstateId = 1,
                Month = ResidentalManager.Data.Models.Enum.Month.January,
                Year = 2020,
                IsPaid = true,
                PetTax = 2,
                ResidentsTax = 2,
                PropertyTax = 2,
                Total = 6,
            });
            await db.SaveChangesAsync();

            using var taxRepo = new EfDeletableEntityRepository<Tax>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var realEstateRepo = new EfRepository<RealEstate>(db);
            var taxService = new TaxesService(taxRepo, propRepo, realEstateRepo);

            await taxService.ReversePayment(1);

            var singleTax = db.Taxes.Where(x => x.Id == 1).FirstOrDefault();
            Assert.False(singleTax.IsPaid);
        }

        [Fact]
        public async Task UpdateTaxeShouldUpdateTaxesForSinglePropertieUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Taxes4").Options;
            using var db = new ApplicationDbContext(options);
            db.Fees.Add(new Fee() { Name = "Животно", Description = "Такса Животно", Price = 2, RealEstateId = 1, });
            db.Fees.Add(new Fee() { Name = "Апартамент", Description = "Такса Апартамент", Price = 1, RealEstateId = 1, });
            db.Fees.Add(new Fee() { Name = "Живущ", Description = "Такса Живущ", Price = 4, RealEstateId = 1, });

            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 2,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            db.Pets.Add(new Pet() { Breed = "Dog", Comment = "auauau", FeeId = 1, PropertyId = 1, });
            db.Pets.Add(new Pet() { Breed = "Dog", Comment = "aaa", FeeId = 1, PropertyId = 1, });

            db.Residents.Add(new Resident()
            {
                FirstName = "Pesho",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 3,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });

            db.Residents.Add(new Resident()
            {
                FirstName = "Pesha",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 3,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });
            await db.SaveChangesAsync();

            using var taxRepo = new EfDeletableEntityRepository<Tax>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var realEstateRepo = new EfRepository<RealEstate>(db);
            var taxService = new TaxesService(taxRepo, propRepo, realEstateRepo);

            await taxService.GenerateTaxes(1, new Web.ViewModels.Taxes.TaxesGenerateInputModel
            {
                Month = ResidentalManager.Data.Models.Enum.Month.December,
                Year = 2020,
            });

            var pet = db.Pets.Where(x => x.Id == 1).FirstOrDefault();

            db.Pets.Remove(pet);
            await db.SaveChangesAsync();

            await taxService.UpdateTax(1);

            var singleTax = db.Taxes.Where(x => x.Id == 1).FirstOrDefault();
            Assert.Equal(11, singleTax.Total);
        }

        [Fact]
        public async Task GetAllEstateTaxesShouldReturnAllTaxesForRealEstateUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Taxes6").Options;
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
            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 2,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });
            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 23,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 2,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });

            db.Taxes.Add(new Tax()
            {
                PropertyId = 1,
                RealEstateId = 1,
                Month = ResidentalManager.Data.Models.Enum.Month.January,
                Year = 2020,
                IsPaid = true,
                PetTax = 2,
                ResidentsTax = 2,
                PropertyTax = 2,
                Total = 6,
            });
            db.Taxes.Add(new Tax()
            {
                PropertyId = 1,
                RealEstateId = 1,
                Month = ResidentalManager.Data.Models.Enum.Month.February,
                Year = 2020,
                IsPaid = true,
                PetTax = 2,
                ResidentsTax = 2,
                PropertyTax = 2,
                Total = 6,
            });
            db.Taxes.Add(new Tax()
            {
                PropertyId = 2,
                RealEstateId = 1,
                Month = ResidentalManager.Data.Models.Enum.Month.February,
                Year = 2020,
                IsPaid = true,
                PetTax = 2,
                ResidentsTax = 2,
                PropertyTax = 2,
                Total = 6,
            });
            await db.SaveChangesAsync();

            using var taxRepo = new EfDeletableEntityRepository<Tax>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var realEstateRepo = new EfRepository<RealEstate>(db);
            var taxService = new TaxesService(taxRepo, propRepo, realEstateRepo);

            var taxes = taxService.GetAllEstateTaxes(1, 1);

            Assert.Equal(2, taxes.Taxes.Select(x => x).Count());
        }

        [Fact]
        public async Task GetAllPropertyTaxesShouldReturnAllTaxesForPropertyUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Taxes7").Options;
            using var db = new ApplicationDbContext(options);

            db.Properties.Add(new Property()
            {
                Floor = 1,
                Number = 22,
                Size = 55,
                PercentageCommonParts = 34,
                FeeId = 2,
                PropertyOwnership = ResidentalManager.Data.Models.Enum.PropertyOwnership.Private,
                PropertyType = ResidentalManager.Data.Models.Enum.PropertyType.Apartment,
                RealEstateId = 1,
            });

            db.Taxes.Add(new Tax()
            {
                PropertyId = 1,
                RealEstateId = 1,
                Month = ResidentalManager.Data.Models.Enum.Month.January,
                Year = 2020,
                IsPaid = true,
                PetTax = 2,
                ResidentsTax = 2,
                PropertyTax = 2,
                Total = 6,
            });
            db.Taxes.Add(new Tax()
            {
                PropertyId = 1,
                RealEstateId = 1,
                Month = ResidentalManager.Data.Models.Enum.Month.February,
                Year = 2020,
                IsPaid = true,
                PetTax = 2,
                ResidentsTax = 2,
                PropertyTax = 2,
                Total = 6,
            });
            db.Taxes.Add(new Tax()
            {
                PropertyId = 2,
                RealEstateId = 1,
                Month = ResidentalManager.Data.Models.Enum.Month.February,
                Year = 2020,
                IsPaid = true,
                PetTax = 2,
                ResidentsTax = 2,
                PropertyTax = 2,
                Total = 6,
            });
            await db.SaveChangesAsync();

            using var taxRepo = new EfDeletableEntityRepository<Tax>(db);
            using var propRepo = new EfDeletableEntityRepository<Property>(db);
            using var realEstateRepo = new EfRepository<RealEstate>(db);
            var taxService = new TaxesService(taxRepo, propRepo, realEstateRepo);

            var taxes = taxService.GetAllPropertyTaxes(1, 1, 1);

            Assert.Equal(2, taxes.Taxes.Select(x => x).Count());
        }
    }
}
