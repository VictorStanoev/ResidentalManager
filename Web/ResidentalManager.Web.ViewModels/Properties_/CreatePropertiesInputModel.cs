namespace ResidentalManager.Web.ViewModels.Properties_
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ResidentalManager.Data.Models.Enum;
    using ResidentalManager.Web.ViewModels.Fees;

    public class CreatePropertiesInputModel
    {
        public int RealEstateId { get; set; }

        [Range(0, 40)]
        public int Floor { get; set; }

        public int RealEstateFloors { get; set; }

        [Range(0, 999_999)]
        public int Number { get; set; }

        [Range(5, 1500)]
        public int Size { get; set; }

        [Display(Name = "% common parts")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Please add valid decimal number for common parts.")]
        [Range(0, 9999999999999999.99)]
        public decimal PercentageCommonParts { get; set; }

        [Display(Name = "Choose property Type")]
        public virtual PropertyType PropertyType { get; set; }

        [Display(Name = "Choose property Ownership")]
        public virtual PropertyOwnership PropertyOwnership { get; set; }

        public int FeeId { get; set; }

        [Display(Name = "Choose property Fee")]
        public ICollection<FeeDropDown> Fee { get; set; }
    }
}
