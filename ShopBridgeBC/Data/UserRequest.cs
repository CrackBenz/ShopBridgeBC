using System.ComponentModel.DataAnnotations;

namespace ShopBridgeBC.Data
{
    public class UserRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Please enter 8 eight digits")]
        public string Password { get; set; }
    }
}
