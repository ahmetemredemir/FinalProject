namespace FinalProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeApp();

            MainPage = new AppShell();
        }

        private async void InitializeApp()
        {
            // Veritabanını başlat
            await FinalProject.Models.DatabaseService.InitializeDatabase();
        }
    }
}
