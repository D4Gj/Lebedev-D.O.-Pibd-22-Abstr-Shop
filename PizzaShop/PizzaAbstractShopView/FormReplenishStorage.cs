using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.BusinessLogic;
using Unity;


namespace PizzaAbstractShopView
{
    public partial class FormReplenishStorage : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly MainLogic logic;
        private readonly IIngridientLogic ingridientLogic;
        private readonly IStorageLogic storageLogic;
        public FormReplenishStorage(MainLogic logic, IIngridientLogic ingridientLogic, IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.ingridientLogic = ingridientLogic;
            this.storageLogic = storageLogic;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxIngridient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.logic.ReplenishStorage(new StorageIngridientBindingModel
                {
                    Id = 0,
                    StorageId = Convert.ToInt32(comboBoxStorage.SelectedValue),
                    IngridientId = Convert.ToInt32(comboBoxIngridient.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormReplenishStorage_Load(object sender, EventArgs e)
        {
            try
            {
                var storageList = storageLogic.GetList();
                comboBoxStorage.DisplayMember = "StorageName";
                comboBoxStorage.ValueMember = "Id";
                comboBoxStorage.DataSource = storageList;
                comboBoxStorage.SelectedItem = null;
                var ingridientList = ingridientLogic.Read(null);
                comboBoxIngridient.DataSource = ingridientList;
                comboBoxIngridient.DisplayMember = "IngridientName";
                comboBoxIngridient.ValueMember = "Id";
                comboBoxIngridient.SelectedItem = null;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
    }
}
