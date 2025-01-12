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

        string email = emailregister.Text;
        string password = passwordregister.Text;
        string fullName = namesurnameregister.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullName))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        try
        {
            var newUser = new Models.UserInfo
            {
                Email = email,
                Password = password,
                FullName = fullName
            };

            //Kullanýcýyý veritabanýna kaydet
            var database = Models.DatabaseService.GetDatabase();
            await database.InsertAsync(newUser);

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await FinalProject.Models.DatabaseService.InitializeDatabase();
    }

}