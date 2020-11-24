namespace ResidentalManager.Data.Models
{
    using ResidentalManager.Data.Common.Models;

    public class RealEstateExpenceOther : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Decription { get; set; }

        public int RealEstateExpenceId { get; set; }

        public RealEstateExpence RealEstateExpence { get; set; }
    }
}
