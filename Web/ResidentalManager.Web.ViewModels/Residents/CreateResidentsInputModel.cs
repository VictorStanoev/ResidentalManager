namespace ResidentalManager.Web.ViewModels.Residents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ResidentalManager.Data.Models.Enum;
    using ResidentalManager.Web.ViewModels.Fees;

    public class CreateResidentsInputModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Enter only nubers for telephone!")]
        public string Telephone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        public ResidentType ResidentType { get; set; }

        public int PropertyId { get; set; }

        public int FeeId { get; set; }

        public ICollection<FeeDropDown> Fee { get; set; }
    }
}
