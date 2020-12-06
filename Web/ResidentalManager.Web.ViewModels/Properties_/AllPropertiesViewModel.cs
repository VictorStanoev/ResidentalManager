namespace ResidentalManager.Web.ViewModels.Properties_
{
    using System.Collections.Generic;

    using ResidentalManager.Data.Models.Enum;
    using ResidentalManager.Web.ViewModels.Fees;

    public class AllPropertiesViewModel
    {
        public int Id { get; set; }

        public int RealEstateId { get; set; }

        public int Number { get; set; }

        public int Floor { get; set; }

        public int Size { get; set; }

        public decimal PercentageCommonParts { get; set; }

        public PropertyType PropertyType { get; set; }

        public PropertyOwnership PropertyOwnership { get; set; }

        public int FeeId { get; set; }

        public int? CompanyId { get; set; }

        public ICollection<FeeDropDown> Fee { get; set; }

        public int Residents { get; set; }

        public int Animals { get; set; }
    }
}
