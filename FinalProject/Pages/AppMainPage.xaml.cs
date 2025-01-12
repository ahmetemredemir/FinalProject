using Microsoft.Maui.Controls;
using System;
using FinalProject.Models;

namespace FinalProject.Pages;

public partial class AppMainPage : ContentPage
{
    public AppMainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateIncomeLabel();
    }

    private async Task UpdateIncomeLabel()
    {
        try
        {
            var database = DatabaseService.GetDatabase();
            string currentUserEmail = App.CurrentUserEmail;

            var user = await database.Table<UserInfo>().FirstOrDefaultAsync(u => u.Email == currentUserEmail);

            if (user != null)
            {
                aylikgelir.Text = $"Aylýk Gelir: {user.MonthlyIncome} TL";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred while loading income data.", "OK");
        }
    }

    private async void aylikgelirbutton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(IncomeEntryPage));
    }

    private void gelireklebutton_Clicked(object sender, EventArgs e)
    {

    }

    private void gidereklebutton_Clicked(object sender, EventArgs e)
    {

    }
}
