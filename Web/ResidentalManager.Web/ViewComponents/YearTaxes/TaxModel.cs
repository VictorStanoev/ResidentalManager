namespace ResidentalManager.Web.ViewComponents
{
    using ResidentalManager.Data.Models.Enum;

    public class TaxModel
    {
        public int PropertyId { get; set; }

        public int PropertyNumber { get; set; }

        public Month Month { get; set; }

        public string Total { get; set; }
    }
}
