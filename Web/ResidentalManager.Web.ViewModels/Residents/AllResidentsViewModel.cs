namespace ResidentalManager.Web.ViewModels.Residents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ResidentalManager.Data.Models.Enum;
    using ResidentalManager.Web.ViewModels.Fees;

    public class AllResidentsViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "d")]
        public DateTime? DateOfBirth { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public Gender? Gender { get; set; }

        public ResidentType ResidentType { get; set; }

        public int PropertyId { get; set; }

        public int FeeId { get; set; }

        public ICollection<FeeDropDown> Fee { get; set; }
    }
}
