using ProfilePage.Models;
using ProfilePage.Services;

namespace ProfilePage.Views
{
    public partial class ProfilePage : ContentPage
    {
        private readonly ProfileService _profileService;
        private Profile _currentProfile;

        public ProfilePage()
        {
            InitializeComponent();
            _profileService = new ProfileService();
            LoadProfile();
        }

        private async void LoadProfile()
        {
            _currentProfile = await _profileService.LoadProfileAsync();

            NameEntry.Text = _currentProfile.Name;
            SurnameEntry.Text = _currentProfile.Surname;
            EmailEntry.Text = _currentProfile.Email;
            BioEditor.Text = _currentProfile.Bio;

            if(!string.IsNullOrEmpty(_currentProfile.ProfileImage))
            {
                ProfileImage.Source = _currentProfile.ProfileImage;
            }
        }

        private async void OnSaveProfile(object sender, EventArgs e)
        {
            _currentProfile.Name = NameEntry.Text;
            _currentProfile.Surname = SurnameEntry.Text;
            _currentProfile.Email = EmailEntry.Text;
            _currentProfile.Bio = BioEditor.Text;

            await _profileService.SaveProfileAsync(_currentProfile);
            await DisplayAlert("Success", "Profile saved successfully!", "OK");
        }

        private async void OnChangeProfilePicture(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                if(photo != null)
                {
                    using var stream = await photo.OpenReadAsync();
                    var imagePath = await _profileService.SaveProfileImageAsync(stream);

                    _currentProfile.ProfileImage = imagePath;
                    ProfileImage.Source = imagePath;
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Failed to pick photo: " + ex.Message, "OK");
            }
        }
    }
}