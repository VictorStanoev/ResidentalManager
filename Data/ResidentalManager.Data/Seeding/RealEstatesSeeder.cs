namespace ResidentalManager.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Models;

    internal class RealEstatesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.RealEstates.Any())
            {
                return;
            }

            await dbContext.RealEstates.AddAsync(new RealEstate
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

            await dbContext.RealEstates.AddAsync(new RealEstate
            {
                Name = "Сердика 17",
                Region = "София-Град",
                Municipality = "Възраждане",
                Town = "София",
                PostCode = 1716,
                ResidentalArea = "жк.Сердика",
                StreetName = null,
                StreetNumber = null,
                BuildingNumber = "17",
                EntranceNumber = null,
                Floors = 16,
                Attics = true,
                Basements = true,
                Elevator = true,
                Garages = false,
            });
        }
    }
}
