namespace ResidentalManager.Web.ViewModels.Taxes
{
    using ResidentalManager.Data.Models.Enum;

    public class GenerateTaxesInputModel
    {
        public int Year { get; set; }

        public Month Month { get; set; }
    }
}
