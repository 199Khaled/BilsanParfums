using BilsanDb_BusinessLayer;
using Busnisse_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilsanParfums
{
    public partial class frmFlakons : Form
    {
        enum enMode { addnew = 0, update = 1}
        enMode _mode;
        private readonly object _dataloadLock = new object();

        DataTable _dtFlakons;
        clsFlakons _flakons;
        BindingSource _bindingSource;
        public frmFlakons()
        {
            InitializeComponent();
        }

        private void frmFlakons_Load(object sender, EventArgs e)
        {
            _LadeFlakonsdatenFromDatabase();
        }
        private void _LadeFlakonsdatenFromDatabase()
        {
            lock (_dataloadLock)
            {
                _dtFlakons = clsFlakons.GetAllFlakons();
                if (_dtFlakons != null && _dtFlakons.Rows.Count > 0)
                {
                    _bindingSource.DataSource = _dtFlakons;
                    dgvFlakons.DataSource = _bindingSource;
                }
            }

        }
        private void _ResetDefaultValues()
        {
            cbFlakonsMengeInMl.SelectedIndex = -1;
            cbForm.SelectedIndex = -1;
            cbVerschlussart.SelectedIndex = -1;
            cbFarbe.SelectedIndex = -1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _ResetDefaultValues();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        private void _FlakonsDatenSpeichern()
        {

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void txtKarfonLager_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void txtFlakonsProkarton_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }
    }
}
