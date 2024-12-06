using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GESTIONAR_PROMOCIONES.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, NotNull]
        public string Name { get; set; }

        [Required, NotNull]
        public string? Description { get; set; }

        [NotNull]
        public byte Status { get; set; } = 1;

        public ICollection<Product>? Products { get; set; }
    }
}
