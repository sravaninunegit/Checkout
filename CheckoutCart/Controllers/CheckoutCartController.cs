using CheckoutCart.Services;
using CheckoutCart.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutCart.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckoutCartController : ControllerBase
    {
        private ICheckout _checkOut;
        private ILogger<CheckoutCartController> _logger;


        public CheckoutCartController(ICheckout _checkOut, ILogger<CheckoutCartController> _logger)
        {
            this._logger = _logger;
            this._checkOut = _checkOut;
        }

        [HttpPost]
        public IActionResult Scan([FromBody] List<string> items)
        {

            if (items == null || items.Count == 0 || items.Any(item => string.IsNullOrWhiteSpace(item)))
            {
                return BadRequest("Item cannot be empty");
            }
            // Validate that all items exist in the allowed list
            var invalidItems = items.Where(item => !AppUtils.GetProducts().Select(p => p.SKU).ToHashSet().Contains(item)).ToList();

            if (invalidItems.Any())
            {
                return BadRequest($"Invalid items detected: {string.Join(", ", invalidItems)}");
            }
            else
            {
                foreach (var item in items)
                {
                    _checkOut.Scan(item);
                }

                return Ok(_checkOut.GetTotalPrice());
            }

        }
    }
}