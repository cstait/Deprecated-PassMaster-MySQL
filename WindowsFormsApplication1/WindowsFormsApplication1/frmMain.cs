using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
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
            temp.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //cycles through each selected row, deleting based on the id tag
            foreach (DataGridViewRow row in dgPassword.SelectedRows)
            {
                string id = row.Cells[0].Value.ToString();
                Console.WriteLine(id);
                DBManager.DeleteId(id);
            }
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
            MySqlDataAdapter temp = PassMaster.DBManager.getDataAdapter();
            temp.Fill(table);
            BindingSource source = new BindingSource();
            source.DataSource = table;
            dgPassword.DataSource = source;
        }
    }
    }


