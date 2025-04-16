namespace DevExpert.Marketplace.Core.Configurations;

public class Jwt
{
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string TokenTypeAccessToken { get; set; }
    public required int ExpireMinutes { get; set; }

    public static Jwt? Instance { get; private set; }

    public static void Initialize(Jwt? jwt)
    {
        Instance = jwt;
    }
}