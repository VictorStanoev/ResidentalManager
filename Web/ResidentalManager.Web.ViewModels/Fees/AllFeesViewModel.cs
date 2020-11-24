namespace ResidentalManager.Web.ViewModels.Fees
{
    public class AllFeesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int RealEstateId { get; set; }
    }
}
