namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Web.ViewModels.Fees;
    using ResidentalManager.Web.ViewModels.Properties_;

    public class Properties_Service : IProperties_Service
    {
        private readonly IDeletableEntityRepository<Property> repository;
        private readonly IDeletableEntityRepository<Resident> residentRepo;
        private readonly IDeletableEntityRepository<Pet> petRepo;
        private readonly IDeletableEntityRepository<Fee> feeRepository;
        private readonly IRepository<RealEstate> realEstateRepository;

        public Properties_Service(
            IDeletableEntityRepository<Property> propertyRepo,
            IDeletableEntityRepository<Resident> residentRepo,
            IDeletableEntityRepository<Pet> petRepo,
            IDeletableEntityRepository<Fee> feeRepository,
            IRepository<RealEstate> realEstateRepository)
        {
            this.repository = propertyRepo;
            this.residentRepo = residentRepo;
            this.petRepo = petRepo;
            this.feeRepository = feeRepository;
            this.realEstateRepository = realEstateRepository;
        }

        public async Task CreateAsync(CreatePropertiesInputModel inputModel)
        {
            var property = new Property()
            {
                Number = inputModel.Number,
                Floor = inputModel.Floor,
                Size = inputModel.Size,
                FeeId = inputModel.FeeId,
                PercentageCommonParts = inputModel.PercentageCommonParts,
                PropertyOwnership = inputModel.PropertyOwnership,
                PropertyType = inputModel.PropertyType,
                RealEstateId = inputModel.RealEstateId,
            };

            await this.repository.AddAsync(property);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var property = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            var isResidents = this.residentRepo.All().Any(x => x.PropertyId == id);
            var isPets = this.petRepo.All().Any(x => x.PropertyId == id);
            var isFee = this.feeRepository.All().Any(x => x.RealEstateId == id);

            if (!isResidents && !isPets && !isFee)
            {
                this.repository.Delete(property);
                await this.repository.SaveChangesAsync();
            }
        }

        public AllPropertiesViewModel Get(int id)
        {
            var fees = this.feeRepository.AllAsNoTracking().Select(x => new FeeDropDown
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var realEstateFloors = this.repository.All()
                .Where(x => x.Id == id)
                .Select(x => x.RealEstate.Floors)
                .FirstOrDefault();

            return this.repository.AllAsNoTracking()
                 .Where(x => x.Id == id)
                 .Select(x => new AllPropertiesViewModel
                 {
                     Number = x.Number,
                     Floor = x.Floor,
                     PercentageCommonParts = x.PercentageCommonParts,
                     PropertyOwnership = x.PropertyOwnership,
                     FeeId = x.FeeId,
                     PropertyType = x.PropertyType,
                     RealEstateId = x.RealEstateId,
                     Size = x.Size,
                     Id = x.Id,
                     Fee = fees,
                     RealEstateFloors = realEstateFloors,
                     Residents = x.Residents.Count(),
                     Pets = x.Pets.Count(),
                 }).OrderByDescending(x => x.Number).FirstOrDefault();
        }

        public CreatePropertiesInputModel AddFee(int realEstateId)
        {
            var model = new CreatePropertiesInputModel();
            var feeCollection = this.feeRepository
                .AllAsNoTracking()
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => new FeeDropDown
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            model.Fee = feeCollection;

            model.RealEstateFloors = this.realEstateRepository
                .All()
                .Where(x => x.Id == realEstateId)
                .Select(x => x.Floors)
                .FirstOrDefault();

            return model;
        }

        public IEnumerable<AllPropertiesViewModel> GetAll(int realEstateId)
        {
            var fees = this.feeRepository.AllAsNoTracking().Select(x => new FeeDropDown
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var properties = this.repository.AllAsNoTracking()
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => new AllPropertiesViewModel
            {
                Id = x.Id,
                RealEstateId = x.RealEstateId,
                Size = x.Size,
                Floor = x.Floor,
                Number = x.Number,
                PercentageCommonParts = x.PercentageCommonParts,
                Fee = fees,
                PropertyOwnership = x.PropertyOwnership,
                PropertyType = x.PropertyType,
                Residents = x.Residents.Count(),
                Pets = x.Pets.Count(),
            }).OrderByDescending(x => x.Number).ToList().AsEnumerable();

            return properties;
        }

        public async Task Update(int id, CreatePropertiesInputModel inputModel)
        {
            var fee = this.repository.All().Where(x => x.Id == id).FirstOrDefault();
            fee.Number = inputModel.Number;
            fee.Floor = inputModel.Floor;
            fee.FeeId = inputModel.FeeId;
            fee.PercentageCommonParts = inputModel.PercentageCommonParts;
            fee.PropertyOwnership = inputModel.PropertyOwnership;
            fee.PropertyType = inputModel.PropertyType;
            fee.RealEstateId = inputModel.RealEstateId;
            fee.Size = inputModel.Size;
            this.repository.Update(fee);
            await this.repository.SaveChangesAsync();
        }
    }
}
