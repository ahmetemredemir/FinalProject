namespace FinalProject.Pages;

public partial class IncomeEntryPage : ContentPage
{
	public IncomeEntryPage()
	{
		InitializeComponent();
	}

    private async void confirmIncome_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(AppMainPage));
    }
}