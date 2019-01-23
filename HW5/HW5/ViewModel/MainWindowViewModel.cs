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
        static Random rnd = new Random();

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
            _InitializeModel(100, 100);
        }

        /// <summary>
        /// Метод инициализации модели. Генерирует отделы и сотрудников
        /// </summary>
        /// <param name="DepartmentCount">Количество отделов для генерации</param>
        /// <param name="EmployeeCont">Количество сотрудников для генерации</param>
        public void _InitializeModel( int DepartmentCount, int EmployeeCont)
        {
            //employees = Employee.GetEmployees();
            //department = Department.GetDepartments();
            employees = new ObservableCollection<Employee>();
            department = new ObservableCollection<Department>();

            for (int i = 0; i < DepartmentCount; i++)
            {
                department.Add(new Department($"Отдел {i + 1}", i));
            }

            for (int i = 0; i < EmployeeCont; i++)
            {
                employees.Add(
                    new Employee($"Имя_{i + 1}",
                    $"Фамилия_{i + 1}",
                    $"Отчество_{i + 1}",
                    $"{rnd.Next(1, 30)}.{rnd.Next(1,12)}.{rnd.Next(1960, 2000)}",
                    rnd.Next(department.Count)));
            }
        }

        public void DeleteDep(int index)
        {
            for (int i = employees.Count - 1; i >= 0; i--)
            {
                if (employees[i].DepartmentId == index)
                {
                    employees.RemoveAt(i);
                }
            }
            
            for (int i = department.Count - 1; i >= 0; i--)
            {
                if (department[i].DepartmentId == index)
                {
                    department.RemoveAt(i);
                }
            }
        }

        //public void AddEmployee()
        //{
        //    Employee employee = new Employee();
        //    employees.Insert(0, employee);
        //    SelectedEmployee = employee;
        //}
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
