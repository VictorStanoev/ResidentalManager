namespace ResidentalManager.Data.Models
{
    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class RealEstateExpenceRegular : BaseDeletableModel<int>
    {
        public RegularExpenceType MyProperty { get; set; }

        public decimal Price { get; set; }

        public string Decription { get; set; }

        public int RealEstateExpenceId { get; set; }

        public RealEstateExpence RealEstateExpence { get; set; }
    }
}
