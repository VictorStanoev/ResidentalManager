namespace ResidentalManager.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Common;
    using ResidentalManager.Data;

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
                    Total = x.Total + GlobalConstants.Currency.ToString(),
                })
                .ToList();

            var taxesDic = new Dictionary<int, List<TaxModel>>();

            foreach (var item in taxes)
            {
                var id = item.PropertyId;

                if (!taxesDic.ContainsKey(item.PropertyId))
                {
                    taxesDic[id] = new List<TaxModel>
                    {
                        item,
                    };
                    continue;
                }

                taxesDic[id].Add(item);
            }

            var taxList = new List<YearTaxesViewModel>();

            foreach (var tax in taxesDic.Values)
            {
                var yearTax = new YearTaxesViewModel
                {
                    ApartmenNumber = tax
                    .Select(x => x.PropertyNumber).First(),
                };
                foreach (var item in tax)
                {
                    if (item.Month == Data.Models.Enum.Month.January)
                    {
                        yearTax.TaxJan = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.February)
                    {
                        yearTax.TaxFeb = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.March)
                    {
                        yearTax.TaxMarch = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.April)
                    {
                        yearTax.TaxApril = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.May)
                    {
                        yearTax.TaxMay = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.June)
                    {
                        yearTax.TaxJune = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.July)
                    {
                        yearTax.TaxJuly = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.August)
                    {
                        yearTax.TaxAugust = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.September)
                    {
                        yearTax.TaxSept = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.October)
                    {
                        yearTax.TaxOct = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.November)
                    {
                        yearTax.TaxNov = item.Total;
                        continue;
                    }

                    if (item.Month == Data.Models.Enum.Month.December)
                    {
                        yearTax.TaxDec = item.Total;
                        continue;
                    }
                }

                taxList.Add(yearTax);
            }

            return this.View(taxList);
        }
    }
}
