namespace ResidentalManager.Data.Models
{
    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class Company : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string VatNumber { get; set; }

        public ResidentType ResidentType { get; set; }

        public int PropertyId { get; set; }

        public Property Property { get; set; }
    }
}
