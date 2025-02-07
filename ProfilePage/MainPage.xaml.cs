namespace ProfilePage;

public partial class MainPage : ContentPage
{
    private readonly ProfilePage _profilePage;

    public MainPage(ProfilePage profilePage)
    {
        InitializeComponent();
        _profilePage = profilePage;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Navigation.PushAsync(_profilePage);
    }
}