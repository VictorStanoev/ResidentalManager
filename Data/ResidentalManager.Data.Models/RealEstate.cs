namespace ResidentalManager.Data.Models
{
    using System.Collections.Generic;

    using ResidentalManager.Data.Common.Models;

    public class RealEstate : BaseModel<int>
    {
        public RealEstate()
        {
            this.Properties = new HashSet<Property>();

            this.Fees = new HashSet<Fee>();

            this.Expences = new HashSet<RealEstateExpence>();

            this.Taxes = new HashSet<Tax>();
        }

        public string Name { get; set; }

        public string Region { get; set; }

        public string Municipality { get; set; }

        public string Town { get; set; }

        public int PostCode { get; set; }

        public string ResidentalArea { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string BuildingNumber { get; set; }

        public string EntranceNumber { get; set; }

        public int Floors { get; set; }

        public bool Attics { get; set; }

        public bool Basements { get; set; }

        public bool Elevator { get; set; }

        public bool Garages { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public virtual ICollection<Fee> Fees { get; set; }

        public virtual ICollection<RealEstateExpence> Expences { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
