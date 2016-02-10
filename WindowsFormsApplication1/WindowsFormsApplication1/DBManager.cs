using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

//class serves as the controller access to Database


namespace PassMaster
{
    static class DBManager
    {
        //connection information for application, uses passmaster database on localhost
        private static DataSet dsPassword;
        private static SQLiteDataAdapter daPassword;
        private static SQLiteCommandBuilder cb;
        
       
        private static string connStr = "Data Source=pmDB.sqlite;";
        private static SQLiteConnection conn;
        

        // checks to see if table exists, if it doesn't it creates the table
        public static void createTable()
        {
            
                if (System.IO.File.Exists("pmDB.sqlite")){
                    Console.WriteLine("Error file exists!");
                }
                else {
                    SQLiteConnection.CreateFile("pmDB.sqlite");
                }


                conn = new SQLiteConnection(connStr);
                conn.Open();
                string sql;
                SQLiteCommand cmd;
                //checks to see if table already exists, creates table otherwise
                sql = "CREATE TABLE if not exists Password" +
                            "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                            "Username VARCHAR(100), " +
                            "Password VARCHAR(100), " +
                            "Website  VARCHAR(100), " +
                            "Date    DATE);";
                //creates table based on sql string
                cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
           
        }

        public static void createDS()
        {
            try
            {
                //creates dataset which holds a copy of the table
                string sql = "SELECT Id, Username, Password, Website, Date from Password;";
                daPassword = new SQLiteDataAdapter(sql, conn);
                cb = new SQLiteCommandBuilder(daPassword);
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
                string sql = "INSERT INTO Password (Username, Password, Website, Date) VALUES (@Username, @Password, @Website, datetime());";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
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
                string sql = "DELETE FROM Password WHERE Id = @Id;";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
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


            string sql = "UPDATE Password SET Username = @Username, Password = @Password, Website = @Website, Date = datetime() WHERE Id = @Id;";
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
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
        public static SQLiteDataAdapter getDataAdapter()
        {
            return daPassword;
        }

    }
    }

