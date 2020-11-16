namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Services.Mapping;
    using ResidentalManager.Web.ViewModels.Estates;

    public class EstatesService : IEstatesService
    {
        private readonly IRepository<RealEstate> repository;

        public EstatesService(IRepository<RealEstate> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(CreateEstatesInputModel inputModel)
        {
            var estate = new RealEstate
            {
                Name = inputModel.Name,
                Municipality = inputModel.Municipality,
                Region = inputModel.Region,
                ResidentalArea = inputModel.ResidentalArea,
                Basements = inputModel.Basements,
                Floors = inputModel.Floors,
                BuildingNumber = inputModel.BuildingNumber,
                Attics = inputModel.Attics,
                Elevator = inputModel.Elevator,
                EntranceNumber = inputModel.EntranceNumber,
                Garages = inputModel.Garages,
                PostCode = inputModel.PostCode,
                StreetName = inputModel.StreetName,
                StreetNumber = inputModel.StreetNumber,
                Town = inputModel.Town,
            };

            await this.repository.AddAsync(estate);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var fee = this.repository.All().Where(x => x.Id == id).FirstOrDefault();
            this.repository.Delete(fee);
            await this.repository.SaveChangesAsync();
        }

        public AllEstatesViewModel Get(int id)
        {
            return this.repository.All()
                 .Where(x => x.Id == id)
                 .Select(x => new AllEstatesViewModel
                 {
                     Name = x.Name,
                     Municipality = x.Municipality,
                     ResidentalArea = x.ResidentalArea,
                     Region = x.Region,
                     Town = x.Town,
                     Floors = x.Floors,
                     Attics = x.Attics,
                     Basements = x.Basements,
                     BuildingNumber = x.BuildingNumber,
                     Elevator = x.Elevator,
                     EntranceNumber = x.EntranceNumber,
                     Garages = x.Garages,
                     Id = x.Id,
                     PostCode = x.PostCode,
                     StreetName = x.StreetName,
                     StreetNumber = x.StreetNumber,
                 }).FirstOrDefault();
        }

        public IEnumerable<AllEstatesViewModel> GetAll()
        {
            var estates = this.repository.All().Select(x => new AllEstatesViewModel
            {
                Name = x.Name,
                Floors = x.Floors,
                Basements = x.Basements,
                Id = x.Id,
                Region = x.Region,
                ResidentalArea = x.ResidentalArea,
                Attics = x.Attics,
                BuildingNumber = x.BuildingNumber,
                Elevator = x.Elevator,
                EntranceNumber = x.EntranceNumber,
                Garages = x.Garages,
                Municipality = x.Municipality,
                PostCode = x.PostCode,
                StreetName = x.StreetName,
                StreetNumber = x.StreetNumber,
                Town = x.Town,
            }).ToList().AsEnumerable();

            return estates;
        }

        public void Update(int id, CreateEstatesInputModel inputModel)
        {
            var estate = this.repository.All().Where(x => x.Id == id).FirstOrDefault();
            estate.Name = inputModel.Name;
            estate.StreetNumber = inputModel.StreetNumber;
            estate.StreetName = inputModel.StreetName;
            estate.Attics = inputModel.Attics;
            estate.Basements = inputModel.Basements;
            estate.BuildingNumber = inputModel.BuildingNumber;
            estate.Elevator = inputModel.Elevator;
            estate.EntranceNumber = inputModel.EntranceNumber;
            estate.Floors = inputModel.Floors;
            estate.Garages = inputModel.Garages;
            estate.Municipality = inputModel.Municipality;
            estate.PostCode = inputModel.PostCode;
            estate.Region = inputModel.Region;
            estate.ResidentalArea = inputModel.ResidentalArea;
            estate.Town = inputModel.Town;
            this.repository.Update(estate);
            this.repository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
