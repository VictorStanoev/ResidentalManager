namespace ResidentalManager.Data.Models
{
    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class Company : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string VatNumber { get; set; }

        public virtual ResidentType ResidentType { get; set; }

        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }
    }
}
