namespace ResidentalManager.Web.ViewModels.Animals
{
    using System.Collections.Generic;

    using ResidentalManager.Web.ViewModels.Fees;

    public class AnimalsInputModel
    {
        public int Id{ get; set; }

        public string Breed { get; set; }

        public string Comment { get; set; }

        public int PropertyId { get; set; }

        public int FeeId { get; set; }

        public ICollection<FeeDropDown> Fee { get; set; }
    }
}
