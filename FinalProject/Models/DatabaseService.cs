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
