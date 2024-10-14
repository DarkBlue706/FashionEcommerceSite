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
            // Fetch product details from the database asynchronously using ProductID
            var product = await _productContext.Product.FindAsync(productId);  // Async call to the database

            if (product == null)
            {
                // Handle the case where the product was not found
                return NotFound();
            }

            // Create a cart item from the fetched product
            var cartItem = new CartItem
            {
                ProductId = product.ProductID,  // Ensure property names match your Product class
                Name = product.Name,
                Quantity = quantity,
                Price = (decimal)product.Price
            };

            await _cartService.AddToCartAsync(cartItem);  // Ensure AddToCartAsync is awaited
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
