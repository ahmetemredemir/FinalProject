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
                // Veritabanı dosyasının yolu
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDatabase.db");
                database = new SQLiteAsyncConnection(databasePath);

                // Kullanıcı tablosunu oluştur
                await database.CreateTableAsync<UserInfo>();
            }
        }

        public static SQLiteAsyncConnection GetDatabase()
        {
            return database;
        }
    }
}
