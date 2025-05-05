using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Products
{
  public  class ProductQueryPrams
    {
  
                                                                                                
        
        public int? brandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions productSortingOptions { get; set; }
        public string? Search { get; set; }

       
        private const int _DefaultPageSize = 5;
        private const int _MaxPageSize = 5;

        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value>0&& value < _MaxPageSize? value:_DefaultPageSize; }
        }


        public int PageIndex { get; set; } = 1;

    }
}
