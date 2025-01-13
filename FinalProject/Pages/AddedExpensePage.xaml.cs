using Microsoft.Maui.Controls;
using FinalProject.Models;

namespace FinalProject.Pages
{
    public partial class AddedExpensePage : ContentPage
    {
        public AddedExpensePage()
        {
            InitializeComponent();
        }

        private async void addExpense_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddedExpenseEntry.Text))
            {
                await DisplayAlert("Error", "Please enter an amount.", "OK");
                return;
            }

            if (decimal.TryParse(AddedExpenseEntry.Text, out decimal addedExpense))
            {
                try
                {
                    var database = DatabaseService.GetDatabase();
                    string currentUserEmail = App.CurrentUserEmail;

                    var user = await database.Table<UserInfo>()
                        .FirstOrDefaultAsync(u => u.Email == currentUserEmail);

                    if (user != null)
                    {
                        user.MonthlyExpense += addedExpense;
                        await database.UpdateAsync(user);

                        var transaction = new Transaction
                        {
                            UserEmail = currentUserEmail,
                            Amount = addedExpense,
                            IsIncome = false,
                            Date = DateTime.Now,
                            Description = ExpenseDescription.Text ?? string.Empty
                        };
                        await database.InsertAsync(transaction);

                        await DisplayAlert("Successful", "Expense has added successfully", "OK");
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