using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCotas.Cotas;

public class ConsorcioEntity
{
    [Column("id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    [Column("nome")]
    public string Nome { get; set; }
    
    [Column("valor_total")]
    public decimal ValorTotal { get; set; }
    
    [Column("numero_participantes")]
    public int NumeroParticipantes { get; set; }
    
    [Column("data_create")]
    public DateTime DataCreate { get; init; }
    
    [Column("data_update")]
    public DateTime DataUpdate { get; set; }
    
    public virtual ICollection<UsuarioConsorcio> UsuarioConsorcios { get; set; } = new List<UsuarioConsorcio>();
}