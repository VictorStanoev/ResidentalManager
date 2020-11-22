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
        private readonly IDeletableEntityRepository<Fee> feeRepository;

        public Properties_Service(
            IDeletableEntityRepository<Property> repository,
            IDeletableEntityRepository<Fee> feeRepository)
        {
            this.repository = repository;
            this.feeRepository = feeRepository;
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
            this.repository.Delete(property);
            await this.repository.SaveChangesAsync();
        }

        public AllPropertiesViewModel Get(int id)
        {
            var fees = this.feeRepository.AllAsNoTracking().Select(x => new FeeDropDown
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

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
                     CompanyId = x.CompanyId,
                     RealEstateId = x.RealEstateId,
                     Size = x.Size,
                     Id = x.Id,
                     Fee = fees,
                     Residents = x.Residents.Count(),
                 }).OrderBy(x => x.Number).FirstOrDefault();
        }

        public CreatePropertiesInputModel AddFee()
        {
            var model = new CreatePropertiesInputModel();
            var feeCollection = this.feeRepository.AllAsNoTracking()
                .Select(x => new FeeDropDown
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            model.Fee = feeCollection;

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
                CompanyId = x.CompanyId,
                Fee = fees,
                PropertyOwnership = x.PropertyOwnership,
                PropertyType = x.PropertyType,
                Residents = x.Residents.Count(),
            }).OrderBy(x => x.Number).ToList().AsEnumerable();

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
