using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ElasticSearch.Models
{
    public class ElasticSearchInsertUpdateModel : ElasticSearchModel
    {
        public object Item { get; set; }
    }
}
