using CheckoutCart.Models;
using CheckoutCart.Utils;

namespace CheckoutCart.Services
{
    public class Checkout : ICheckout
    {
        private readonly List<string> _scannedItems;

        public Checkout()
        {
            _scannedItems = new List<string>();
        }
        /// <summary>
        /// This Method is to calculate total based on all conditions
        /// </summary>
        /// <returns></returns>
        public int GetTotalPrice()
        {
            try
            {
                int totalPrice = 0;
                var _products = AppUtils.GetProducts();

                foreach (var product in _products)
                {
                    var itemCount = _scannedItems.Count(i => i == product.SKU);
                    totalPrice += CalculatePrice(product, itemCount);
                }

                return totalPrice;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int CalculatePrice(Product product, int itemCount)
        {
            if (product.SpecialPrice != null && itemCount >= product.SpecialPrice.Quantity)
            {
                int specialPriceGroups = itemCount / product.SpecialPrice.Quantity;
                int remainder = itemCount % product.SpecialPrice.Quantity;
                return specialPriceGroups * product.SpecialPrice.Price + remainder * product.UnitPrice;
            }

            return itemCount * product.UnitPrice;
        }
        /// <summary>
        /// to get item to calculate total
        /// </summary>
        /// <param name="item"></param>
        public void Scan(string item)
        {
            try
            {
                _scannedItems.Add(item);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
