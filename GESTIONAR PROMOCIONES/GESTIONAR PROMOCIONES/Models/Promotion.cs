using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GESTIONAR_PROMOCIONES.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required, NotNull]
        public string? Name { get; set; }

        [Required, NotNull]
        public double Price { get; set; }

        [Required, NotNull]
        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        [NotNull]
        public byte Status { get; set; }

        public int ProductID { get; set; }

        public Product? Product { get; set; }
    }
}
