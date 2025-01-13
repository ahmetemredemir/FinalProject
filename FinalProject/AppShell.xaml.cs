using FinalProject.Pages;

namespace FinalProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(OpeningScreen), typeof(OpeningScreen));

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

            Routing.RegisterRoute(nameof(IncomeEntryPage), typeof(IncomeEntryPage));

            Routing.RegisterRoute(nameof(AppMainPage), typeof(AppMainPage));

            Routing.RegisterRoute(nameof(AddedIncomePage), typeof(AddedIncomePage));

            Routing.RegisterRoute(nameof(AddedExpensePage), typeof(AddedExpensePage));
        }
    }
}
