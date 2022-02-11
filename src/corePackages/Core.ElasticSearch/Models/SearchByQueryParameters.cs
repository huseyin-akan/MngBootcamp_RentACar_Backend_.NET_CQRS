using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ElasticSearch.Models
{
    public class SearchByQueryParameters : SearchParameters
    {
        public string QueryName { get; set; }
        public string Query { get; set; }
        public string[] Fields { get; set; }
    }
}
