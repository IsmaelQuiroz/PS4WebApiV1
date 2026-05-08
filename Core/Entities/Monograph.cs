using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Monograph: BaseClass
    {
        public string Title {  get; set; }
        public int Code { get; set; }
        public int Stock { get; set; }
        public string? Keyword { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
