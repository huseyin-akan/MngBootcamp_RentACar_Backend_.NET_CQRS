using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transmission :Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Model> Models { get; set; }

        public Transmission()
        {
            Models = new HashSet<Model>();
        }

        public Transmission(int id, string name) : this()
        {
            Name = name;
            Id = id;
        }
    }
}
