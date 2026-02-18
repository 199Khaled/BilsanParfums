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
    public partial class frmHauptForm : Form
    {
        public frmHauptForm()
        {
            InitializeComponent();
        }

        private void btmParfüm_Click(object sender, EventArgs e)
        {
            frmParfüms form = new frmParfüms();
           
           form.Show();
            
        }

        private void btnDuftÖle_Click(object sender, EventArgs e)
        {
            frmÖle form = new frmÖle();
           
            form.Show();
            
        }

        private void btnFlaschen_Click(object sender, EventArgs e)
        {
            frmFlakons form = new frmFlakons();
         
                form.Show();         
        }
    }
}
