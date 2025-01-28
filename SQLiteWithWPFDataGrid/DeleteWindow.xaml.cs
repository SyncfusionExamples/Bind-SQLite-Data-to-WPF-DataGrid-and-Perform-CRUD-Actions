using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        public Employee SelectedRecord { get; set; }

        public DeleteWindow()
        {
            this.InitializeComponent();
        }

        private async void OnYesClick(object sender, RoutedEventArgs e)
        {
            await App.Database.DeleteEmployeeAsync(this.SelectedRecord);
            this.Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
