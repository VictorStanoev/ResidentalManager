namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Web.ViewModels.Fees;

    public class FeesService : IFeesService
    {
        private readonly IDeletableEntityRepository<Fee> repository;
        private readonly IDeletableEntityRepository<Property> propertyRepo;
        private readonly IDeletableEntityRepository<Pet> petRepo;
        private readonly IDeletableEntityRepository<Resident> residentRepo;

        public FeesService(
            IDeletableEntityRepository<Fee> repository,
            IDeletableEntityRepository<Property> propertyRepo,
            IDeletableEntityRepository<Pet> petRepo,
            IDeletableEntityRepository<Resident> residentRepo)
        {
            this.repository = repository;
            this.propertyRepo = propertyRepo;
            this.petRepo = petRepo;
            this.residentRepo = residentRepo;
        }

        public async Task CreateAsync(CreateFeesInputModel inputModel)
        {
            var fee = new Fee()
            {
                Name = inputModel.Name,
                Price = inputModel.Price,
                Description = inputModel.Description,
                RealEstateId = inputModel.RealEstateId,
            };

            await this.repository.AddAsync(fee);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var fee = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            var isPetFee = this.petRepo.All().Any(x => x.FeeId == id);
            var isResidentFee = this.residentRepo.All().Any(x => x.FeeId == id);
            var isPropertyFee = this.propertyRepo.All().Any(x => x.FeeId == id);

            if (!isPetFee && !isResidentFee && !isPropertyFee)
            {
                this.repository.Delete(fee);
                await this.repository.SaveChangesAsync();
            }
        }

        public FeesViewModel Get(int id)
        {
            return this.repository.All()
                .Where(x => x.Id == id)
                .Select(x => new FeesViewModel
            {
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
            }).FirstOrDefault();
        }

        public IEnumerable<FeesViewModel> GetAll(int realEstateId)
        {
            var fees = this.repository.AllAsNoTracking()
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => new FeesViewModel
            {
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                Id = x.Id,
                RealEstateId = x.RealEstateId,
            }).AsEnumerable();

            return fees;
        }

        public void Update(int id, CreateFeesInputModel inputModel)
        {
            var fee = this.repository.All().Where(x => x.Id == id).FirstOrDefault();
            fee.Name = inputModel.Name;
            fee.Price = inputModel.Price;
            fee.Description = inputModel.Description;
            this.repository.Update(fee);
            this.repository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
