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
using PizzaShopBusinessLogic.BusinessLogic;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.BindingModels;

namespace PizzaAbstractShopView
{
    public partial class FormStorageIngridients : Form
    {
        private readonly MainLogic mainLogic;
        private readonly IStorageLogic storageLogic;
        private readonly IIngridientLogic ingridientLogic;
        private List<StorageViewModel> storageViews;
        private List<IngridientViewModel> ingridientViews;

        public FormStorageIngridients(MainLogic mainLogic, IStorageLogic storageLogic, IIngridientLogic ingridientLogic)
        {
            InitializeComponent();
            this.mainLogic = mainLogic;
            this.storageLogic = storageLogic;
            this.ingridientLogic = ingridientLogic;
            LoadData();
        }

        private void LoadData()
        {
            storageViews = storageLogic.Read(null);
            if (storageViews != null)
            {
                comboBoxStorages.DataSource = storageViews;
                comboBoxStorages.DisplayMember = "StorageName";
            }
            ingridientViews = ingridientLogic.Read(null);
            if (ingridientViews != null)
            {
                comboBoxComponent.DataSource = ingridientViews;
                comboBoxComponent.DisplayMember = "MaterialName";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxCount.Text == string.Empty)
                throw new Exception("Введите количество материала");

            mainLogic.AddMaterials(comboBoxStorages.SelectedItem as StorageViewModel, Convert.ToInt32(textBoxCount.Text),
                comboBoxComponent.SelectedItem as IngridientViewModel);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
