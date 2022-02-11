using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching
{
    public class CacheSettings
    {
        IConfiguration configuration;

        public int SlidingExpiration { get; set; }

        public CacheSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
            SlidingExpiration = configuration.GetValue<int>("CacheSettings:SlidingExpiration");
        }
    }
}
