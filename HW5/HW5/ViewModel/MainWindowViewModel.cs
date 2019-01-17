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
        List<Employee> employees;
        Employee selectedEmployee;
        public ObservableCollection<Employee> Employees { get; private set; }

        public MainWindowViewModel()
        {
            employees = Employee.GetEmployees();
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
