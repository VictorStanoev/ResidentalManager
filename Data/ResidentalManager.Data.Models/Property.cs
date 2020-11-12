namespace ResidentalManager.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class Property : BaseDeletableModel<int>
    {
        public int RealEstateId { get; set; }

        public RealEstate RealEstate { get; set; }

        public int Number { get; set; }

        public int Floor { get; set; }

        public int Size { get; set; }

        public decimal PercentageCommonParts { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public virtual PropertyOwnership PropertyOwnership { get; set; }

        [ForeignKey(nameof(Fee))]
        public int FeeId { get; set; }

        public virtual Fee PropertyFee { get; set; }

        [ForeignKey(nameof(Company))]
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Resident> Residents { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
