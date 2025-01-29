namespace CheckoutCart.Models
{
    public class Product
    {
        public required string SKU { get; set; }
        public int UnitPrice { get; set; }
        public PricingRule SpecialPrice { get; set; }

    }
}
