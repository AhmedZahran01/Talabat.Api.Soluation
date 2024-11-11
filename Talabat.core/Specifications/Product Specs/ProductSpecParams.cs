using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Specifications.Product_Specs
{
    public class ProductSpecParams
    {
        private const int MaxSiza = 10;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }



        private int pageSize = 5 ;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxSiza ? MaxSiza : value  ; }
        }

        public int PageIndex { get; set; } = 1;
    }
}
