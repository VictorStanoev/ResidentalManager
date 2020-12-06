namespace ResidentalManager.Web.ViewComponents.YearFinanacial
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;

    [ViewComponent(Name = "YearFinancial")]
    public class YearFinancialViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Tax> taxRepository;
        private readonly IDeletableEntityRepository<RealEstateExpence> expenceRepository;

        public YearFinancialViewComponent(
            IDeletableEntityRepository<Tax> taxRepository,
            IDeletableEntityRepository<RealEstateExpence> expenceRepository)
        {
            this.taxRepository = taxRepository;
            this.expenceRepository = expenceRepository;
        }

        public IViewComponentResult Invoke(int realEstateId)
        {
            var totalYearTaxes = this.taxRepository
                .All()
                .Where(x => x.Year == DateTime.UtcNow.Year)
                .Where(x => x.RealEstateId == realEstateId)
                .Where(x => x.IsPaid == true)
                .Select(x => x.Total).Sum().Value;

            var totalYearExpenses = this.expenceRepository
                .All()
                .Where(x => x.Year == DateTime.UtcNow.Year)
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => x.Amount)
                .Sum();

            var model = new YearFinancialViewModel()
            {
                IncomeTaxes = totalYearTaxes.ToString(),
                Expences = totalYearExpenses.ToString(),
                Total = (totalYearTaxes - totalYearExpenses).ToString(),
            };
            return this.View(model);
        }
    }
}
