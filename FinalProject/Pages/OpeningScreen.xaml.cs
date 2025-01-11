namespace FinalProject.Pages;

public partial class OpeningScreen : ContentPage
{
	public OpeningScreen()
	{
		InitializeComponent();
	}

    private void loginbutton_Clicked(object sender, EventArgs e)
    {

    }

    private async void donthaveacc_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}