namespace FinalProject.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private async void gobackbutton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void registerbutton_Clicked(object sender, EventArgs e)
    {
        // Kullan�c� girdilerini al
        string email = emailregister.Text;
        string password = passwordregister.Text;
        string fullName = namesurnameregister.Text;

        // Bo� alan kontrol�
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullName))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        try
        {
            // Yeni kullan�c� olu�tur
            var newUser = new Models.UserInfo
            {
                Email = email,
                Password = password,
                FullName = fullName
            };

            // Kullan�c�y� veritaban�na kaydet
            var database = Models.DatabaseService.GetDatabase();
            await database.InsertAsync(newUser);

            // Kay�t ba�ar�l� mesaj�
            await DisplayAlert("Success", "Registration successful!", "OK");

            // Giri� sayfas�na y�nlendir
            await Shell.Current.GoToAsync("OpeningScreen"); // LoginPage rotas� tan�ml� olmal�
        }
        catch (Exception ex)
        {
            // Hata durumunda mesaj
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Veritaban�n� ba�lat
        await FinalProject.Models.DatabaseService.InitializeDatabase();
    }

}