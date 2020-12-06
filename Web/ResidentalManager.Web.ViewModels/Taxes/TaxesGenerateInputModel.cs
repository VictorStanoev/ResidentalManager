namespace ResidentalManager.Web.ViewModels.Taxes
{
    using ResidentalManager.Data.Models.Enum;

    public class TaxesGenerateInputModel
    {
        public int Year { get; set; }

        public Month Month { get; set; }
    }
}
