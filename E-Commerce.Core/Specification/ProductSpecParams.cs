using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }


        private const int Maxpagesize = 10;
        private int pagesize = 5;
        public int Pagesize
        {
            get { return pagesize; }
            set { pagesize = value> Maxpagesize ? Maxpagesize : value; }
        }
        public int PageIndex { get; set; } = 1;

    }
}
