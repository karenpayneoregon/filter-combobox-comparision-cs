using System.Collections.Generic;
using System.Linq;
using NorthWindEntityCore.Contexts;
using NorthWindEntityCore.Models;

namespace NorthWindEntityCore.Classes
{
    public class LoadReferenceData
    {
        public static List<Category> Categories()
        {
            using (var context = new NorthContext()) 
            {
                return context.Categories.OrderBy(category => category.CategoryName).ToList();
            }
        }
        /// <summary>
        /// Load only properties to load a ComboBox
        /// </summary>
        /// <returns></returns>
        public static List<CategoryItem> CategoryItems()
        {
            using (var context = new NorthContext())
            {
                return context.Categories.Select(category => new CategoryItem()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                }) .OrderBy(category => category.CategoryName).ToList();
            }

        }
        /// <summary>
        /// Load all products
        /// </summary>
        /// <returns></returns>
        public static List<Product> Products()
        {
            using (var context = new NorthContext())
            {
                return context.Products.OrderBy(product => product.ProductName).ToList();
            }
        }
    }
}
