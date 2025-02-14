using System.Text.Json.Serialization;

namespace ProfilePage.Models
{
    public class Profile
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("surname")]
        public string Surname { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("bio")]
        public string Bio { get; set; } = string.Empty;

        [JsonPropertyName("profileImage")]
        public string ProfileImage { get; set; } = string.Empty;
    }
}