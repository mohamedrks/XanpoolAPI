using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XanpoolAPI.Utilities
{
    public interface IBookCatalogDbSettings
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
