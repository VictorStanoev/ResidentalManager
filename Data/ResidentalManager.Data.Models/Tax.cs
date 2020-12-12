namespace ResidentalManager.Data.Models
{

    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class Tax : BaseDeletableModel<int>
    {
        public virtual Month Month { get; set; }

        public int Year { get; set; }

        public int RealEstateId { get; set; }

        public virtual RealEstate RealEstate { get; set; }

        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }

        public decimal? PropertyTax { get; set; }

        public decimal? ResidentsTax { get; set; }

        public decimal? PetTax { get; set; }

        public decimal? Total { get; set; }

        public bool IsPaid { get; set; }
    }
}
