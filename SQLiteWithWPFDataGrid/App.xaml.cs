using SQLGrid.DataBase;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SQLGrid
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static SQLiteDatabase? database;

        // Create the database connection as a singleton.
        public static SQLiteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    SQLitePCL.Batteries_V2.Init();
                    database = new SQLiteDatabase();
                }
                return database;
            }
        }
    }
}
