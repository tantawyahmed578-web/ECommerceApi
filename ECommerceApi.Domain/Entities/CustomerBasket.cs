using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;
        public List<BasketItem> Items { get; set; } = new();

        public CustomerBasket() { }
        public CustomerBasket(string id) { Id = id; }
    }
}
