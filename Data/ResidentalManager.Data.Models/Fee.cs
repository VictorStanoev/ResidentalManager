﻿namespace ResidentalManager.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ResidentalManager.Data.Common.Models;

    public class Fee : BaseDeletableModel<int>
    {
        public Fee()
        {
            this.PropertyFees = new HashSet<Property>();
            this.ResidentFees = new HashSet<Resident>();
            this.PetFees = new HashSet<Pet>();
        }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int RealEstateId { get; set; }

        public virtual RealEstate RealEstate { get; set; }

        public virtual ICollection<Property> PropertyFees { get; set; }

        public virtual ICollection<Resident> ResidentFees { get; set; }

        public virtual ICollection<Pet> PetFees { get; set; }
    }
}
