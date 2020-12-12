namespace ResidentalManager.Web.ViewModels.Pets
{
    using System.Collections.Generic;

    using ResidentalManager.Web.ViewModels.Fees;

    public class PetsInputModel
    {
        public int Id { get; set; }

        public string Breed { get; set; }

        public string Comment { get; set; }

        public int PropertyId { get; set; }

        public int FeeId { get; set; }

        public ICollection<FeeDropDown> Fee { get; set; }
    }
}
