namespace Löwen.Domain.ConfigurationClasses.JWT;

public class JWT
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required short Duration { get; set; }
    public required string SigningKey { get; set; }
}

