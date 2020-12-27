namespace ResidentalManager.Web.ViewModels.Properties_
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ResidentalManager.Data.Models.Enum;
    using ResidentalManager.Web.ViewModels.Fees;

    public class AllPropertiesViewModel
    {
        public int Id { get; set; }

        public int RealEstateId { get; set; }

        public int Number { get; set; }

        public int Floor { get; set; }

        public int Size { get; set; }

        [Display(Name = "% common parts")]
        [Range(0, 100.00)]
        public decimal PercentageCommonParts { get; set; }

        [Display(Name = "Choose property Type")]
        public PropertyType PropertyType { get; set; }

        [Display(Name = "Choose property Ownership")]
        public PropertyOwnership PropertyOwnership { get; set; }

        public int FeeId { get; set; }

        public ICollection<FeeDropDown> Fee { get; set; }

        public int Residents { get; set; }

        public int Pets { get; set; }
    }
}
