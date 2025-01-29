﻿namespace CheckoutCart.Services
{
    public interface ICheckout
    {
        void Scan(string item); 
        int GetTotalPrice();
    }
}
