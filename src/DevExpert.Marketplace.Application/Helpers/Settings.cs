namespace DevExpert.Marketplace.Application.Helpers;

public class Settings
{
    public required string ProductImageDirectoryPath { get; set; }
    
    public static Settings? Instance { get; private set; }

    public static void Initialize(Settings? settings)
    {
        Instance = settings;
    }
}