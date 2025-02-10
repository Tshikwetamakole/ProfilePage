using ProfilePage.Models;
using ProfilePage.Services;

namespace ProfilePage.Views;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileService _profileService;
    private Profile _currentProfile;

    public ProfilePage(ProfileService profileService)
    {
        InitializeComponent();
        _profileService = profileService;
        LoadProfile();
    }

    private async void LoadProfile()
    {
        _currentProfile = await _profileService.LoadProfileAsync();
        BindingContext = _currentProfile;

        // Set default image if none exists
        if(string.IsNullOrEmpty(_currentProfile.ProfileImagePath))
        {
            imgProfile.Source = "layer.png"; // Default placeholder image
        }
    }

    private async void OnChangePictureClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Profile Image",
                FileTypes = FilePickerFileType.Images
            });

            if(result != null)
            {
                // Copy the selected image to the app's local storage
                var localFilePath = Path.Combine(FileSystem.AppDataDirectory,
                    $"profile_picture{Path.GetExtension(result.FileName)}");

                using(var stream = await result.OpenReadAsync())
                using(var fileStream = File.OpenWrite(localFilePath))
                {
                    await stream.CopyToAsync(fileStream);
                }

                // Update the profile image
                _currentProfile.ProfileImagePath = localFilePath;
                imgProfile.Source = ImageSource.FromFile(localFilePath);
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error",
                             $"Failed to pick image: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            await _profileService.SaveProfileAsync(_currentProfile);
            await DisplayAlert("Success", "Profile saved successfully!", "OK");
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error",
                             $"Failed to save profile: {ex.Message}", "OK");
        }
    }
}