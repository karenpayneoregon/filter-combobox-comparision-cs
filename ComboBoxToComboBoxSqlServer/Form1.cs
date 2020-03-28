using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComboBoxToComboBoxSqlServer
{
    public partial class Form1 : Form
    {
        readonly BindingSource _productBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            CategoryComboBox.SelectedIndexChanged += CategoryComboBox_SelectedIndexChanged;
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedItem == null) return;

            var categoryIdentifier = ((DataRowView)CategoryComboBox.SelectedItem).Row.Field<int>("CategoryId");

            _productBindingSource.Filter = $"CategoryId = {categoryIdentifier}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ops = new DataOperations();
            CategoryComboBox.DisplayMember = "CategoryName";

            CategoryComboBox.DataSource = ops.CategoryDataTable();

            ProductComboBox.DisplayMember = "ProductName";

            _productBindingSource.DataSource = ops.ProductDataTable();
            ProductComboBox.DataSource = _productBindingSource;
        }
    }
}
