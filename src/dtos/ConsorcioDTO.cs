namespace ApiCotas.dtos;

public record ConsorcioDTO(string Id, String Nome, decimal ValorTotal, double Tempo, double NumeroParticipantes, string CriadorNome, string CriadorId);