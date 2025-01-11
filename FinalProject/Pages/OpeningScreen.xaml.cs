namespace FinalProject.Pages;

public partial class OpeningScreen : ContentPage
{
	public OpeningScreen()
	{
		InitializeComponent();
	}

    private async void loginbutton_Clicked(object sender, EventArgs e)
    {
        string email = emailentry.Text;
        string password = passwordentry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        try
        {
            //Veritabanýndan kullanýcýyý bul
            var database = FinalProject.Models.DatabaseService.GetDatabase();
            var user = await database.Table<Models.UserInfo>()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                await Shell.Current.GoToAsync("AppMainPage");
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void donthaveacc_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}