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
        // Kullanýcý girdilerini al
        string email = emailregister.Text;
        string password = passwordregister.Text;
        string fullName = namesurnameregister.Text;

        // Boþ alan kontrolü
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullName))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        try
        {
            // Yeni kullanýcý oluþtur
            var newUser = new Models.UserInfo
            {
                Email = email,
                Password = password,
                FullName = fullName
            };

            // Kullanýcýyý veritabanýna kaydet
            var database = Models.DatabaseService.GetDatabase();
            await database.InsertAsync(newUser);

            // Kayýt baþarýlý mesajý
            await DisplayAlert("Success", "Registration successful!", "OK");

            // Giriþ sayfasýna yönlendir
            await Shell.Current.GoToAsync("OpeningScreen"); // LoginPage rotasý tanýmlý olmalý
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

        // Veritabanýný baþlat
        await FinalProject.Models.DatabaseService.InitializeDatabase();
    }

}