# Bind SQLite Data to WPF DataGrid and Perform CRUD Actions

## Overview

This repository demonstrates a comprehensive example of how to bind SQLite database data to a WPF DataGrid and efficiently perform CRUD (Create, Read, Update, Delete) operations. The sample showcases an Employee Management system where users can view, add, edit, and delete employee records stored in a local SQLite database.

## What is SQLite?

SQLite is a lightweight, file-based relational database that does not require a separate database server. It stores all data in a single file (SQLiteData.db) on the local machine and is ideal for desktop applications. SQLite provides a complete SQL database engine that can handle data storage, retrieval, and manipulation efficiently.

## How SQLite is Used in This Application

### Database Setup

The application creates an SQLite database connection using `SQLiteAsyncConnection` which allows asynchronous database operations:

```csharp
public class SQLiteDatabase
{
    readonly SQLiteAsyncConnection _database;
    public const string DatabaseFilename = "SQLiteData.db";
    
    public static string DatabasePath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename);
        
    public SQLiteDatabase()
    {
        _database = new SQLiteAsyncConnection(DatabasePath, Flags);
        _database.CreateTableAsync<Employee>();
    }
}
```

The database file is stored in the LocalApplicationData folder and is created automatically on first run. The Employee table is created with the necessary schema.

### Reading Data

When the application starts, it loads all employee records from the SQLite database and displays them in the WPF DataGrid:

```csharp
private async void OnWindowActivated(object? sender, EventArgs e)
{
    sfDataGrid.ItemsSource = await App.Database.GetEmployeesAsync();
}
```

The `GetEmployeesAsync()` method retrieves all records from the SQLite database:

```csharp
public async Task<List<Employee>> GetEmployeesAsync()
{
    return await _database.Table<Employee>().ToListAsync();
}
```

## WPF DataGrid Binding

The application uses Syncfusion's `SfDataGrid` control to display and manage employee data. The DataGrid is configured with the following features:

```xml
<syncfusion:SfDataGrid x:Name="sfDataGrid"
                       AutoGenerateColumns="True"
                       AllowDraggingColumns="True"
                       AllowResizingColumns="True"
                       AllowSorting="True"
                       SelectionMode="Single">
    <!-- Context Menu for CRUD operations -->
</syncfusion:SfDataGrid>
```

**Key Features:**
- **AutoGenerateColumns**: Automatically creates columns for all properties of the Employee class
- **AllowSorting**: Users can sort data by clicking on column headers
- **AllowResizingColumns**: Columns can be resized by dragging
- **SelectionMode**: Users can select individual employee records for editing or deletion
- **RecordContextMenu**: Right-click context menu provides quick access to CRUD operations

## Performing CRUD Actions

### CREATE - Adding New Employee Records

Users can add new employee records through the Add menu option:

```csharp
private void OnAddMenuClick(object sender, RoutedEventArgs e)
{
    AddOrEditWindow addWindow = new AddOrEditWindow();
    addWindow.Title = "Add Record";
    addWindow.Show();
}
```

When the save button is clicked in the AddOrEditWindow, a new employee record is inserted into the SQLite database:

```csharp
 private async void OnSaveClick(object sender, RoutedEventArgs e)
{
    bool isEdit = true;
    if (SelectedRecord == null)
    {
        isEdit = false;
        SelectedRecord = new Employee();
    }

    double employeeID;
    if (double.TryParse(this.employeeIDTextBox.Text, out employeeID))
        SelectedRecord.EmployeeID = employeeID;
    SelectedRecord.Name = this.employeeNameTextBox.Text;
    SelectedRecord.EMail = this.EmployeeMailTextBox.Text;
    SelectedRecord.Gender = this.GenderComboBox.SelectedItem.ToString();
    SelectedRecord.BirthDate = this.EmployeeBirthDatePicker.SelectedDate;
    SelectedRecord.Location = this.EmployeeLocationTextBox.Text;

    if (isEdit)
        await App.Database.UpdateEmployeeAsync(SelectedRecord);
    else
        await App.Database.AddEmployeeAsync(SelectedRecord);

    this.Close();
}
```

The `AddEmployeeAsync()` method inserts the record:

```csharp
public async Task<int> AddEmployeeAsync(Employee employee)
{
    return await _database.InsertAsync(employee);
}
```

### READ - Displaying Data in DataGrid

The DataGrid continuously displays the employee data stored in SQLite. Users can:
- Sort data by clicking column headers
- Resize columns to view different fields
- Select individual records for viewing details
- Search and filter data through the DataGrid's built-in features

### UPDATE - Editing Existing Records

Users can edit employee records by selecting a record and clicking the Edit option:

```csharp
private void OnEditMenuClick(object sender, RoutedEventArgs e)
{
    AddOrEditWindow editWindow = new AddOrEditWindow();
    editWindow.DataContext = (Employee)sfDataGrid.SelectedItem;
    editWindow.Title = "Edit Record";
    editWindow.SelectedRecord = (Employee)sfDataGrid.SelectedItem;
    editWindow.Show();
}
```

The selected employee record is passed to the edit window where users can modify the data and save:

```csharp
if (isEdit)
    await App.Database.UpdateEmployeeAsync(SelectedRecord);
```

The `UpdateEmployeeAsync()` method handles the update operation:

```csharp
public Task<int> UpdateEmployeeAsync(Employee employee)
{
    if (employee.EmployeeID != 0)
        return _database.UpdateAsync(employee);
    else
        return _database.InsertAsync(employee);
}
```

### DELETE - Removing Records

Users can delete employee records by selecting a record and clicking the Delete option:

```csharp
private void OnDeleteMenuClick(object sender, RoutedEventArgs e)
{
    DeleteWindow deleteWindow = new DeleteWindow();
    deleteWindow.SelectedRecord = (Employee)sfDataGrid.SelectedItem;
    deleteWindow.Show();
}
```

When confirmed, the record is deleted from the SQLite database:

```csharp
private async void OnYesClick(object sender, RoutedEventArgs e)
{
    await App.Database.DeleteEmployeeAsync(this.SelectedRecord);
    this.Close();
}
```

The `DeleteEmployeeAsync()` method removes the record:

```csharp
public Task<int> DeleteEmployeeAsync(Employee employee)
{
    return _database.DeleteAsync(employee);
}
```