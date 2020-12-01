namespace ResidentalManager.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ResidentalManager.Data.Common.Models;
    using ResidentalManager.Data.Models.Enum;

    public class Resident : BaseDeletableModel<string>
    {
        public Resident()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public Gender? Gender { get; set; }

        [Required]
        public ResidentType ResidentType { get; set; }

        [ForeignKey(nameof(Property))]
        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [ForeignKey(nameof(Fee))]

        public int FeeId { get; set; }

        public virtual Fee ResidentFee { get; set; }
    }
}
