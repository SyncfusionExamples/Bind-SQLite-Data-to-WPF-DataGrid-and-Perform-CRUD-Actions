
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using System.Windows.Input;
using SQLite;

namespace SQLGrid
{
    public class EmployeeViewModel : INotifyPropertyChanged, IDisposable
    {
        public EmployeeViewModel()
        {
            PopulateData();
            employees = this.GetEmployeeDetails(10);
            PopulateDB();
        }

        private async void PopulateDB()
        {
            foreach (Employee contact in Employees)
            {
                var item = await App.Database.GetEmployeeAsync(contact);
                if (item == null)
                    await App.Database.AddEmployeeAsync(contact);
            }
        }

        private ObservableCollection<Employee> employees;

        /// <summary>
        /// Get or set the EmployeeDetails
        /// </summary>
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return employees;
            }

        }

        public Employee SelectedRecord { get; set; }

        Random r = new Random();
        Dictionary<string, string> gender = new Dictionary<string, string>();

        /// <summary>
        /// Get the EmployeeDetails
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ObservableCollection<Employee> GetEmployeeDetails(int count)
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            for (int i = 1; i <= count; i++)
            {
                var name = employeeName[r.Next(employeeName.Length - 1)];
                var emp = new Employee()
                {
                    EmployeeID = 1000 + i,
                    Name = name,
                    Location = location[r.Next(1, 8)],
                    Gender = gender[name],
                    Title = title[r.Next(title.Length - 1)],
                    BirthDate = new DateTime(r.Next(1975, 1985), r.Next(1, 12), r.Next(1, 28)),
                    EMail = name + "@" + mail[r.Next(0, mail.Count() - 1)],
                    EmployeeStatus = r.Next() % 2 == 0 ? true : false,
                };
                employees.Add(emp);
            }

            return employees;
        }

        /// <summary>
        /// Populate the data for Gender
        /// </summary>
        private void PopulateData()
        {
            gender.Add("Sean Jacobson", "Male");
            gender.Add("Phyllis Allen", "Male");
            gender.Add("Marvin Allen", "Male");
            gender.Add("Michael Allen", "Male");
            gender.Add("Cecil Allison", "Male");
            gender.Add("Oscar Alpuerto", "Male");
            gender.Add("Sandra Altamirano", "Female");
            gender.Add("Selena Alvarad", "Female");
            gender.Add("Emilio Alvaro", "Female");
            gender.Add("Maxwell Amland", "Male");
            gender.Add("Mae Anderson", "Male");
            gender.Add("Ramona Antrim", "Female");
            gender.Add("Sabria Appelbaum", "Male");
            gender.Add("Hannah Arakawa", "Male");
            gender.Add("Kyley Arbelaez", "Male");
            gender.Add("Tom Johnston", "Female");
            gender.Add("Thomas Armstrong", "Female");
            gender.Add("John Arthur", "Male");
            gender.Add("Chris Ashton", "Female");
            gender.Add("Teresa Atkinson", "Male");
            gender.Add("John Ault", "Male");
            gender.Add("Robert Avalos", "Male");
            gender.Add("Stephen Ayers", "Male");
            gender.Add("Phillip Bacalzo", "Male");
            gender.Add("Gustavo Achong", "Male");
            gender.Add("Catherine Abel", "Male");
            gender.Add("Kim Abercrombie", "Male");
            gender.Add("Humberto Acevedo", "Male");
            gender.Add("Pilar Ackerman", "Male");
            gender.Add("Frances Adams", "Female");
            gender.Add("Margar Smith", "Male");
            gender.Add("Carla Adams", "Male");
            gender.Add("Jay Adams", "Male");
            gender.Add("Ronald Adina", "Female");
            gender.Add("Samuel Agcaoili", "Male");
            gender.Add("James Aguilar", "Female");
            gender.Add("Robert Ahlering", "Male");
            gender.Add("Francois Ferrier", "Male");
            gender.Add("Kim Akers", "Male");
            gender.Add("Lili Alameda", "Female");
            gender.Add("Amy Alberts", "Male");
            gender.Add("Anna Albright", "Female");
            gender.Add("Milton Albury", "Male");
            gender.Add("Paul Alcorn", "Male");
            gender.Add("Gregory Alderson", "Male");
            gender.Add("J. Phillip Alexander", "Male");
            gender.Add("Michelle Alexander", "Male");
            gender.Add("Daniel Blanco", "Male");
            gender.Add("Cory Booth", "Male");
            gender.Add("James Bailey", "Female");
        }

        string[] title = new string[]
        {
            "Marketing Assistant", "Engineering Manager", "Senior Tool Designer", "Tool Designer",
            "Marketing Manager", "Production Supervisor", "Production Technician", "Design Engineer",
            "Vice President", "Product Manager", "Network Administrator", "HR Manager", "Stocker",
            "Clerk", "QA Supervisor", "Services Manager", "Master Scheduler",
            "Marketing Specialist", "Recruiter", "Maintenance Supervisor",
        };

        string[] employeeName = new string[]
        {
            "Sean Jacobson", "Phyllis Allen", "Marvin Allen", "Michael Allen", "Cecil Allison",
            "Oscar Alpuerto", "Sandra Altamirano", "Selena Alvarad", "Emilio Alvaro", "Maxwell Amland",
            "Mae Anderson", "Ramona Antrim", "Sabria Appelbaum", "Hannah Arakawa", "Kyley Arbelaez",
            "Tom Johnston", "Thomas Armstrong", "John Arthur", "Chris Ashton", "Teresa Atkinson",
            "John Ault", "Robert Avalos", "Stephen Ayers", "Phillip Bacalzo", "Gustavo Achong",
            "Catherine Abel", "Kim Abercrombie", "Humberto Acevedo", "Pilar Ackerman", "Frances Adams",
            "Margar Smith", "Carla Adams", "Jay Adams", "Ronald Adina", "Samuel Agcaoili",
            "James Aguilar", "Robert Ahlering", "Francois Ferrier", "Kim Akers", "Lili Alameda",
            "Amy Alberts", "Anna Albright", "Milton Albury", "Paul Alcorn", "Gregory Alderson",
            "J. Phillip Alexander", "Michelle Alexander", "Daniel Blanco", "Cory Booth",
            "James Bailey"
        };

        string[] location = new string[] { "UK", "USA", "Sweden", "France", "Canada", "Argentina", "Austria", "Germany", "Mexico" };

        string[] mail = new string[] { "arpy.com", "sample.com", "rpy.com", "jourrapide.com" };

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposable)
        {
            if (Employees != null)
                Employees.Clear();
        }
    }
}