using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5.Model
{
    class Department : INotifyPropertyChanged
    {
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
