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