using CheckoutCart.Models;

namespace CheckoutCart.Utils
{
    public static class AppUtils
    {
        public static List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product { SKU = "A", UnitPrice = 50, SpecialPrice = new PricingRule { Quantity = 3, Price = 130 } },
                new Product { SKU = "B", UnitPrice = 30, SpecialPrice = new PricingRule { Quantity = 2, Price = 45 } },
                new Product { SKU = "C", UnitPrice = 20 },
                new Product { SKU = "D", UnitPrice = 15 }
            };
            return products;
        }
    }
}
