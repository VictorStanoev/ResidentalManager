namespace ResidentalManager.Web.ViewModels.Estates
{
   using System.ComponentModel.DataAnnotations;

   public class CreateEstatesInputModel
    {
        [Required]
        [Display(Name = "Real Estate Name")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 3)]
        public string Region { get; set; }

        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 3)]
        public string Municipality { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 3)]
        public string Town { get; set; }

        [Display(Name = "Post Code")]
        [Range(0, 99999)]
        public int PostCode { get; set; }

        [Display(Name = "Residental Area")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 3)]
        public string ResidentalArea { get; set; }

        [Display(Name = "Street Name")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 3)]
        public string StreetName { get; set; }

        [Display(Name = "Street Number")]
        [StringLength(6, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 1)]
        public string StreetNumber { get; set; }

        [Display(Name = "Building Number")]
        [StringLength(6, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 1)]
        public string BuildingNumber { get; set; }

        [Display(Name = "Entrance Number")]
        [StringLength(6, ErrorMessage = "{0} length must be between {2} and {1} simvols.", MinimumLength = 1)]
        public string EntranceNumber { get; set; }

        [Range(0, 30)]
        public int Floors { get; set; }

        public bool Attics { get; set; }

        public bool Basements { get; set; }

        public bool Elevator { get; set; }

        public bool Garages { get; set; }
    }
}
