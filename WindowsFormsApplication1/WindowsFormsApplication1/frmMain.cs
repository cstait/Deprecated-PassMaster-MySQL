using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using PassMaster;

namespace WindowsFormsApplication1
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //binds the datagrid view to the dataset

            DBManager.createTable();
            //refreshes the datagrid to show newest information
            this.refreshDG();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAdd temp = new frmAdd(this);
            temp.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //cycles through each selected row, deleting based on the id tag
            int selectedrowindex = dgPassword.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgPassword.Rows[selectedrowindex];
            string id = selectedRow.Cells[0].Value.ToString();
            Console.WriteLine(id);
            DBManager.DeleteId(id);
            
            //refreshes datagrid view
            this.refreshDG();
        }


        //rebinds data source to table
        public void refreshDG()
        {
            //creates dataset
            DBManager.createDS();
            dgPassword.AutoGenerateColumns = true;
            DataTable table = new DataTable();
            SQLiteDataAdapter temp = DBManager.getDataAdapter();
            temp.Fill(table);
            BindingSource source = new BindingSource();
            source.DataSource = table;
            dgPassword.DataSource = source;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dgPassword.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgPassword.Rows[selectedrowindex];
            string id = selectedRow.Cells[0].Value.ToString();
            string username = selectedRow.Cells[1].Value.ToString();
            string password = selectedRow.Cells[2].Value.ToString();
            string website = selectedRow.Cells[3].Value.ToString();

            frmEdit temp = new frmEdit(this, id, username, password, website);
            temp.Show();
        }
    }
    }


