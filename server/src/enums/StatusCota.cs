using System.Text.Json.Serialization;

namespace ApiCotas.Cotas;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusCota
{
    Ativa,
    Contemplada,
    Inativa
}