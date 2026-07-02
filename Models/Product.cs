using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management.Models
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; set; }
        public int CategoryId { get; private set; }  //Foreign Key -> stores relationship identifier
        //public Category? Category{ get; private set; }  //Navigation Property - Product has relationship with Category -> stores related object reference




        public Product(int productId, string productName, decimal price, int quantity, int categoryId)
        {
            if(productId <= 0 || productName == null || productName.Trim().Equals("") || price <= 0 || quantity < 0 || categoryId <=0)
            {
                throw new ArgumentException("Invalid values entered");
            }

            this.ProductId = productId;
            this.ProductName = productName;
            this.Price = price;
            this.Quantity = quantity;
            this.CategoryId = categoryId;
        }

        

    }
}
