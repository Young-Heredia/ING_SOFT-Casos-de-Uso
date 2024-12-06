using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GESTIONAR_PROMOCIONES.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 30 characters.")]
        [Display(Name = "NAME")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Only letters, including accented characters and single spaces between words are allowed.")]
        public string? Name { get; set; }



        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "The Description must be between 4 and 100 characters.")]
        [Display(Name = "DESCRIPTION")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Only letters, including accented characters and single spaces between words are allowed.")]
        public string? Description { get; set; }


        [Display(Name = "PRICE")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "The price must be a valid number (no letters or special characters).")]
        [Range(0, double.MaxValue, ErrorMessage = "The note must be a positive number.")]
        public decimal Price { get; set; }

        [NotNull]
        public byte Status { get; set; } = 1;

        [Display(Name = "CATEGORY")]
        public int CategoryID { get; set; }

        public Category? Category { get; set; }

        public ICollection<Promotion>? Promotions { get; set; }
    }
}
