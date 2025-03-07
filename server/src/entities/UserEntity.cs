using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCotas.Cotas;

public class UserEntity
{
    [Column("id")]
    public string? Id { get; init; } = Guid.NewGuid().ToString();
    
    [Column("nome")]
    public string Nome { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("senha")]
    public string Senha { get; set; }
    
    [Column("data_create")]
    public DateTime DataCriacao { get; init; } = DateTime.Now;
    
    [Column("data_update")]
    public DateTime DataUpdate { get; set; } = DateTime.Now;
    
    public virtual ICollection<UsuarioConsorcio> UsuarioConsorcios { get; set; } = new List<UsuarioConsorcio>();
}