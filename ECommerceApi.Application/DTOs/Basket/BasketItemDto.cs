using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.DTOs.Basket
{
    public class BasketItemDto
    {
        [Required] public int Id { get; set; }
        [Required] public string ProductName { get; set; } = string.Empty;
        [Required, Range(0.1, double.MaxValue)] public decimal Price { get; set; }
        [Required, Range(1, int.MaxValue)] public int Quantity { get; set; }
        public string? PictureUrl { get; set; }
        public string? Category { get; set; }
    }
}
