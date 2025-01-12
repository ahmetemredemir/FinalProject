using Microsoft.Maui.Controls;
using System;
using FinalProject.Models;

namespace FinalProject.Pages
{
    public partial class IncomeEntryPage : ContentPage
    {
        public IncomeEntryPage()
        {
            InitializeComponent();
        }

        private async void confirmIncome_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IncomeEntry.Text))
            {
                await DisplayAlert("Error", "Enter your monthly income.", "OK");
                return;
            }

            if (decimal.TryParse(IncomeEntry.Text, out decimal income))
            {
                try
                {
                    var database = DatabaseService.GetDatabase();
                    string currentUserEmail = App.CurrentUserEmail;

                    var user = await database.Table<UserInfo>().FirstOrDefaultAsync(u => u.Email == currentUserEmail);

                    if (user != null)
                    {
                        user.MonthlyIncome = income;
                        await database.UpdateAsync(user);
                        await DisplayAlert("Successful!", "Your monthly income has been added successfully. ", "OK");
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
                await DisplayAlert("Error", "Enter a valid number.", "OK");
            }
        }
    }
}