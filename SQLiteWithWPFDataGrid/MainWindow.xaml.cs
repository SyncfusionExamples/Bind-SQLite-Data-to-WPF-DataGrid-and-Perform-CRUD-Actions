using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Activated += OnWindowActivated;
        }

        private async void OnWindowActivated(object? sender, EventArgs e)
        {
            sfDataGrid.ItemsSource = await App.Database.GetEmployeesAsync();
        }


        ////private async void OnActivated(object sender, WindowActivatedEventArgs args)
        ////{
        ////    if (args.WindowActivationState == WindowActivationState.CodeActivated)
        ////        sfDataGrid.ItemsSource = await App.Database.GetEmployeesAsync();
        //}

        private void OnAddMenuClick(object sender, RoutedEventArgs e)
        {
            AddOrEditWindow addWindow = new AddOrEditWindow();
            addWindow.Title = "Add Record";
            addWindow.Show();
        }

        private void OnEditMenuClick(object sender, RoutedEventArgs e)
        {
            AddOrEditWindow editWindow = new AddOrEditWindow();
            editWindow.DataContext = (Employee)sfDataGrid.SelectedItem;
            editWindow.Title = "Edit Record";
            editWindow.SelectedRecord = (Employee)sfDataGrid.SelectedItem;
            editWindow.Show();
        }

        private void OnDeleteMenuClick(object sender, RoutedEventArgs e)
        {
            DeleteWindow deleteWindow = new DeleteWindow();
            deleteWindow.SelectedRecord = (Employee)sfDataGrid.SelectedItem;
            deleteWindow.Show();
        }
    }
}