using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ADO.NET_Entity_TASK.DB_Class
{
    internal class PersonContext:DbContext
    {
        public DbSet<Person> Persons { get; set;}
        public PersonContext() : base("name=PersonContext")
        {
        }
    }
}
