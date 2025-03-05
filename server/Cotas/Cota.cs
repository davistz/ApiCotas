namespace ApiCotas.Cotas;

public class Cota
{
    public Guid Id { get; init; }
    public Guid ConsorcioId { get; set; }
    public double NumeroCota { get; set; }
    public double Valor { get; set; }
    public StatusCota Status { get; }
    public DateTime DataCriacao { get; set; }

    public Cota(Guid consorcioId, double numeroCota, double valor, StatusCota status, DateTime dataCriacao)
    {
        Id = Guid.NewGuid();
        ConsorcioId = consorcioId;
        NumeroCota = numeroCota;
        Valor = valor;
        Status = status; 
        DataCriacao = dataCriacao;
    }
}