public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string Name => _name;
    public Address Address => _address;

    // Returns customer information (name + formatted address)
    public string GetInfo()
    {
        return $"Name: {_name}\nAddress:\n{_address.ToPrintableString()}";
    }
}