using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Brand : Entity
    {
        public string Name { get; set; }

        //virtual keywordü özellikle ORMler için önemli bir keywordtür. Eager/Lazy Loading için çeşitli 
        //anlamları vardır.
        public virtual ICollection<Model> Models { get; set; }

        public Brand(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public Brand()
        {
            Models = new HashSet<Model>();
        }
    }
}
