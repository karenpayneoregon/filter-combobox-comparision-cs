using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NorthWindEntityCore.Classes;
using NorthWindEntityCore.Models;

namespace CoreDualComboBoxes
{
    public partial class Form2 : Form
    {
        private BindingList<Product> _productsBindingList = new BindingList<Product>();
        private BindingList<Product> _productsBindingListFilter = new BindingList<Product>();
        public Form2()
        {
            InitializeComponent();
            Shown += Form2_Shown;
            CategoryComboBox.SelectedIndexChanged += CategoryComboBox_SelectedIndexChanged;
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetProductFilter(((CategoryItem)CategoryComboBox.SelectedItem).CategoryId);
            ProductComboBox.DataSource = _productsBindingListFilter;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            List<CategoryItem> categories = LoadReferenceData.CategoryItems();
            CategoryComboBox.DataSource = categories;

            _productsBindingList = new BindingList<Product>(LoadReferenceData.Products());
            SetProductFilter((int)categories.FirstOrDefault()?.CategoryId);
            ProductComboBox.DataSource = _productsBindingListFilter;
        }
        private void SetProductFilter(int categoryIdentifier)
        {
            _productsBindingListFilter = new BindingList<Product>(_productsBindingList.Where(product => product.CategoryId == categoryIdentifier).ToList());
        }
    }
}
