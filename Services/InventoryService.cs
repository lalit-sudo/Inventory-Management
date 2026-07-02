using Inventory_Management.Models;
using InventoryManagement.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management.Services
{
    public class InventoryService
    {
        public List<Product> Products { get; set; }
        private CategoryService _categoryService;

        public InventoryService(CategoryService categoryService)
        {
            Products = new List<Product>();
            _categoryService = categoryService;
        }

        public string AddProduct(Product product)
        {
            foreach(Product prod in Products)
            {
                if(prod.ProductId == product.ProductId)
                {
                    return $"Product with product id: {product.ProductId} already exists.";
                }
            }
            
            Category? category = _categoryService.FindCategoryById(product.CategoryId);
            if(category == null)
            {
                throw new Exception($"No category with category id: {product.CategoryId} exists");
            }
            else
            {
                Products.Add(product);
                return $"Product added successfully with id: {product.ProductId}";
            }
        }

        public List<Product> DisplayProducts()
        {
            List<Product> lstProducts = new List<Product>();
            foreach(Product product in Products)
            {
                lstProducts.Add(product);
            }
            return lstProducts;
        }

        public string? RemoveProduct(int productId)
        {
            for(int i=0; i< Products.Count; i++)
            {
                if (Products[i].ProductId == productId)
                {
                    Products.Remove(Products[i]);
                    return $"Product of product id: {productId} has been removed successfully";
                }
            }
            return default;
        }

        //Fetch Product by price
        public List<Product> FetchProductsByPrice(decimal price)
        {
            return Products.Where(p => p.Price == price).ToList();
        }

        //Find by ProductId
        public Product? FindByProductId(int productId)
        {
            return Products.FirstOrDefault(p => p.ProductId == productId);
        }

        //Sort by price
        public List<Product> SortByPrice(string sorting_order = "asc")
        {
            var sortedList = new List<Product>();
            if(sorting_order == null)
            {
                Console.WriteLine("sorting order cannot be null");
            }
            else if (sorting_order.ToLower().Equals("asc"))
            {
                sortedList =  Products.OrderBy(p => p.Price).ToList();
            }
            else if (sorting_order.ToLower().Equals("desc"))
            {
                sortedList =  Products.OrderByDescending(p => p.Price).ToList();
            }
            else
            {
                Console.WriteLine("Please specify valid sorting order: \"asc\" for ascending order and \"desc\" for descending order.");
            }
            return sortedList;
        }

        //Display only product names
        public List<string> DisplayProductNames()
        {
            return Products.Select(p => p.ProductName).ToList();
        }


        //Reduce Qty by ProductId
        public void ReduceQty(int productId, int qty)
        {
            Product? product = FindByProductId(productId);
            if(product != null)
            {
                product.Quantity -= qty;
            }

        }

        // Check stock availablity
        public decimal CheckStockAvailablity(int productId, int desiredQty)
        {
            Product? product = FindByProductId(productId);
            if(product == null || product.Quantity < desiredQty)
            {
                return -1;
            }
            return product.Price;
        }

    }
}
