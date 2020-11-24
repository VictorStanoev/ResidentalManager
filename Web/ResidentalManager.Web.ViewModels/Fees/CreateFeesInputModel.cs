namespace ResidentalManager.Web.ViewModels.Fees
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CreateFeesInputModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)]

        public string Name { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Please add valid decimal number for price.")]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public int RealEstateId { get; set; }
    }
}
