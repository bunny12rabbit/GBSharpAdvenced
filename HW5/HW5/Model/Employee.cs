using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5.Model
{
    public class Employee : INotifyPropertyChanged
    {
        string name;
        string lastName;
        string secondName;
        string age;
        string birthDay;
        int birthDayYear;
        string department;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged(name);
            }
        }
        public string LastName
        {
            get { return LastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(lastName);
            }
        }
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged(secondName);
            }
        }
        public string Age
        {
            get
            {
                return  (DateTime.Today.Year - BirthDayYear).ToString();
            }
        }
        public string BirthDay
        {
            get { return birthDay; }
            set
            {
                birthDay = value;
                OnPropertyChanged(birthDay);
            }
        }
        public int BirthDayYear
        {
            get { return Int32.Parse(birthDay.Substring(birthDay.Length-4)); }
        }
        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged(department);
            }
        }

        public static List<Employee> GetEmployees()
        {
            var result = new List<Employee>()
            {
                new Employee
                {
                    Name = "Иван",
                    LastName = "Иванов",
                    SecondName = "Иванович",
                    BirthDay = "12.10.1989",
                    Department = "Отдел продаж"
                },
                new Employee
                {
                    Name = "Игорь",
                    LastName = "Кузнецов",
                    SecondName = "Викторович",
                    BirthDay = "21.07.1975",
                    Department = "Отдел сопровождения"
                },
                new Employee
                {
                    Name = "Андрей",
                    LastName = "Романенко",
                    SecondName = "Григорьевич",
                    BirthDay = "23.10.1992",
                    Department = "IT"
                }
            };
            return result;
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
