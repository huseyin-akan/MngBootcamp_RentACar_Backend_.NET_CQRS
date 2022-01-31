using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Fuel :Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Model> Models { get; set; }

        public Fuel()
        {
            Models = new HashSet<Model>();
        }

        public Fuel(string name, int id) : this()
        {
            Name = name;
            Id = id;
        }
    }
}
