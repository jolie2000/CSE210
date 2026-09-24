// Connects a customer's name with their Address object.
public class Customer
{
    // Keep customer information private and expose it through methods.
    private string _name;
    private Address _address;

    // Create a customer with a name and mailing address.
    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    // Provide the customer's name for labels.
    public string GetName()
    {
        return _name;
    }

    // Provide the customer's Address object to the order for its shipping label.
    public Address GetAddress()
    {
        return _address;
    }

    // Delegate the location check to the Address class.
    public bool IsInUSA()
    {
        return _address.IsInUSA();
    }
}
