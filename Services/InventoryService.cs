using Inventory_Management.Models;
using InventoryManagement.DTO.Product;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management.Services
{
    public class InventoryService
    {

        private InventoryManagementContext _context;
        public InventoryService(InventoryManagementContext context)
        {
            _context = context;
        }

        //public List<Product> Products { get; set; }
        //private CategoryService _categoryService;

        //public InventoryService(CategoryService categoryService)
        //{
        //    Products = new List<Product>();
        //    _categoryService = categoryService;
        //}

        public string AddProduct(AddProductDto dto)
        {
            try
            {
                Product product = new Product
                {
                    ProductName = dto.ProductName,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    CategoryId = dto.CategoryId
                };

                _context.Products.Add(product);
                _context.SaveChanges();
                return $"Product with Product Id: {product.ProductId} added successfully";
            }
            catch(Exception ex)
            {
                return $"Error occured while addding product with error message:- {ex.ToString()}";
            }
            
        }

        public List<ProductDto> DisplayProducts()
        {

            var lstProducts = _context.Products.Select(p => new ProductDto
            {
                ProductName = p.ProductName,
                Price = p.Price,
                Quantity = p.Quantity,
                CategoryName = p.Category.CategoryName
            }).ToList();
            
            return lstProducts;
        }

        public string? RemoveProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            try
            {
                if(product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return $"Product with Product Id: {productId} removed successfully";
                }
                else
                {
                    return $"Product with Product Id: {productId} doesn't exist";
                }
                
            }
            catch(Exception ex)
            {
                return $"Error occured while removing product with message:- {ex.ToString()}";
            }
            
        }

        //Fetch Product by price
        public List<Product> FetchProductsByPrice(decimal price)
        {
            return _context.Products.Where(p => p.Price ==  price).ToList();
        }

        //Find by ProductId
        public ProductDto FindByProductId(int productId)
        {

            Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            ProductDto productDto;
            if (product != null)
            {
               productDto = new ProductDto
                {
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    CategoryName = product.Category.CategoryName
                };
            }
            else
            {
                productDto = new ProductDto();
            }
           
            return productDto;

        }

        //Sort by price
        public List<ProductDto> SortByPrice(string sorting_order = "asc")
        {
            List<ProductDto> sortedListDto;
            if (sorting_order.ToLower().Equals("asc"))
            {
                sortedListDto = _context.Products.OrderBy(p => p.Price).Select(p => new ProductDto
                {
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    CategoryName = p.Category.CategoryName
                }).ToList();
            }
            else if (sorting_order.ToLower().Equals("desc"))
            {
                sortedListDto = _context.Products.OrderByDescending(p => p.Price).Select(p => new ProductDto
                {
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    CategoryName = p.Category.CategoryName
                }).ToList();
            }
            else
            {
                sortedListDto = new List<ProductDto>();
            }
            return sortedListDto;

        }

        //Display only product names
        public List<string> DisplayProductNames()
        {
            List<string> productNameslst = _context.Products.Select(p => p.ProductName).ToList();
            return productNameslst;
            
        }


        //Reduce Qty by ProductId
        public void ReduceQty(int productId, int qty)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            try
            {
                if(product != null)
                {
                    product.Quantity -= qty;
                    _context.Products.Update(product);
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("No Product with provided exits.");
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error occured while reducing qty:- {ex.ToString()}");
            }

        }

        // Check stock availablity
        public decimal CheckStockAvailablity(int productId, int desiredQty)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null || product.Quantity < desiredQty)
            {
                return -1;
            }
            return product.Price;
        }

    }
}
