namespace ResidentalManager.Data.Models
{
    using System.Collections.Generic;

    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class RealEstateExpence : BaseDeletableModel<int>
    {
        public Month Month { get; set; }

        public int Year { get; set; }

        public decimal TotalExpences { get; set; }

        public int RealEstateId { get; set; }

        public RealEstate RealEstate { get; set; }

        public string Description { get; set; }
    }
}
