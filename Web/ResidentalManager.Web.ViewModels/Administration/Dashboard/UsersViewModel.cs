namespace ResidentalManager.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    public class UsersViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string RealEstateName { get; set; }

        public string PropertyNumber { get; set; }

        public int? PropertyId { get; set; }

        public int? RealEstateId { get; set; }

        public ICollection<RealEstateDropDown> RealEstate { get; set; }

        public ICollection<PropertyDropDown> Property { get; set; }
    }
}
