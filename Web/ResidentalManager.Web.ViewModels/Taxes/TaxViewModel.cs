namespace ResidentalManager.Web.ViewModels.Taxes
{
    using ResidentalManager.Data.Models.Enum;

    public class TaxViewModel
    {
        public int Id { get; set; }

        public Month Month { get; set; }

        public int Year { get; set; }

        public int PropertyNumber { get; set; }

        public decimal? PropertyTax { get; set; }

        public decimal? ResidentsTax { get; set; }

        public decimal? AnimalTax { get; set; }

        public string Total { get; set; }

        public bool IsPaid { get; set; }
    }
}
