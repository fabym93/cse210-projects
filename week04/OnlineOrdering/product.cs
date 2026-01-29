using System;

public class Product
{
    // Private fields
    private string _name;
    private string _productId;
    private decimal _price;
    private int _quantity;

    // Constructor
    public Product(string name, string productId, decimal price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    // Read-only properties (encapsulation)
    public string Name => _name;
    public string ProductId => _productId;
    public decimal Price => _price;
    public int Quantity => _quantity;

    // Method: total cost for this product
    public decimal GetTotalCost()
    {
        return _price * _quantity;
    }
}