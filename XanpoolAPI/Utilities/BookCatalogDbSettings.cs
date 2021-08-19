using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XanpoolAPI.Utilities
{
    public class BookCatalogDbSettings : IBookCatalogDbSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
