namespace ResidentalManager.Data.Models
{
    using System.Collections.Generic;

    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class RealEstateExpence : BaseDeletableModel<int>
    {
        public virtual Month Month { get; set; }

        public int Year { get; set; }

        public virtual ExpenceType ExpenceType { get; set; }

        public decimal Amount { get; set; }

        public int RealEstateId { get; set; }

        public virtual RealEstate RealEstate { get; set; }

        public string Description { get; set; }
    }
}
