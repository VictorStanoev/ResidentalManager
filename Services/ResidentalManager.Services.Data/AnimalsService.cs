namespace ResidentalManager.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Services.Mapping;
    using ResidentalManager.Web.ViewModels.Animals;
    using ResidentalManager.Web.ViewModels.Fees;

    public class AnimalsService : IAnimalsService
    {
        private readonly IDeletableEntityRepository<Animal> animalsReporsitory;
        private readonly IDeletableEntityRepository<Fee> feeRepository;

        public AnimalsService(
            IDeletableEntityRepository<Animal> animalsReporsitory,
            IDeletableEntityRepository<Fee> feeRepository)
        {
            this.animalsReporsitory = animalsReporsitory;
            this.feeRepository = feeRepository;
        }

        public AnimalsInputModel AddFeeDropDown()
        {
            var model = new AnimalsInputModel();
            var feeCollection = this.feeRepository.AllAsNoTracking()
                .Select(x => new FeeDropDown
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            model.Fee = feeCollection;

            return model;
        }

        public async Task CreateAsync(AnimalsInputModel inputModel)
        {
            var animal = new Animal
            {
                PropertyId = inputModel.PropertyId,
                FeeId = inputModel.FeeId,
                Comment = inputModel.Comment,
                Id = inputModel.Id,
                Breed = inputModel.Breed,
            };

            await this.animalsReporsitory.AddAsync(animal);
            await this.animalsReporsitory.SaveChangesAsync();
        }
    }
}
