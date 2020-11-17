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

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public AllPropertiesViewModel Get(int id)
        {
            throw new System.NotImplementedException();
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
            }).ToList().AsEnumerable();

            return properties;
        }

        public void Update(int id, CreatePropertiesInputModel inputModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
