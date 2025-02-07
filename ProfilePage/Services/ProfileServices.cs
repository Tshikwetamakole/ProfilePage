using ProfilePage.Models;
using System.Text.Json;

namespace ProfilePage.Services;

public class ProfileService
{
    private readonly string _filePath;

    public ProfileService()
    {
        _filePath = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "profile.json");
    }

    public async Task SaveProfileAsync(Profile profile)
    {
        var jsonString = JsonSerializer.Serialize(profile, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        await File.WriteAllTextAsync(_filePath, jsonString);
    }

    public async Task<Profile> LoadProfileAsync()
    {
        if(!File.Exists(_filePath))
            return new Profile();

        var jsonString = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<Profile>(jsonString) ?? new Profile();
    }
}