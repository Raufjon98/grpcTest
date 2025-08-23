namespace Eshop.Domain.Entities;

public class Cart
{
    public int  Id { get; set; }
    public int  CustomerId { get; set; }
    public Order? Order { get; set; }
    public Customer? Customer { get; set; }
}