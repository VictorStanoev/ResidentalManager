namespace ResidentalManager.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using ResidentalManager.Data;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Data.Repositories;
    using Xunit;

    public class ResidentsServiceTest
    {
        [Fact]
        public async Task CreateShouldCreateResidentUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Residents").Options;
            using var db = new ApplicationDbContext(options);
            db.Residents.Add(new Resident()
            {
                FirstName = "Pesho",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });
            await db.SaveChangesAsync();

            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var service = new ResidentsService(resRepo, feeRepo);

            var resident = service.CreateAsync(new Web.ViewModels.Residents.CreateResidentsInputModel
            {
                FirstName = "Ivanka",
                MiddleName = "Peshova",
                LastName = "Pesheva",
                DateOfBirth = new DateTime(2066, 01, 01),
                Email = "pesha@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Female,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.ConnectedPerson,
            });

            var count = db.Residents.Select(x => x.Id).Count();
            var added = db.Residents.Select(x => x).Last();

            Assert.Equal(2, count);
            Assert.Equal("Ivanka", added.FirstName);
        }

        [Fact]
        public async Task DeleteShouldDeleteResidentUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Residents2").Options;
            using var db = new ApplicationDbContext(options);
            db.Residents.Add(new Resident()
            {
                FirstName = "Pesho",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });
            db.Residents.Add(new Resident()
            {
                FirstName = "Ivanka",
                MiddleName = "Peshova",
                LastName = "Pesheva",
                DateOfBirth = new DateTime(2066, 01, 01),
                Email = "pesha@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Female,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.ConnectedPerson,
            });
            await db.SaveChangesAsync();

            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var service = new ResidentsService(resRepo, feeRepo);

            var id = db.Residents.Select(x => x.Id).First();

            await service.DeleteAsync(id);

            var resident = db.Residents.Select(x => x).Last();

            Assert.Single(db.Residents);
            Assert.Equal("Ivanka", resident.FirstName);
        }

        [Fact]
        public async Task AddFeeShouldAddFeesForDropDownToResidentInputModelUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Residents3").Options;
            using var db = new ApplicationDbContext(options);
            db.Residents.Add(new Resident()
            {
                FirstName = "Pesho",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
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

            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var service = new ResidentsService(resRepo, feeRepo);

            var model = service.AddFee();

            Assert.Equal("Куче", model.Fee.Select(x => x.Name).First());
            Assert.Equal("Котка", model.Fee.Select(x => x.Name).Last());
        }

        [Fact]
        public async Task UpdateShouldUpdateResidentUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Residents4").Options;
            using var db = new ApplicationDbContext(options);
            db.Residents.Add(new Resident()
            {
                FirstName = "Pesho",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });
            await db.SaveChangesAsync();

            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var service = new ResidentsService(resRepo, feeRepo);

            var id = db.Residents.Select(x => x.Id).FirstOrDefault();

            var model = service.Update(id, new Web.ViewModels.Residents.CreateResidentsInputModel
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 02, 23),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });

            var resident = db.Residents.Where(x => x.Id == id).Select(x => x).FirstOrDefault();

            Assert.Equal("Ivan", resident.FirstName);
            Assert.Single(db.Residents);
        }

        [Fact]
        public async Task GetAllShouldReturnAllResidentForPropertyUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Residents5").Options;
            using var db = new ApplicationDbContext(options);
            db.Residents.Add(new Resident()
            {
                FirstName = "Pesho",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });
            db.Residents.Add(new Resident()
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 02, 23),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });

            await db.SaveChangesAsync();

            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var service = new ResidentsService(resRepo, feeRepo);

            var id = db.Residents.Select(x => x.Id).FirstOrDefault();

            var residents = service.GetAll(1);

            Assert.Equal(2, residents.Count());
        }

        [Fact]
        public async Task GetShouldReturnResidentUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Residents6").Options;
            using var db = new ApplicationDbContext(options);
            db.Residents.Add(new Resident()
            {
                FirstName = "Pesho",
                MiddleName = "Peshov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 12, 01),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });
            db.Residents.Add(new Resident()
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Peshev",
                DateOfBirth = new DateTime(2020, 02, 23),
                Email = "pesho@mail.com",
                Telephone = "099223322",
                Gender = ResidentalManager.Data.Models.Enum.Gender.Male,
                FeeId = 1,
                PropertyId = 1,
                ResidentType = ResidentalManager.Data.Models.Enum.ResidentType.Owner,
            });

            await db.SaveChangesAsync();

            using var resRepo = new EfDeletableEntityRepository<Resident>(db);
            using var feeRepo = new EfDeletableEntityRepository<Fee>(db);
            var service = new ResidentsService(resRepo, feeRepo);

            var id = db.Residents.Select(x => x.Id).FirstOrDefault();

            var residents = service.Get(id);

            Assert.Equal("Pesho", residents.FirstName);
            Assert.Equal("Peshev", residents.LastName);
        }
    }
}
