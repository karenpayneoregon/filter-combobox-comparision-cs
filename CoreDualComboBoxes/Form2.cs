using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            SetProductFilter(((Category)CategoryComboBox.SelectedItem).CategoryId);
            ProductComboBox.DataSource = _productsBindingListFilter;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            var categories = LoadReferenceData.Categories();
            CategoryComboBox.DataSource = categories;

            _productsBindingList = new BindingList<Product>(LoadReferenceData.Products());
            // ReSharper disable once PossibleInvalidOperationException
            SetProductFilter((int)categories.FirstOrDefault()?.CategoryId);
            ProductComboBox.DataSource = _productsBindingListFilter;
        }

        private void SetProductFilter(int categoryIdentifier)
        {
            _productsBindingListFilter = new BindingList<Product>(
                _productsBindingList.Where(product => product.CategoryId == categoryIdentifier)
                    .ToList());
        }
    }
}
