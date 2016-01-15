using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

//class serves as the controller access to Database


namespace PassMaster
{
    static class DBManager
    {
        //connection information for application, uses passmaster database on localhost
        private static string connStr = "server=localhost;user=root;database=passmaster;port=3306;password=root;";
        private static MySqlConnection conn = new MySqlConnection(connStr);
        private static DataSet dsPassword;
        private static MySqlDataAdapter daPassword;
        private static MySqlCommandBuilder cb;

        // checks to see if table exists, if it doesn't it creates the table
        public static void createTable()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql;
                MySqlCommand cmd;
                //checks to see if table already exists,
                sql = "SHOW TABLES LIKE 'Password';";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                conn.Close();

                if (!rdr.HasRows)
                {
                    conn.Open();
                    sql = "CREATE TABLE Password" +
                              "(Id int NOT NULL AUTO_INCREMENT, " +
                               "Username VARCHAR(100), " +
                               "Password VARCHAR(100), " +
                               "Website  VARCHAR(100), " +
                               "Date    DATE, " +
                               "PRIMARY KEY (id))";
                    //creates table based on sql string
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                }

                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        public static void createDS()
        {
            try
            {
                //creates dataset which holds a copy of the table
                string sql = "SELECT Id, Username, Password, Website, Date from password;";
                daPassword = new MySqlDataAdapter(sql, conn);
                cb = new MySqlCommandBuilder(daPassword);
                dsPassword = new DataSet();
                daPassword.Fill(dsPassword, "password");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //inserts text values from addform into sql database
        public static void InsertIntoTable(string Username, string Password, string Website)
        {
            try
            {
                //insert sql command string with injection
                string sql = "INSERT INTO password (Username, Password, Website, Date) VALUES (@Username, @Password, @Website, Now());";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //adds information from textboxes to placeholders 
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Website", Website);
                //executes the command
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }

        public static void DeleteId(string id)
        {
            try
            {
                string sql = "DELETE FROM password WHERE Id = @Id;";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //adds information from id to placeholders 
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }

        //updates table based on users information
        public static void UpdateTable(string id, string username, string password, string website)
        {
        try
            {


            string sql = "UPDATE password SET Username = @Username, Password = @Password, Website = @Website, Date = Now() WHERE Id = @Id;";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            //adds information from id to placeholders
            cmd.Parameters.AddWithValue("@Username", username); 
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Website", website);
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }

        //returns dataset
        public static DataSet getDataSet()
        {
            return dsPassword;
        }

        //returns datadaptor
        public static MySqlDataAdapter getDataAdapter()
        {
            return daPassword;
        }

    }
    }

