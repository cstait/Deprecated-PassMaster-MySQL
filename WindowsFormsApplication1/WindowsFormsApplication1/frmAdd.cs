using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmAdd : Form
    {
        private frmMain caller;

        public frmAdd(frmMain mainForm)
        {
            InitializeComponent();
            caller = mainForm;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //relays information to Database manager to insert text;
            PassMaster.DBManager.InsertIntoTable(txtUser.Text, txtPass.Text, txtWeb.Text);
            //resets textboxes to empty strings
            txtUser.Text = "";
            txtPass.Text = "";
            txtWeb.Text = "";
            //shows confirmation box to user
            MessageBox.Show("Entry Successful", "Confirmation");
            //refreshes datagrid
            caller.refreshDG();
            

        }

        private void frmAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
