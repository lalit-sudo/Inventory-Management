using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management.Models
{
    public class Product
    {
        public int ProductId { get;  set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }  //Foreign Key -> stores relationship identifier
        public Category Category { get; set; } = null!;//Reference Navigation Property - Product has relationship with Category -> stores related object 
        public ICollection<OrderItem> OrderItems{ get; set; } = new List<OrderItem>();

    }
}
