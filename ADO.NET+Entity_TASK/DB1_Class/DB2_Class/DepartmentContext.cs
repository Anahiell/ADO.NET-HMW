using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Entity_TASK.DB1_Class.DB2_Class
{
    internal class DepartmentContext:DbContext
    {
        public DbSet<Department> departmentSet {  get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DepartmentContext():base("name=DepartmentContext")
        {

        }
    }
}
