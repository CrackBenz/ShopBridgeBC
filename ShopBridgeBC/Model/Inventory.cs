using System.ComponentModel.DataAnnotations;

namespace ShopBridgeBC.Model
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required]
        public string ItemName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
