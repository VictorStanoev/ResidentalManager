namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Web.ViewModels.Expences;

    public class ExpencesService : IExpencesService
    {
        private readonly IDeletableEntityRepository<RealEstateExpence> expenceRepository;

        public ExpencesService(IDeletableEntityRepository<RealEstateExpence> expenceRepository)
        {
            this.expenceRepository = expenceRepository;
        }

        public async Task CreateAsync(int realEstateId, CreateExpencesInputModel inputModel)
        {
            var fee = new RealEstateExpence()
            {
                RealEstateId = realEstateId,
                Amount = inputModel.Amount,
                Description = inputModel.Description,
                Month = inputModel.Month,
                Year = inputModel.Year,
                ExpenceType = inputModel.ExpenceType,
            };

            await this.expenceRepository.AddAsync(fee);
            await this.expenceRepository.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public AllExpencesViewModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AllExpencesViewModel> GetAll(int realEstateId)
        {
            var expences = this.expenceRepository.AllAsNoTracking()
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => new AllExpencesViewModel
                {
                   Month = x.Month,
                   Year = x.Year,
                   Description = x.Description,
                   Amount = x.Amount,
                   RealEstateId = x.RealEstateId,
                   ExpenceType = x.ExpenceType,
                   Id = x.Id,
                })
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .AsEnumerable();

            return expences;
        }

        public Task Update(int id, CreateExpencesInputModel inputModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
