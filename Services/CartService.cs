using System.Text.Json;
using System.Threading.Tasks;  
using ShirtCompany.Models;

namespace ShirtCompany.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Cart> GetCartAsync()
        {
            // Retrieve the serialized cart from the session asynchronously (though GetString itself is synchronous)
            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");

            // If the cart is null, create a new one
            if (string.IsNullOrEmpty(cartJson))
            {
                var cart = new Cart();
                await SaveCartAsync(cart);
                return cart;
            }

            // Deserialize the JSON back into a Cart object
            return JsonSerializer.Deserialize<Cart>(cartJson);
        }

        public async Task SaveCartAsync(Cart cart)
        {
            // Serialize the cart object to JSON
            var cartJson = JsonSerializer.Serialize(cart);

            // Save the serialized cart JSON to the session asynchronously (though SetString is synchronous)
            _httpContextAccessor.HttpContext.Session.SetString("Cart", cartJson);

            // Using Task.CompletedTask to align with async nature
            await Task.CompletedTask;
        }
        public async Task AddToCartAsync(CartItem item)
        {
            var cart = await GetCartAsync();
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem == null)
            {
                Console.WriteLine($"Adding new item: {item.ProductId} with quantity {item.Quantity}");
                cart.Items.Add(item); 
            }
            else
            {
                existingItem.Quantity += item.Quantity;
                Console.WriteLine($"Incrementing quantity for item {existingItem.ProductId}. New quantity: {existingItem.Quantity}");
            }

            await SaveCartAsync(cart);
        }
        public async Task UpdateCartItemAsync(int productId, int quantity)
        {
            var cart = await GetCartAsync();
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                // If quantity is 0 or less, remove the item from the cart
                if (quantity <= 0)
                {
                    cart.Items.Remove(existingItem);
                }
                else
                {
                    // Update the quantity of the existing item
                    existingItem.Quantity = quantity;
                }

                await SaveCartAsync(cart);
            }
        }   

        public async Task RemoveFromCartAsync(int productId)
        {
            var cart = await GetCartAsync();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                await SaveCartAsync(cart);
            }
        }

        public async Task ClearCartAsync()
        {
            await SaveCartAsync(new Cart());
        }
        
    }
}
