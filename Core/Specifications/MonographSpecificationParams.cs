using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class MonographSpecificationParams
    {
        public int? CategoryId { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        public int MaxPageSize = 15;
        private int _pageSize = 4;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? Keyword { get; set; }
        public string? Search { get; set; }

    }
}
