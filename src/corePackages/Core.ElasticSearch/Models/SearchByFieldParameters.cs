using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ElasticSearch.Models
{
    public class SearchByFieldParameters : SearchParameters
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}
