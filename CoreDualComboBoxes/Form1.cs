using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using NorthWindEntityCore.Contexts;
using NorthWindEntityCore.Models;

namespace CoreDualComboBoxes
{
    public partial class Form1 : Form
    {
        private readonly NorthContext _context = new NorthContext();
        private BindingList<Product> _productsBindingList = new BindingList<Product>();
        private BindingList<Product> _productsBindingListFilter = new BindingList<Product>();
        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
            CategoryComboBox.SelectedIndexChanged += CategoryComboBox_SelectedIndexChanged;
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            var categoryIdentifier = ((Category)CategoryComboBox.SelectedItem).CategoryId;
            _productsBindingListFilter = new BindingList<Product>(
                _productsBindingList.Where(product => product.CategoryId == categoryIdentifier).ToList());

            ProductComboBox.DataSource = _productsBindingListFilter;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            /*
             * Get all categories, inform the DbContext not to track
             * changes as this is purely for show not add, edit, delete
             * operations
             */
            var categories = _context.Categories.AsNoTracking()
                .OrderBy(cat => cat.CategoryName).ToList();

            CategoryComboBox.DataSource = categories;


            /*
             * Get all products, inform the DbContext not to track
             * changes as this is purely for show not add, edit, delete
             * operations, only select via a filter on CategoryComboBox
             * selection.
             */

            _productsBindingList = new BindingList<Product>(_context.Products.AsNoTracking()
                .OrderBy(prod => prod.ProductName).ToList());

            _productsBindingListFilter = new BindingList<Product>(
                _productsBindingList.Where(product => 
                    product.CategoryId == categories.FirstOrDefault()?.CategoryId).ToList());

            ProductComboBox.DataSource = _productsBindingListFilter;

        }
        /// <summary>
        /// Show current selections
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionButton_Click(object sender, EventArgs e)
        {
            var currentCategory = ((Category) CategoryComboBox.SelectedItem);
            var currentProduct = (Product)ProductComboBox.SelectedItem;

            if (currentProduct.UnitPrice != null)
            {
                SelectionTextBox.Text =
                    $"Category: {currentCategory.CategoryId},{currentCategory.CategoryName}   " +
                    $"Product: {currentProduct.ProductId}, {currentProduct.ProductName} at {currentProduct.UnitPrice.Value:C}";
            }
            else
            {
                SelectionTextBox.Text =
                    $"Category: {currentCategory.CategoryId},{currentCategory.CategoryName}   " +
                    $"Product: {currentProduct.ProductId}, {currentProduct.ProductName}";
            }
        }
    }
}
