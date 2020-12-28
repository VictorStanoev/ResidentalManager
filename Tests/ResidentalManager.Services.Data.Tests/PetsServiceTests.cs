namespace ResidentalManager.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using ResidentalManager.Data;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Data.Repositories;
    using Xunit;

    public class PetsServiceTests
    {
        [Fact]
        public async Task AddAsynkShouldAddPetToPetUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Pets").Options;
            using var db = new ApplicationDbContext(options);
            db.Pets.Add(new Pet()
            {
                Breed = "Dog",
                Comment = "auauau",
                FeeId = 1,
                PropertyId = 1,
            });
            await db.SaveChangesAsync();

            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var petService = new PetsService(petRepo, feeRepo);

            await petService.CreateAsync(new Web.ViewModels.Pets.PetsInputModel
            {
                Breed = "Cat",
                Comment = "mauu",
                FeeId = 1,
                PropertyId = 1,
            });

            var pet = db.Pets.Select(x => x).Last();

            Assert.Equal("Cat", pet.Breed);
            Assert.Equal(2, db.Pets.Count());
        }

        [Fact]
        public async Task DeleteAsyncShouldDeletePetUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Pets2").Options;
            using var db = new ApplicationDbContext(options);
            db.Pets.Add(new Pet()
            {
                Breed = "Dog",
                Comment = "auauau",
                FeeId = 1,
                PropertyId = 1,
            });
            db.Pets.Add(new Pet()
            {
                Breed = "Cat",
                Comment = "mauu",
                FeeId = 1,
                PropertyId = 1,
            });
            await db.SaveChangesAsync();

            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var petService = new PetsService(petRepo, feeRepo);

            var pet = db.Pets.Select(x => x).FirstOrDefault();

            await petService.DeleteAsync(pet.Id);

            Assert.Single(db.Pets);
        }

        [Fact]
        public async Task UpdateShouldUpdatePetUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Pets3").Options;
            using var db = new ApplicationDbContext(options);
            db.Pets.Add(new Pet()
            {
                Breed = "Dog",
                Comment = "auauau",
                FeeId = 1,
                PropertyId = 1,
            });
            await db.SaveChangesAsync();

            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var petService = new PetsService(petRepo, feeRepo);

            var pet = db.Pets.Select(x => x).FirstOrDefault();

            await petService.Update(pet.Id, new Web.ViewModels.Pets.PetsInputModel
            {
                Breed = "Cat",
                Comment = "mauu",
                FeeId = 1,
                PropertyId = 1,
            });

            Assert.Single(db.Pets);
            Assert.Equal("Cat", db.Pets.Select(x => x.Breed).FirstOrDefault()); ;
        }

        [Fact]
        public async Task GetAllShouldGetAllPetUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Pets4").Options;
            using var db = new ApplicationDbContext(options);
            db.Pets.Add(new Pet()
            {
                Breed = "Dog",
                Comment = "auauau",
                FeeId = 1,
                PropertyId = 1,
            });
            db.Pets.Add(new Pet()
            {
                Breed = "Cat",
                Comment = "mauu",
                FeeId = 1,
                PropertyId = 1,
            });
            db.Pets.Add(new Pet()
            {
                Breed = "Cat",
                Comment = "dfdfdf",
                FeeId = 2,
                PropertyId = 2,
            });
            await db.SaveChangesAsync();

            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var petService = new PetsService(petRepo, feeRepo);

            var pets = petService.GetAll(1);

            Assert.Equal(2, pets.Count());
        }

        [Fact]
        public async Task GetShouldGetPetUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Pets5").Options;
            using var db = new ApplicationDbContext(options);
            db.Pets.Add(new Pet()
            {
                Breed = "Dog",
                Comment = "auauau",
                FeeId = 1,
                PropertyId = 2,
            });
            db.Pets.Add(new Pet()
            {
                Breed = "Cat",
                Comment = "mauu",
                FeeId = 1,
                PropertyId = 1,
            });
            db.Pets.Add(new Pet()
            {
                Breed = "Cat",
                Comment = "dfdfdf",
                FeeId = 2,
                PropertyId = 2,
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

            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var petService = new PetsService(petRepo, feeRepo);

            var id = db.Pets.Where(x => x.Breed == "Cat").Select(x => x.Id).FirstOrDefault();

            var pets = petService.Get(id, 1);

            Assert.Equal(2, pets.Fee.Count);
            Assert.Equal("mauu", pets.Comment);
        }

        [Fact]
        public async Task AddFeeDropDownShouldGetPetModelWithFeeDataUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Pets6").Options;
            using var db = new ApplicationDbContext(options);
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

            using var petRepo = new EfDeletableEntityRepository<Pet>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var petService = new PetsService(petRepo, feeRepo);

            var id = db.Pets.Where(x => x.Breed == "Cat").Select(x => x.Id).FirstOrDefault();

            var pets = petService.AddFeeDropDown(1);

            Assert.Equal(2, pets.Fee.Count);
            Assert.Equal("Куче", pets.Fee.Select(x => x.Name).FirstOrDefault());
        }
    }
}