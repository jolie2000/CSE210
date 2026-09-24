using System.Collections.Generic;
using System.Text;

// Combines a customer and their products, then calculates costs and creates labels.
public class Order
{
    // One shipping charge is applied to the order, based on the customer's country.
    private const decimal _domesticShippingCost = 5.00m;
    private const decimal _internationalShippingCost = 35.00m;

    // The order owns a list of products and the customer who receives them.
    private List<Product> _products;
    private Customer _customer;

    // Start an order for one customer with no products yet.
    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    // Add a product, including its quantity, to this order.
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    // Sum product costs and add exactly one domestic or international shipping fee.
    public decimal GetTotalCost()
    {
        decimal productTotal = 0m;

        foreach (Product product in _products)
        {
            productTotal += product.GetTotalCost();
        }

        decimal shippingCost = _customer.IsInUSA()
            ? _domesticShippingCost
            : _internationalShippingCost;

        return productTotal + shippingCost;
    }

    // List each ordered product by name and product ID.
    public string GetPackingLabel()
    {
        StringBuilder label = new StringBuilder();
        label.AppendLine("Packing Label:");

        foreach (Product product in _products)
        {
            label.AppendLine($"{product.GetName()} (Product ID: {product.GetProductId()})");
        }

        return label.ToString().TrimEnd();
    }

    // Show the customer's name and formatted address as the destination label.
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddress().GetAddressString()}";
    }
}
