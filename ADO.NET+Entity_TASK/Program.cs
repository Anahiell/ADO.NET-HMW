using ADO.NET_Entity_TASK.DB_Class;
using ADO.NET_Entity_TASK.DB1_Class.DB2_Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_Entity_TASK
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Fill_BD();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void Fill_BD()
        {
            using (var context = new PersonContext())
            {
                //парсим данные
                var persons = new List<Person>()
                {
            new Person() { Name = "Andrey", Age = 24, City = "Kyiv" },
            new Person() { Name = "Liza", Age = 18, City = "Kryvyi Rih" },
            new Person() { Name = "Oleg", Age = 15, City = "London" },
            new Person() { Name = "Sergey", Age = 55, City = "Kyiv" },
            new Person() { Name = "Sergey", Age = 32, City = "Kyiv" }
                };
                foreach (var person in persons)
                {
                    //если такие данные есть, то не заполняем
                    if (!context.Persons.Any(p => p.Name == person.Name && p.Age == person.Age && p.City == person.City))
                    {
                        context.Persons.Add(person);
                    }
                }
                context.SaveChanges();
            }
            using (var context = new DepartmentContext())
            {
                List<Department> departments = new List<Department>()
                {
                    new Department(){ Id = 1, Country = "Ukraine", City = "Donetsk" },
                    new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                    new Department(){ Id = 3, Country = "France", City = "Paris" },
                    new Department(){ Id = 4, Country = "UK", City = "London" }
                };
                foreach (var department in departments)
                {
                    if (!context.departmentSet.Any(o => o.Id == department.Id && o.Country == department.Country && o.City == department.City))
                    {
                        context.departmentSet.Add(department);
                    }
                }
                context.SaveChanges();
                List<Employee> employees = new List<Employee>()
                {
                    new Employee(){ Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age =
                       22, DepId = 2 },
                    new Employee(){ Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33,
                       DepId = 1 },
                    new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age =
                       43, DepId = 3 },
                    new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22,
                       DepId = 2 },
                    new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36,
                       DepId = 4 },
                    new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22,
                       DepId = 2 },
                    new Employee() { Id = 7, FirstName = "Nikita", LastName = " Krotov ", Age =
                        27,DepId = 4 }
                };
                foreach (var emp in employees)
                {
                    if (!context.Employees.Any(e => e.Id == emp.Id))
                    {
                        context.Employees.Add(emp);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
