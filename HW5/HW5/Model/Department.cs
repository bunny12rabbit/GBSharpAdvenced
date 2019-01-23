using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5.Model
{
    public class Department : INotifyPropertyChanged
    {
        int departmentId;
        string departmentName;
        public string DepartmentName
        {
            get { return departmentName; }
            set
            {
                departmentName = value;
                OnPropertyChanged(departmentName);
            }
        }
        public int DepartmentId { get; set; }

        public Department(string Name, int Id)
        {
            DepartmentName = Name;
            DepartmentId = Id;
        }

        //public static ObservableCollection<Department> GetDepartments()
        //{
        //    var result = new ObservableCollection<Department>()
        //    {
        //        new Department {DepartmentName = "IT" },
        //        new Department {DepartmentName = "Отдел продаж"}
        //    };
        //    return result;
        //}

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
