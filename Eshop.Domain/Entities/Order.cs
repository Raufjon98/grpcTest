namespace Eshop.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public Cart? Cart { get; set; }
    public Customer? Customer { get; set; }
    public int  CustomerId { get; set; }
    public int CartId { get; set; }
}