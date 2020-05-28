using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Infrastructure.MongoDb
{
    public class MongoDbOptions
    {
        public MongoDbOptions()
        {
        }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
