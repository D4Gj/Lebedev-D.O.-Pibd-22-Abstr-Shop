using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;


namespace PizzaAbstractShopView
{
    public partial class FormPizzaIngridient : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxIngridient.SelectedValue); }
            set { comboBoxIngridient.SelectedValue = value; }
        }
        public string IngridientName { get { return comboBoxIngridient.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }
        public FormPizzaIngridient(IIngridientLogic logic)
        {
            InitializeComponent();
            List<IngridientViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxIngridient.DisplayMember = "IngridientName";
                comboBoxIngridient.ValueMember = "Id";
                comboBoxIngridient.DataSource = list;
                comboBoxIngridient.SelectedItem = null;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngridient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
