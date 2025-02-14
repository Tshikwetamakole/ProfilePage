using ProfilePage.Models;
using System.Text.Json;

namespace ProfilePage.Services
{
    public class ProfileService
    {
        private readonly string _filePath;

        public ProfileService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "profile.json");
        }

        public async Task<Profile> LoadProfileAsync()
        {
            if(!File.Exists(_filePath))
            {
                return new Profile();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<Profile>(json) ?? new Profile();
        }

        public async Task SaveProfileAsync(Profile profile)
        {
            var json = JsonSerializer.Serialize(profile);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<string> SaveProfileImageAsync(Stream imageStream)
        {
            var imagesFolder = Path.Combine(FileSystem.AppDataDirectory, "ProfileImages");
            Directory.CreateDirectory(imagesFolder);

            var fileName = $"profile_image_{DateTime.Now.Ticks}.jpg";
            var filePath = Path.Combine(imagesFolder, fileName);

            using var fileStream = File.OpenWrite(filePath);
            await imageStream.CopyToAsync(fileStream);

            return filePath;
        }
    }
}