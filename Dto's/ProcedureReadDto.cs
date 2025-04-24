namespace Backend.Dtos;

public record ProcedureReadDto
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Steps { get; init; }
    public string? Department { get; init; }
    public string? CreatedAt { get; init; }        // already formatted
    public string? LastUpdatedAt { get; init; }        // formatted or null
    public string? CreatedBy { get; init; }
}
