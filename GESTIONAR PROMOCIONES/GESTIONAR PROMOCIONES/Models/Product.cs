using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GESTIONAR_PROMOCIONES.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        [NotNull]
        public byte Status { get; set; } = 1;

        public int CategoryID { get; set; }

        public Category? Category { get; set; }

        public ICollection<Promotion>? Promotions { get; set; }
    }
}
