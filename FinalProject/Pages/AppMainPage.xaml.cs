using Microsoft.Maui.Controls;
using System;
using FinalProject.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FinalProject.Pages;

public partial class AppMainPage : ContentPage
{
    public ObservableCollection<Transaction> Transactions { get; set; }
    public AppMainPage()
    {
        InitializeComponent();
        Transactions = new ObservableCollection<Transaction>();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateIncomeLabel();
        await LoadTransactions();
    }

    private async Task LoadTransactions()
    {
        try
        {
            var database = DatabaseService.GetDatabase();
            string currentUserEmail = App.CurrentUserEmail;

            var transactions = await database.Table<Transaction>()
                                          .Where(t => t.UserEmail == currentUserEmail)
                                          .OrderByDescending(t => t.Date)
                                          .ToListAsync();

            Transactions.Clear();
            foreach (var transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to load transactions.", "OK");
        }
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
                aylikgelir.Text = $"Monthly Income: {user.MonthlyIncome:N2} TL";
                aylikgider.Text = $"Monthly Expense: {user.MonthlyExpense:N2} TL";
                ayliknet.Text = $"Monthly Net: {user.MonthlyIncome - user.MonthlyExpense:N2} TL";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred while loading data.", "OK");
        }
    }

    private async void aylikgelirbutton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(IncomeEntryPage));
    }

    private async void gelireklebutton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddedIncomePage));
    }

    private async void gidereklebutton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddedExpensePage));
    }
}
