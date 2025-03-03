namespace ApiCotas.Cotas;

public class Cota
{
    public Guid Id { get; init; }
    public Guid ConsorcioId { get; set; }
    public Double NumeroCota { get; set; }
    public Double Valor { get; set; }
    public String Status { get; }
    public DateTime DataCriacao { get; set; }
}