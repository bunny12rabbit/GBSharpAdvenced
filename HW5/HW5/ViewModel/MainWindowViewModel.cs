using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW5.Model;

namespace HW5.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Employee> employees;
        ObservableCollection<Department> department;
        public Employee selectedEmployee;
        public ObservableCollection<Employee> Employees { get { return this.employees; }  }
        public ObservableCollection<Department> Departments { get { return this.department; } }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }



        public MainWindowViewModel()
        {
            employees = Employee.GetEmployees();
            department = Department.GetDepartments();


        }

        public void AddEmployee()
        {
            Employee employee = new Employee();
            employees.Insert(0, employee);
            SelectedEmployee = employee;
        }
        public void DeleteEmployee()
        {
            if (selectedEmployee != null)
            {
                Employees.Remove(SelectedEmployee);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Обновление свойства при редактировании привязанных данных в интерфейсе
        /// </summary>
        /// <param name="propertyName">Редактируемое поле</param>
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
