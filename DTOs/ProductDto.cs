namespace Lab_8___Carlos_Mamani.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}