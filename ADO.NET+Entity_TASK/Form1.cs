using ADO.NET_Entity_TASK.DB_Class;
using ADO.NET_Entity_TASK.DB1_Class.DB2_Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_Entity_TASK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using (var context = new PersonContext())
            {
                var people = context.Persons.ToList();
                dataGridView1.DataSource = people;
            }

            using (var context = new DepartmentContext())
            {
                var departments = context.departmentSet.ToList();
                dataGridView2.DataSource = departments;
            }

        }
        public void some()
        {
            //    var peopleOver25 = context.People.Where(p => p.Age > 25).ToList();

            //    // Запрос 2: Выбрать людей, не проживающих в Киеве.
            //    var peopleNotInKiev = context.People.Where(p => p.City != "Киев").ToList();

            //    // Запрос 3: Выбрать имена людей, проживающих в Киеве.
            //    var namesInKiev = context.People.Where(p => p.City == "Киев").Select(p => p.Name).ToList();

            //    // Запрос 4: Выбрать людей старших 35 лет с именем Sergey.
            //    var peopleSergeyOver35 = context.People.Where(p => p.Name == "Sergey" && p.Age > 35).ToList();

            //    // Запрос 5: Выбрать людей, проживающих в Кривом Роге.
            //    var peopleInKrivoyRog = context.People.Where(p => p.City == "Кривой Рог").ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var con = new PersonContext())
            {
                var peopleOver25 = con.Persons.Where(p => p.Age > 25).ToList();
                dataGridView1.DataSource = peopleOver25;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var con = new PersonContext())
            {
                var peopleNotInKiev = con.Persons.Where(p => p.City != "Киев").ToList();
                dataGridView1.DataSource = peopleNotInKiev;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var con = new PersonContext())
            {
                var namesInKiev = con.Persons.Where(p => p.City == "Kyiv").ToList();
                dataGridView1.DataSource = namesInKiev;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var con = new PersonContext())
            {
                var peopleSergeyOver35 = con.Persons.Where(p => p.Name == "Sergey" && p.Age > 35).ToList();
                dataGridView1.DataSource = peopleSergeyOver35;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var con = new PersonContext())
            {
                var peopleInKrivoyRog = con.Persons.Where(p => p.City == "Kryvyi Rih").ToList();
                dataGridView1.DataSource = peopleInKrivoyRog;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (var context = new DepartmentContext())
            {
                var employeesInUkraineNotInDonetsk = context.departmentSet
                    .Where(d => d.Country == "Ukraine" && d.City != "Donetsk")
                    .Join(context.Employees, d => d.Id, w => w.DepId, (d, w) => new
                    {
                        EmployeeName = w.FirstName + " " + w.LastName
                    }).ToList();

                dataGridView2.DataSource = employeesInUkraineNotInDonetsk;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (var context = new DepartmentContext())
            {
                var distinctCountries = context.departmentSet.Select(d => d.Country).Distinct().ToList();

                // Создаем новый DataTable
                DataTable dataTable = new DataTable();

                // Добавляем столбец "Country" в DataTable
                dataTable.Columns.Add("Country", typeof(string));

                // Заполняем DataTable данными
                foreach (var country in distinctCountries)
                {
                    dataTable.Rows.Add(country);
                }

                // Устанавливаем DataGridView.DataSource в DataTable
                dataGridView2.DataSource = dataTable;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (var context = new DepartmentContext())
            {
                var employeesOver25 = context.Employees
                    .Where(w => w.Age > 25)
                    .Take(3)
                    .Select(w => new
                    {
                        EmployeeName = w.FirstName + " " + w.LastName,
                        w.Age
                    }).ToList();

                dataGridView2.DataSource = employeesOver25;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var context = new DepartmentContext())
            {
                var employeesInKievOver23 = context.departmentSet
                    .Where(d => d.City == "Kyiv")
                    .Join(context.Employees, d => d.Id, w => w.DepId, (d, emp) => new
                    {
                        emp.FirstName,
                        emp.LastName,
                        emp.Age
                    })
                    .Where(w => w.Age > 23).ToList();

                dataGridView2.DataSource = employeesInKievOver23;
            }
        }
    }
}
