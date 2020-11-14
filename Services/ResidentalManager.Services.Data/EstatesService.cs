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
    }
}
