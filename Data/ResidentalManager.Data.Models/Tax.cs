namespace ResidentalManager.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class Tax : BaseDeletableModel<int>
    {
        public Month Month { get; set; }

        public int Year { get; set; }

        public int RealEstateId { get; set; }

        public RealEstate RealEstate { get; set; }

        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public decimal PropertyTax { get; set; }

        public decimal ResidentsTax { get; set; }

        public decimal AnimalTax { get; set; }

        public bool IsPaid { get; set; }
    }
}
