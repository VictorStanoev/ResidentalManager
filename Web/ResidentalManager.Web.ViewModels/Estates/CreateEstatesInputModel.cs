namespace ResidentalManager.Web.ViewModels.Estates
{
   using System.ComponentModel.DataAnnotations;

   public class CreateEstatesInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Name { get; set; }

        public string Region { get; set; }

        public string Municipality { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Town { get; set; }

        [Range(1000, 99999)]
        public int PostCode { get; set; }

        public string ResidentalArea { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string BuildingNumber { get; set; }

        public string EntranceNumber { get; set; }

        [Range(0, 50)]
        public int Floors { get; set; }

        public bool Attics { get; set; }

        public bool Basements { get; set; }

        public bool Elevator { get; set; }

        public bool Garages { get; set; }
    }
}
