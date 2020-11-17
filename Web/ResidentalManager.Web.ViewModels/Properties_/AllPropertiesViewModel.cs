namespace ResidentalManager.Web.ViewModels.Properties_
{
    using ResidentalManager.Data.Models.Enum;
    using ResidentalManager.Web.ViewModels.Fees;
    using System.Collections.Generic;

    public class AllPropertiesViewModel
    {
        public int Id { get; set; }

        public int RealEstateId { get; set; }

        public int Number { get; set; }

        public int Floor { get; set; }

        public int Size { get; set; }

        public decimal PercentageCommonParts { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public virtual PropertyOwnership PropertyOwnership { get; set; }

        public int FeeId { get; set; }

        public int? CompanyId { get; set; }

        public ICollection<FeeDropDown> Fee { get; set; }
    }
}
