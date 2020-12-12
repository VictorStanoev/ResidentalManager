namespace ResidentalManager.Web.ViewModels.Taxes
{
    public class TaxReceiptViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public string Email { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Ip { get; set; }
    }
}
