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

    public class EstatesServiceTests
    {
        [Fact]
        public void GetAllShouldReturnCorrectNumber()
        {
            var repository = new Mock<IRepository<RealEstate>>();
            repository.Setup(r => r.All()).Returns(new List<RealEstate>
                                                        {
                                                            new RealEstate()
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
                                                            }, new RealEstate()
                                                            {
                                                                Name = "Бъкстон 35A",
                                                                Region = "София-Град",
                                                                Municipality = "Витоша",
                                                                Town = "София",
                                                                PostCode = 1618,
                                                                ResidentalArea = "жк.Бъкстон",
                                                                StreetName = "Казбек",
                                                                StreetNumber = null,
                                                                BuildingNumber = "35",
                                                                EntranceNumber = "A",
                                                                Floors = 8,
                                                                Attics = false,
                                                                Basements = true,
                                                                Elevator = true,
                                                                Garages = false,
                                                            },
                                                        }.AsQueryable());

            var service = new EstatesService(repository.Object);
            Assert.Equal(2, service.GetAll().Count());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetShouldReturnCorrectObjectUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RealEstateDbTest1").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.RealEstates.Add(new RealEstate()
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
            dbContext.RealEstates.Add(new RealEstate()
            {
                Name = "Бъкстон 35A",
                Region = "София-Град",
                Municipality = "Витоша",
                Town = "София",
                PostCode = 1618,
                ResidentalArea = "жк.Бъкстон",
                StreetName = "Казбек",
                StreetNumber = null,
                BuildingNumber = "35",
                EntranceNumber = "A",
                Floors = 8,
                Attics = false,
                Basements = true,
                Elevator = true,
                Garages = false,
            });
            await dbContext.SaveChangesAsync();

            var id = dbContext.RealEstates.Where(x => x.Name == "Бъкстон 35Б").Select(x => x.Id).FirstOrDefault();
            var id2 = dbContext.RealEstates.Where(x => x.Name == "Бъкстон 35A").Select(x => x.Id).FirstOrDefault();

            using var repository = new EfRepository<RealEstate>(dbContext);
            var service = new EstatesService(repository);
            Assert.Equal("Бъкстон 35Б", service.Get(id).Name);
            Assert.Equal("Бъкстон 35A", service.Get(id2).Name);
        }

        [Fact]
        public async Task DeleteShouldRemoveObjectUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RealEstateDbTest2").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.RealEstates.Add(new RealEstate()
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
            dbContext.RealEstates.Add(new RealEstate()
            {
                Name = "Бъкстон 35A",
                Region = "София-Град",
                Municipality = "Витоша",
                Town = "София",
                PostCode = 1618,
                ResidentalArea = "жк.Бъкстон",
                StreetName = "Казбек",
                StreetNumber = null,
                BuildingNumber = "35",
                EntranceNumber = "A",
                Floors = 8,
                Attics = false,
                Basements = true,
                Elevator = true,
                Garages = false,
            });
            await dbContext.SaveChangesAsync();

            var id = dbContext.RealEstates.Where(x => x.Name == "Бъкстон 35Б").Select(x => x.Id).FirstOrDefault();
            var id2 = dbContext.RealEstates.Where(x => x.Name == "Бъкстон 35A").Select(x => x.Id).FirstOrDefault();

            using var repository = new EfRepository<RealEstate>(dbContext);
            var service = new EstatesService(repository);

            await service.DeleteAsync(id);
            var countEstates = dbContext.RealEstates.Select(x => x.Id).Count();

            Assert.Equal(1, countEstates);
        }

        [Fact]
        public async Task CreateShouldCreateObjectUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RealEstateDbTest3").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.RealEstates.Add(new RealEstate()
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

            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<RealEstate>(dbContext);
            var service = new EstatesService(repository);

            await service.CreateAsync(new Web.ViewModels.Estates.CreateEstatesInputModel()
            {
                Name = "Нов Обект",
                Region = "София-Град",
                Municipality = "Витоша",
                Town = "София",
                PostCode = 1618,
                ResidentalArea = "жк.Бъкстон",
                StreetName = "Казбек",
                StreetNumber = null,
                BuildingNumber = "35",
                EntranceNumber = "A",
                Floors = 8,
                Attics = false,
                Basements = true,
                Elevator = true,
                Garages = false,
            });
            var countEstates = dbContext.RealEstates.Select(x => x.Id).Count();
            var name = dbContext.RealEstates.Select(x => x.Name).Last();

            Assert.Equal(2, countEstates);
            Assert.Equal("Нов Обект", name);
        }

        [Fact]
        public void UpdateShouldUpdateRealEstateUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RealEstateDbTest4").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.RealEstates.Add(new RealEstate()
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
            dbContext.SaveChangesAsync().GetAwaiter().GetResult();

            using var repository = new EfRepository<RealEstate>(dbContext);
            var service = new EstatesService(repository);

            var id = dbContext.RealEstates.Where(x => x.Name == "Бъкстон 35Б").Select(x => x.Id).FirstOrDefault();

            service.Update(id, new Web.ViewModels.Estates.CreateEstatesInputModel()
            {
                Name = "NovObekt",
                Region = "Тестов",
                Municipality = "Банкя",
                Town = "Пловдив",
                PostCode = 1111,
                ResidentalArea = "жк.Тест",
                StreetName = "Тестова",
                StreetNumber = "нова",
                BuildingNumber = "22",
                EntranceNumber = "Д",
                Floors = 5,
                Attics = true,
                Basements = false,
                Elevator = false,
                Garages = true,
            });

            var name = dbContext.RealEstates.Select(x => x.Name).FirstOrDefault();
            var region = dbContext.RealEstates.Select(x => x.Region).FirstOrDefault();
            var town = dbContext.RealEstates.Select(x => x.Town).FirstOrDefault();
            var floors = dbContext.RealEstates.Select(x => x.Floors).FirstOrDefault();
            var attics = dbContext.RealEstates.Select(x => x.Attics).FirstOrDefault();
            var garages = dbContext.RealEstates.Select(x => x.Garages).FirstOrDefault();

            Assert.Equal("NovObekt", name);
            Assert.Equal("Тестов", region);
            Assert.Equal("Пловдив", town);
            Assert.Equal(5, floors);
            Assert.True(attics);
            Assert.True(garages);
        }
    }
}
