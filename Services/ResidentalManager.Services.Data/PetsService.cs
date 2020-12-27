namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Web.ViewModels.Fees;
    using ResidentalManager.Web.ViewModels.Pets;

    public class PetsService : IPetsService
    {
        private readonly IDeletableEntityRepository<Pet> petsReporsitory;
        private readonly IDeletableEntityRepository<Fee> feeRepository;

        public PetsService(
            IDeletableEntityRepository<Pet> petsReporsitory,
            IDeletableEntityRepository<Fee> feeRepository)
        {
            this.petsReporsitory = petsReporsitory;
            this.feeRepository = feeRepository;
        }

        public PetsInputModel AddFeeDropDown(int realEstateId)
        {
            var model = new PetsInputModel();
            var feeCollection = this.feeRepository.AllAsNoTracking()
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => new FeeDropDown
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            model.Fee = feeCollection;

            return model;
        }

        public async Task CreateAsync(PetsInputModel inputModel)
        {
            var pet = new Pet
            {
                PropertyId = inputModel.PropertyId,
                FeeId = inputModel.FeeId,
                Comment = inputModel.Comment,
                Id = inputModel.Id,
                Breed = inputModel.Breed,
            };

            await this.petsReporsitory.AddAsync(pet);
            await this.petsReporsitory.SaveChangesAsync();
        }

        public IEnumerable<PetsViewModel> GetAll(int propertyId)
        {
            var pets = this.petsReporsitory.All()
                .Where(x => x.PropertyId == propertyId)
                .Select(x => new PetsViewModel
            {
                Breed = x.Breed,
                Comment = x.Comment,
                FeeId = x.FeeId,
                PropertyId = x.PropertyId,
                Id = x.Id,
            }).AsEnumerable();

            return pets;
        }

        public PetsViewModel Get(int id, int realEstateId)
        {
            var feeCollection = this.feeRepository.AllAsNoTracking()
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => new FeeDropDown
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            var petModel = this.petsReporsitory.All()
                .Where(x => x.Id == id)
                .Select(x => new PetsViewModel
                {
                    Breed = x.Breed,
                    Comment = x.Comment,
                    FeeId = x.FeeId,
                    PropertyId = x.PropertyId,
                    Id = x.Id,
                    Fee = feeCollection,
                }).FirstOrDefault();

            return petModel;
        }

        public async Task Update(int id, PetsInputModel model)
        {
            var pet = this.petsReporsitory.All().Where(x => x.Id == id).FirstOrDefault();
            pet.Breed = model.Breed;
            pet.Comment = model.Comment;
            pet.FeeId = model.FeeId;

            this.petsReporsitory.Update(pet);
            await this.petsReporsitory.SaveChangesAsync();
        }
    }
}
