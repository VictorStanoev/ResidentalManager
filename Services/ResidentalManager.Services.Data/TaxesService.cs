namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Web.ViewModels.Taxes;

    public class TaxesService : ITaxesService
    {
        private readonly IDeletableEntityRepository<Tax> taxRepository;
        private readonly IDeletableEntityRepository<Property> propertyRepository;
        private readonly IRepository<RealEstate> realEstateReposiotory;

        public TaxesService(
            IDeletableEntityRepository<Tax> taxRepository,
            IDeletableEntityRepository<Property> propertyRepository,
            IRepository<RealEstate> realEstateReposiotory)
        {
            this.taxRepository = taxRepository;
            this.propertyRepository = propertyRepository;
            this.realEstateReposiotory = realEstateReposiotory;
        }

        public async Task GenerateTaxes(int realEstateId, TaxesGenerateInputModel inputModel)
        {
            var properties = this.propertyRepository
                .All()
                .Where(x => x.RealEstateId == realEstateId)
                .ToList();

            var taxes = new List<Tax>();
            foreach (var prop in properties)
            {
                var tax = new Tax
                {
                    Year = inputModel.Year,
                    Month = inputModel.Month,
                    RealEstateId = realEstateId,
                    PropertyId = prop.Id,
                    PropertyTax = prop.PropertyFee.Price,
                    ResidentsTax = prop.Residents.Sum(x => x.ResidentFee.Price),
                    AnimalTax = prop.Animals.Sum(x => x.AnimalFee.Price),
                    Total = prop.PropertyFee.Price + prop.Residents.Sum(x => x.ResidentFee.Price),
                };

                await this.taxRepository.AddAsync(tax);
            }

            await this.taxRepository.SaveChangesAsync();
        }

        public TaxesListViewModel GetAllEstateTaxes(int realEstateId, int pageNum)
        {
            var properties = this.realEstateReposiotory
                .All().Where(x => x.Id == realEstateId)
                .Select(x => x.Properties.Count()).FirstOrDefault();

            var taxes = this.taxRepository
               .AllAsNoTracking()
               .Where(x => x.RealEstateId == realEstateId)
               .OrderByDescending(x => x.Year)
               .ThenByDescending(x => x.Month)
               .Skip((pageNum - 1) * properties).Take(properties)
               .Select(x => new TaxViewModel()
               {
                   Id = x.Id,
                   PropertyTax = x.PropertyTax,
                   ResidentsTax = x.ResidentsTax,
                   AnimalTax = x.AnimalTax,
                   Total = x.Total.ToString(),
                   IsPaid = x.IsPaid,
                   Month = x.Month,
                   Year = x.Year,
                   PropertyNumber = x.Property.Number,
               })
               .OrderByDescending(x => x.PropertyNumber)
               .AsEnumerable();

            var model = new TaxesListViewModel
            {
                PageNumber = pageNum,
                ItemsPerPage = properties,
                TaxesCount = this.GetCount(),
                Taxes = taxes,
            };

            return model;
        }

        public int GetCount()
        {
            return this.taxRepository.All().Count();
        }

        public async Task Pay(int id)
        {
            var tax = this.taxRepository.All()
                .Where(x => x.Id == id).FirstOrDefault();

            if (tax != null)
            {
                tax.IsPaid = true;
                this.taxRepository.Update(tax);
                await this.taxRepository.SaveChangesAsync();
            }
        }

        public async Task ReversePayment(int id)
        {
            var tax = this.taxRepository.All()
                .Where(x => x.Id == id).FirstOrDefault();

            if (tax != null)
            {
                tax.IsPaid = false;
                this.taxRepository.Update(tax);
                await this.taxRepository.SaveChangesAsync();
            }
        }

        public async Task UpdateTax(int id)
        {
            var tax = this.taxRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var property = this.propertyRepository.All().Where(x => x.Id == tax.PropertyId).FirstOrDefault();

            tax.PropertyTax = property.PropertyFee.Price;
            tax.ResidentsTax = property.Residents.Sum(x => x.ResidentFee.Price);
            tax.AnimalTax = property.Animals.Sum(x => x.AnimalFee.Price);
            tax.Total = property.PropertyFee.Price + property.Residents.Sum(x => x.ResidentFee.Price);
            tax.IsPaid = false;

            this.taxRepository.Update(tax);
            await this.taxRepository.SaveChangesAsync();
        }
    }
}
