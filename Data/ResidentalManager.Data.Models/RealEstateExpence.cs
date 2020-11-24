namespace ResidentalManager.Data.Models
{
    using System.Collections.Generic;

    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class RealEstateExpence : BaseDeletableModel<int>
    {
        public RealEstateExpence()
        {
            this.ExpencesOther = new HashSet<RealEstateExpenceOther>();

            this.ExpencesRegular = new HashSet<RealEstateExpenceRegular>();
        }

        public Month Month { get; set; }

        public int Year { get; set; }

        public decimal TotalExpences { get; set; }

        public int RealEstateId { get; set; }

        public virtual RealEstate RealEstate { get; set; }

        public virtual ICollection<RealEstateExpenceOther> ExpencesOther { get; set; }

        public virtual ICollection<RealEstateExpenceRegular> ExpencesRegular { get; set; }
    }
}
