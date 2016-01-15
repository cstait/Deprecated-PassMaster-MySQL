
using System.Windows.Forms;

namespace PassMaster
{
    public partial class frmEdit : Form
    {
        private WindowsFormsApplication1.frmMain caller;
        private string id;


        public frmEdit(WindowsFormsApplication1.frmMain frmMain, string id, string username, string password, string website)
        {
            InitializeComponent();
            caller = frmMain;
            this.id = id;
            txtUser.Text = username;
            txtPass.Text = password;
            txtWeb.Text = website;
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            //relays information to Database manager to insert text;

            string username = txtUser.Text;
            string password = txtPass.Text;
            string website = txtWeb.Text;

            PassMaster.DBManager.UpdateTable(id, username, password, website);
            //shows confirmation box to user
            MessageBox.Show("Entry Successful", "Confirmation");
            //refreshes datagrid
            caller.refreshDG();
            this.Close();

        }
    }
}
