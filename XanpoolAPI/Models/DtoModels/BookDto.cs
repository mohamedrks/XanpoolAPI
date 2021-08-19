using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XanpoolAPI.Models.DtoModels
{
    public class BookDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public short Year { get; set; }

        public string Description { get; set; }
    }
}
