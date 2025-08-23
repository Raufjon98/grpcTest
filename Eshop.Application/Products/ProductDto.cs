namespace Eshop.Application.Products;

public class ProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int  CategoryId { get; set; }
}