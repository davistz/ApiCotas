using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCotas.Cotas;

public class CotaEntity
{
    [Key]
    [Column("id")]
    public string? Id { get; init; } = Guid.NewGuid().ToString();
    
    [Column("consorcioId")]
    public string? ConsorcioId { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    [Column("numeroCota")]
    public double NumeroCota { get; set; }
    
    [Column("valor")]
    public decimal Valor { get; set; }
    
    [Column("status")]
    public StatusCota Status { get; set; }

    [Column("data_create")]
    public DateTime DataCriacao { get; init; } = DateTime.Now;
    
    [Column("data_update")]
    public DateTime DataUpdate { get; set; } = DateTime.Now;

    public CotaEntity() { }
    public CotaEntity(double numeroCota, decimal valor, StatusCota status)
    {
        NumeroCota = numeroCota;
        Valor = valor;
        Status = status;
       
    }

    public void AtualizarCota(double numeroCota, decimal valor, StatusCota status)
    {
        NumeroCota = numeroCota;
        Valor = valor;
        Status = status;
    }

    public void DesativarCota()
    {
        Status = StatusCota.Inativa;
    }
}