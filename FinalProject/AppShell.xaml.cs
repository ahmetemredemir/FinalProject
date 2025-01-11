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

            Routing.RegisterRoute(nameof(AppMainPage), typeof(AppMainPage));
        }
    }
}
