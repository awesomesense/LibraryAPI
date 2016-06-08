using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models
{
    public class AppMongoSettings
    {
        public string MongoConnection { get; set; }

        public string Database { get; set; }
    }
}
