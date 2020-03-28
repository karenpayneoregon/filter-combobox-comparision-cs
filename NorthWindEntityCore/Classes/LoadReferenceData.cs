using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                return context.Categories.OrderBy(cat => cat.CategoryName).ToList();
            }
        }

        public static List<Product> Products()
        {
            using (var context = new NorthContext())
            {
                return context.Products.OrderBy(prod => prod.ProductName).ToList();
            }
        }
    }
}
