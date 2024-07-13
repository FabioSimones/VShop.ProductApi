using VShop.CartApi.Models;

namespace VShop.CartApi.DTOs
{
    public class CartDTO
    {
        public CartHeaderDTO CartHeaderDTO { get; set; } = new CartHeaderDTO();
        public IEnumerable<CartItemDTO> CartItemsDTO { get; set; } = Enumerable.Empty<CartItemDTO>();

    }
}
