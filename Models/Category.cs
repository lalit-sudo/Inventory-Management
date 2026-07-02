using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management.Models
{
    public class Category
    {
        private int _categoryId;
        private string? _categoryName;
        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (value > 0)
                {
                    _categoryId = value;
                }
                else
                {
                    throw new ArgumentException("Invalid Category Id");
                }
            }
        }
        public string? CategoryName
        {
            get { return _categoryName; }
            set 
            { 
                if(value != null && !value.Trim().Equals(""))
                {
                    _categoryName = value;
                }
                else
                {
                    throw new ArgumentException("Invalid Category Name");
                }
            }
        }


        public Category(int categoryId, string categoryName)
        {
           
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
        }
    }
}
