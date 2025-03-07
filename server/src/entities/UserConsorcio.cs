using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCotas.Cotas;

public class UsuarioConsorcio
{
    [Column("user_id")]
    public string UsuarioId { get; set; }
    
    [Column("consorcio_id")]
    public string ConsorcioId { get; set; }

    public virtual UserEntity Usuario { get; set; }

    public virtual ConsorcioEntity Consorcio { get; set; }
}

