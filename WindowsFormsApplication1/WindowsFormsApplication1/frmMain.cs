using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace WindowsFormsApplication1
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAdd temp = new frmAdd(this);
            temp.Visible = true;

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //binds the datagrid view to the dataset

            PassMaster.DBManager.createTable();
            //refreshes the datagrid to show newest information
            this.refreshDG();

            
            //dgPassword.DataMember = "password";
            
        }

        //rebinds data source to table
        public void refreshDG ()
        {
            PassMaster.DBManager.createDS();
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


