
using System.ComponentModel.DataAnnotations;

namespace LearnScapeAPI.DTO
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        
        public List<BasketItemDTO> Items { get; set; }
    }
}
