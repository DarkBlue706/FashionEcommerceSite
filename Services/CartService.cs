using System.Text.Json;
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
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.Session == null)
            {
                throw new InvalidOperationException("Session is not available.");
            }

            var cartJson = httpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cartJson))
            {
                var cart = new Cart();
                await SaveCartAsync(cart);
                return cart;
            }

            return JsonSerializer.Deserialize<Cart>(cartJson) ?? new Cart();
        }

        public async Task SaveCartAsync(Cart cart)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.Session == null)
            {
                throw new InvalidOperationException("Session is not available.");
            }

            var cartJson = JsonSerializer.Serialize(cart);
            httpContext.Session.SetString("Cart", cartJson);

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
                if (quantity <= 0)
                {
                    cart.Items.Remove(existingItem);
                }
                else
                {
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
