namespace ResidentalManager.Web.ViewModels.Administration.Dashboard
{
    public class UsersViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int? PropertyId { get; set; }

        public int? RealEstateId { get; set; }
    }
}
