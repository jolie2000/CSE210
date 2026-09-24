using System;
using System.Collections.Generic;

// Creates example orders and displays their labels and totals.
public class Program
{
    // This assignment needs no prompts; all sample input data is created here.
    public static void Main(string[] args)
    {
        // First order: a customer in the USA pays the domestic shipping rate.
        Address address1 = new Address("123 Maple Street", "Provo", "Utah", "USA");
        Customer customer1 = new Customer("Alex Morgan", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Insulated Water Bottle", "WB-204", 18.50m, 2));
        order1.AddProduct(new Product("Canvas Tote Bag", "TB-118", 12.00m, 1));

        // Second order: a customer in Canada pays the international shipping rate.
        Address address2 = new Address("45 Cedar Avenue", "Toronto", "Ontario", "Canada");
        Customer customer2 = new Customer("Jamie Chen", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Desk Lamp", "DL-305", 32.00m, 1));
        order2.AddProduct(new Product("Notebook Set", "NS-410", 9.75m, 3));
        order2.AddProduct(new Product("Pen Holder", "PH-022", 7.25m, 2));

        // Store both orders together so the same display logic can handle each one.
        List<Order> orders = new List<Order> { order1, order2 };

        // Display both labels and the final total for every order.
        for (int index = 0; index < orders.Count; index++)
        {
            Order order = orders[index];
            Console.WriteLine($"Order {index + 1}");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine();
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total price: ${order.GetTotalCost():F2}");
            Console.WriteLine();
        }
    }
}
