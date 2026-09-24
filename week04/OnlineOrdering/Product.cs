// Stores one product and the quantity included in an order.
public class Product
{
    // Product details are private so callers use the public methods below.
    private string _name;
    private string _productId;
    private decimal _pricePerUnit;
    private int _quantity;

    // Set product details and quantity when creating the product object.
    public Product(string name, string productId, decimal pricePerUnit, int quantity)
    {
        _name = name;
        _productId = productId;
        _pricePerUnit = pricePerUnit;
        _quantity = quantity;
    }

    // Return the product name for the packing label.
    public string GetName()
    {
        return _name;
    }

    // Return the product identifier for the packing label.
    public string GetProductId()
    {
        return _productId;
    }

    // Calculate this product's extended price using unit price times quantity.
    public decimal GetTotalCost()
    {
        return _pricePerUnit * _quantity;
    }
}
