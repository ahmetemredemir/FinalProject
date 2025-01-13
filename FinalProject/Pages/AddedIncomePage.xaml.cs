using Microsoft.Maui.Controls;
using FinalProject.Models;

namespace FinalProject.Pages
{
    public partial class AddedIncomePage : ContentPage
    {
        public AddedIncomePage()
        {
            InitializeComponent();
        }

        private async void addIncome_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddedIncomeEntry.Text))
            {
                await DisplayAlert("Error", "Please enter an amount.", "OK");
                return;
            }

            if (decimal.TryParse(AddedIncomeEntry.Text, out decimal addedIncome))
            {
                try
                {
                    var database = DatabaseService.GetDatabase();
                    string currentUserEmail = App.CurrentUserEmail;

                    var user = await database.Table<UserInfo>()
                        .FirstOrDefaultAsync(u => u.Email == currentUserEmail);

                    if (user != null)
                    {
                        user.MonthlyIncome += addedIncome;
                        await database.UpdateAsync(user);

                        var transaction = new Transaction
                        {
                            UserEmail = currentUserEmail,
                            Amount = addedIncome,
                            IsIncome = true,
                            Date = DateTime.Now,
                            Description = IncomeDescription.Text ?? string.Empty
                        };
                        await database.InsertAsync(transaction);

                        await DisplayAlert("Successful", "Income has added successfully.", "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "An error occured.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid amount.", "OK");
            }
        }
    }
}