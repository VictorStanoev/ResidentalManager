namespace ResidentalManager.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using ResidentalManager.Data.Common.Models;

    public class Animal : BaseDeletableModel<int>
    {
        public string Breed { get; set; }

        public string Comment { get; set; }

        [ForeignKey(nameof(Property))]
        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [ForeignKey(nameof(Fee))]
        public int FeeId { get; set; }

        public virtual Fee AnimalFee { get; set; }
    }
}
