using System;
public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer, List<Product> products)
    {
        _customer = customer;
        _products = products ?? new List<Product>();
    }

    public Customer Customer => _customer;
    public IReadOnlyList<Product> Products => _products.AsReadOnly();

    // Total cost: sum of product costs + shipping
    public decimal GetTotalCost()
    {
        decimal productsTotal = _products.Sum(p => p.GetTotalCost());
        decimal shipping = _customer.Address.IsUSLocation() ? 5m : 35m;
        return productsTotal + shipping;
    }

    // Packing label: name and ID of each product
    public string GetPackingLabel()
    {
        var lines = new List<string>();
        foreach (var p in _products)
        {
            lines.Add($"Product: {p.Name} (ID: {p.ProductId})");
        }
        return string.Join(System.Environment.NewLine, lines);
    }

    // Shipping label: customer name and address
    public string GetShippingLabel()
    {
        return $"Customer: {_customer.Name}\nAddress:\n{_customer.Address.ToPrintableString()}";
    }
}