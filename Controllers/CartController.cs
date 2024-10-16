using Microsoft.AspNetCore.Mvc;
using ShirtCompany.Services;
using ShirtCompany.Models;
using ShirtCompany.Filters.ActionFilters;
using System.Threading.Tasks;

namespace ShirtCompany.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ProductDBContext _productContext;

        public CartController(CartService cartService, ProductDBContext productContext)
        {
            _cartService = cartService;
            _productContext = productContext;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await _cartService.GetCartAsync();  // Ensure the GetCartAsync is awaited
            return View(cart);
        }

        [ServiceFilter(typeof(Product_ValidateProductPriceFilter))]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _productContext.Product.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                ProductId = product.ProductID,
                Name = product.Name,
                Quantity = quantity,
                Price = (decimal)product.Price
            };

            await _cartService.AddToCartAsync(cartItem);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCartItem(int productId, int quantity)
        {
            if (quantity < 1)
            {
                // Optionally handle bad input and return a response or log it
                return BadRequest("Quantity must be at least 1.");
            }

            await _cartService.UpdateCartItemAsync(productId, quantity);
            return RedirectToAction("Index");
        }      

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _cartService.RemoveFromCartAsync(productId);  // Ensure RemoveFromCartAsync is awaited
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync();  // Ensure ClearCartAsync is awaited
            return RedirectToAction("Index");
        }
    }
}
