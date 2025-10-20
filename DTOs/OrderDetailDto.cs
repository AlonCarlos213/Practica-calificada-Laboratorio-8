namespace Lab_8___Carlos_Mamani.DTOs;

public class OrderDetailDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    // Campos “enriquecidos”
    public string? ProductName { get; set; }
    public string? ClientName { get; set; }
    public DateTime? OrderDate { get; set; }
}