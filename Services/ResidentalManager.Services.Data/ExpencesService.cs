﻿namespace ResidentalManager.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Common;
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

        public ExpencesListViewModel GetAll(int realEstateId, int pageNum)
        {
            var expences = this.expenceRepository.AllAsNoTracking()
                .Where(x => x.RealEstateId == realEstateId)
                 .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .Skip((pageNum - 1) * GlobalConstants.ExpencesOnOnePage).Take(GlobalConstants.ExpencesOnOnePage)
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

            var model = new ExpencesListViewModel
            {
                PageNumber = pageNum,
                ItemsPerPage = GlobalConstants.ExpencesOnOnePage,
                ExpencesCount = this.GetCountRealEstateExpences(realEstateId),
                Expences = expences,
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var expence = this.expenceRepository.All().Where(x => x.Id == id).FirstOrDefault();

            this.expenceRepository.Delete(expence);
            await this.expenceRepository.SaveChangesAsync();
        }

        private int GetCountRealEstateExpences(int realEstateId)
            => this.expenceRepository.All().Where(x => x.RealEstateId == realEstateId).Count();
    }
}
