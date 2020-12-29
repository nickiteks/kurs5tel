using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class ModelSave
    {
        public User User { get; set; }
        public List<Person> Persons { get; set; }
        public List<List<ItemPerson>> InventoryPersons { get; set; }
        public List<ItemUser> InventoryUsers { get; set; }
    }
}
