public class Address
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    public string Street => _street;
    public string City => _city;
    public string StateOrProvince => _stateOrProvince;
    public string Country => _country;

    // Indicates if the address is in the US (case-insensitive)
    public bool IsUSLocation()
    {
        if (string.IsNullOrWhiteSpace(_country)) return false;
        var c = _country.Trim().ToLowerInvariant();
        return c == "usa" || c == "us" || c == "united states" || c == "united states of america";
    }

    // Returns the formatted address string
    public string ToPrintableString()
    {
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}