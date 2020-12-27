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
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "d")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(12, ErrorMessage = "{0} length must be between {2} and {1} digits.", MinimumLength = 9)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please, enter only digits!")]
        public string Telephone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Choose gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Choose resident type")]
        public ResidentType ResidentType { get; set; }

        [Range(0, 900000)]
        public int PropertyId { get; set; }

        [Range(0, 900000)]
        public int FeeId { get; set; }

        [Display(Name = "Choose Fee")]
        public ICollection<FeeDropDown> Fee { get; set; }
    }
}
