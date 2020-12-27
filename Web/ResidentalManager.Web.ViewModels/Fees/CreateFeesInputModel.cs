namespace ResidentalManager.Web.ViewModels.Fees
{
    using System.ComponentModel.DataAnnotations;

    public class CreateFeesInputModel
    {
        [Required]
        [Display(Name = "Fee Name")]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 3)]
        public string Name { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Please add valid decimal number in format (0.00) for price.")]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }

        [MaxLength(60)]
        public string Description { get; set; }

        [Required]
        public int RealEstateId { get; set; }
    }
}
