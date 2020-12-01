namespace ResidentalManager.Web.ViewModels.Expences
{
    using ResidentalManager.Data.Models.Enum;

    public class AllExpencesViewModel
    {
        public int Id { get; set; }

        public Month Month { get; set; }

        public int Year { get; set; }

        public ExpenceType ExpenceType { get; set; }

        public decimal Amount { get; set; }

        public int RealEstateId { get; set; }

        public string Description { get; set; }
    }
}
