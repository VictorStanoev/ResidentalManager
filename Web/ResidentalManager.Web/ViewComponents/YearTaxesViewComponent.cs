namespace ResidentalManager.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Data;
    using ResidentalManager.Data.Models;

    [ViewComponent(Name = "YearTaxes")]
    public class YearTaxesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext dbContext;

        public YearTaxesViewComponent(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IViewComponentResult Invoke(int realEstateId)
        {
            var taxes = this.dbContext.Taxes
                .Where(x => x.Year == DateTime.UtcNow.Year)
                .Where(x => x.RealEstateId == realEstateId)
                .Where(x => x.Property.PropertyType == Data.Models.Enum.PropertyType.Apartment)
                .Where(x => x.IsPaid == true)
                .Select(x => new TaxModel
                {
                    PropertyId = x.PropertyId,
                    PropertyNumber = x.Property.Number,
                    Month = x.Month,
                    Total = x.Total.ToString(),
                })
                .ToList();

            var taxesDic = new Dictionary<int, List<TaxModel>>();

            foreach (var item in taxes)
            {
                var id = item.PropertyId;

                if (!taxesDic.ContainsKey(item.PropertyId))
                {
                    taxesDic[id] = new List<TaxModel>();
                    taxesDic[id].Add(item);
                    continue;
                }

                taxesDic[id].Add(item);
            }

            var taxList = new List<YearTaxesViewModel>();

            foreach (var tax in taxesDic.Values)
            {
                var yearTax = new YearTaxesViewModel();
                yearTax.ApartmenNumber = tax
                    .Select(x => x.PropertyNumber).First();
                foreach (var item in tax)
                {
                    if (item.Month == Data.Models.Enum.Month.January)
                    {
                        yearTax.Jan = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.February)
                    {
                        yearTax.Feb = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.March)
                    {
                        yearTax.March = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.April)
                    {
                        yearTax.April = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.May)
                    {
                        yearTax.May = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.June)
                    {
                        yearTax.June = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.July)
                    {
                        yearTax.July = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.August)
                    {
                        yearTax.August = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.September)
                    {
                        yearTax.Sept = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.October)
                    {
                        yearTax.Oct = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.November)
                    {
                        yearTax.Nov = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.December)
                    {
                        yearTax.Dec = item.Total;
                        continue;
                    }
                }

                taxList.Add(yearTax);
            }

            return this.View(taxList);
        }
    }
}
