using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiCotas.Cotas;

public class CotaEntity
{
    
    [Column("id")]
    public string? Id { get; init; } = Guid.NewGuid().ToString();
    
    [Column("consorcioId")] 
    public string ConsorcioId { get; set; }  

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
    
    public virtual GrupoEntity Consorcio { get; set; }

    public CotaEntity() { }

    public CotaEntity(double numeroCota, decimal valor, string consorcioId, StatusCota status) 
    {
        NumeroCota = numeroCota;
        Valor = valor;
        ConsorcioId = consorcioId; 
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