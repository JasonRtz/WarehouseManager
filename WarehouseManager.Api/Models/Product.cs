using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.Api.Models
{
    public class Product
    {

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(30)]
        public string SKU { get; set; } = string.Empty;
        [StringLength(500)]
        public string? Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(typeof(decimal), "0.01", "999999999.99")]
        public decimal Price { get; set; }
    }
}
