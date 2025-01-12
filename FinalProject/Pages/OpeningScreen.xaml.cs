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
            //Veritaban�ndan kullan�c�y� bul
            var database = FinalProject.Models.DatabaseService.GetDatabase();
            var user = await database.Table<Models.UserInfo>()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                App.CurrentUserEmail = user.Email;

                await Shell.Current.GoToAsync(nameof(AppMainPage));
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

    //private async void ClearDatabaseButton_Clicked(object sender, EventArgs e)
    //{
    //    var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDatabase.db");

    //    // Veritaban� dosyas�n� kontrol et ve sil
    //    if (File.Exists(databasePath))
    //    {
    //        File.Delete(databasePath);
    //        await DisplayAlert("Success", "Database has been reset.", "OK");
    //    }
    //    else
    //    {
    //        await DisplayAlert("Info", "Database file does not exist.", "OK");
    //    }

    //    // Veritaban�n� yeniden olu�tur
    //    await FinalProject.Models.DatabaseService.InitializeDatabase();
    //}

}
