namespace AnimeMovies.Domain.Shared;

public record Error(string Message, object? Details)
{
    public static readonly Error None = new("No Error", null);
    public static Error ValidationFailed(object? details = null) => new("Validation Failed", details);
    public static Error NotFound() => new("Not Found", null);
    public static Error InternalServerError(object? details = null) => new("Internal Server Error", details);
}
