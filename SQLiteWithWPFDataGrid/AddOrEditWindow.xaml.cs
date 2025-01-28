using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SQLGrid
{
    /// <summary>
    /// Interaction logic for AddOrEditWindow.xaml
    /// </summary>
    public partial class AddOrEditWindow : Window
    {
        public AddOrEditWindow()
        {
            this.InitializeComponent();
        }

        public Employee SelectedRecord { get; set; }

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

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void employeeIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new(@"^\d+$"); // Allow only digits
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
