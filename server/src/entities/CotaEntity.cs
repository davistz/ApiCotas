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
    public double? NumeroCota { get; set; }
    
    [Column("valor")]
    public double? Valor { get; set; }
    
    [Column("status")]
    public StatusCota? Status { get; set; }

    [Column("data_create")]
    public DateTime DataCriacao { get; set; }
    
    [Column("data_update")]
    public DateTime DataUpdate { get; set; }

    public CotaEntity() { }
    public CotaEntity(double? numeroCota, double? valor, StatusCota? status, DateTime dataCriacao, DateTime dataUpdate)
    {
        NumeroCota = numeroCota;
        Valor = valor;
        Status = status;
        DataCriacao = dataCriacao;
        DataUpdate = dataUpdate;
    }
    
    
}