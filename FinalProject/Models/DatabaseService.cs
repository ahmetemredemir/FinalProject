using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinalProject.Models
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection database;

        public static async Task InitializeDatabase()
        {
            if (database == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDatabase.db");
                database = new SQLiteAsyncConnection(databasePath);

                await database.CreateTableAsync<UserInfo>();
                await database.CreateTableAsync<Transaction>();
            }
        }

        public static SQLiteAsyncConnection GetDatabase()
        {
            if (database == null)
            {
                // Eğer veritabanı başlatılmadıysa bağlantıyı burada başlat
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDatabase.db");
                database = new SQLiteAsyncConnection(databasePath);
            }
            return database;
        }

        public static async Task<List<Transaction>> GetTransactionsForUser(string userEmail)
        {
            var database = GetDatabase();
            return await database.Table<Transaction>()
                .Where(t => t.UserEmail == userEmail)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public static async Task<(decimal TotalIncome, decimal TotalExpense)> GetUserTotals(string userEmail)
        {
            var database = GetDatabase();
            var transactions = await database.Table<Transaction>()
                .Where(t => t.UserEmail == userEmail)
                .ToListAsync();

            decimal totalIncome = transactions.Where(t => t.IsIncome).Sum(t => t.Amount);
            decimal totalExpense = transactions.Where(t => !t.IsIncome).Sum(t => t.Amount);

            return (totalIncome, totalExpense);
        }

        public static async Task ClearAllData()
        {
            var database = GetDatabase();
            if (database != null)
            {
                // UserInfo tablosundaki tüm verileri sil
                await database.DeleteAllAsync<UserInfo>();
            }
        }


    }
}
