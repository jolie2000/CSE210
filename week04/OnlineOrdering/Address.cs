using System;

// Stores a customer's mailing address and provides address-related behavior.
public class Address
{
    // Private fields keep address details encapsulated inside this class.
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    // Set all address details when an Address object is created.
    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    // Return true when the country identifies the United States.
    public bool IsInUSA()
    {
        return string.Equals(_country, "USA", StringComparison.OrdinalIgnoreCase)
            || string.Equals(_country, "United States", StringComparison.OrdinalIgnoreCase);
    }

    // Format the address on multiple lines for a shipping label.
    public string GetAddressString()
    {
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}
