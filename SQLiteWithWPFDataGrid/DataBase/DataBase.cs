using Microsoft.VisualBasic;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLGrid.DataBase
{
    public class SQLiteDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public const string DatabaseFilename = "SQLiteData.db";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename);

        public SQLiteDatabase()
        {
            _database = new SQLiteAsyncConnection(DatabasePath, Flags);
            _database.CreateTableAsync<Employee>();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _database.Table<Employee>().ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Employee employee)
        {
            return await _database.Table<Employee>().Where(i => i.EmployeeID == employee.EmployeeID).FirstOrDefaultAsync();
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            return await _database.InsertAsync(employee);
        }

        public Task<int> DeleteEmployeeAsync(Employee employee)
        {
            return _database.DeleteAsync(employee);
        }

        public Task<int> UpdateEmployeeAsync(Employee employee)
        {
            if (employee.EmployeeID != 0)
                return _database.UpdateAsync(employee);
            else
                return _database.InsertAsync(employee);
        }
    }
}
