using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.DTOs.pagination_and_filtering
{
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new(); 
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize); // Calculate total pages based on total count and page size
    }
}
