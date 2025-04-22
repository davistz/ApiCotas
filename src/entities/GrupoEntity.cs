using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCotas.Cotas;

public class GrupoEntity
{
    public GrupoEntity()
    {
    }

    public GrupoEntity(string nome, decimal valorTotal, int numeroParticipantes, string criadorId, string criadorNome, UserEntity criador)
    {
        Nome = nome;
        ValorTotal = valorTotal;
        NumeroParticipantes = numeroParticipantes;
        CriadorId = criadorId;
        CriadorNome = criadorNome;
        Criador = criador;
    }

    [Column("id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    [Column("nome")]
    public string Nome { get; set; }
    
    [Column("valor_total")]
    public decimal ValorTotal { get; set; }
    
    [Column("numero_participantes")]
    public int NumeroParticipantes { get; set; }

    [Column("tempo")]
    public double Tempo { get; set; }

    [Column("data_create")]
    public DateTime DataCreate { get; init; }
    
    [Column("data_update")]
    public DateTime DataUpdate { get; set; }
    
    [Column("criador_id")] 
    public string CriadorId { get; set; }
    
    [Column("nome_criador")] 
    public string CriadorNome { get; set; }
    
    public virtual UserEntity Criador { get; set; } 
}