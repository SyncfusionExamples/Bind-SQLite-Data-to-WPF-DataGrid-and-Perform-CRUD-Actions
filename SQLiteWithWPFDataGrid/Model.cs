using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using SQLite;

namespace SQLGrid
{
    public class Employee : INotifyPropertyChanged
    {
        private double _EmployeeID;
        private string? _Name;
        private string? _location;
        private string? _Title;
        private DateTime? _BirthDate;
        private string? _Gender;
        private bool employeeStatus;
        private string? _email;

        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        /// <value>The employee ID.</value>

        [PrimaryKey]
        [DisplayName("Employee ID")]
        public double EmployeeID
        {
            get
            {
                return this._EmployeeID;
            }
            set
            {
                this._EmployeeID = value;
                this.RaisePropertyChanged(nameof(EmployeeID));
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string? Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
                this.RaisePropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets or sets the Location.
        /// </summary>
        /// <value>The location.</value>
        public string? Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
                this.RaisePropertyChanged(nameof(Location));
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string? Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
                this.RaisePropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// Gets or sets the BirthDate.
        /// </summary>
        /// <value>The BirthDate.</value>
        public DateTime? BirthDate
        {
            get
            {
                return this._BirthDate;
            }
            set
            {
                this._BirthDate = value;
                this.RaisePropertyChanged(nameof(BirthDate));
            }
        }

        /// <summary>
        /// Gets or sets the Gender.
        /// </summary>
        /// <value>The Gender.</value>
        public string? Gender
        {
            get
            {
                return this._Gender;
            }
            set
            {
                this._Gender = value;
                this.RaisePropertyChanged(nameof(Gender));
            }
        }

        /// <summary>
        /// Gets or sets the EmployeeStatus.
        /// </summary>
        /// <value>The EmployeeStatus.</value>
        [DisplayName("Employee Status")]
        public bool EmployeeStatus
        {
            get
            {
                return employeeStatus;
            }
            set
            {
                employeeStatus = value;
                this.RaisePropertyChanged(nameof(EmployeeStatus));
            }
        }

        /// <summary>
        /// Gets or sets the EMail.
        /// </summary>
        /// <value>The EMail.</value>
        [DisplayName("E-Mail")]
        public string? EMail
        {
            get { return _email; }
            set
            {
                _email = value;
                this.RaisePropertyChanged(nameof(EMail));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}