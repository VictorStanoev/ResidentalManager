namespace ResidentalManager.Web.ViewModels.Properties_
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ResidentalManager.Data.Models.Enum;
    using ResidentalManager.Web.ViewModels.Fees;

    public class CreatePropertiesInputModel
    {
        public int Id { get; set; }

        public int RealEstateId { get; set; }

        [Range(0, 9_999_999)]
        public int Number { get; set; }

        [Range(0, 40)]
        public int Floor { get; set; }

        [Range(5, 1500)]
        public int Size { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Please add valid decimal number for common parts.")]
        [Range(0, 9999999999999999.99)]
        public decimal PercentageCommonParts { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public virtual PropertyOwnership PropertyOwnership { get; set; }

        public int FeeId { get; set; }

        public ICollection<FeeDropDown> Fee { get; set; }

        public int? CompanyId { get; set; }
    }
}
