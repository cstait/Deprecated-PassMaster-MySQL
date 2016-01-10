using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PassMaster
{
    static class DBManager
    {
        private static string connStr = "server=localhost;user=root;database=passmaster;port=3306;password=root;";
        private static MySqlConnection conn = new MySqlConnection(connStr);

        public static void createTable()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql;
                MySqlCommand cmd;
                //checks to see if 
                /* string sql = "SELECT * from Password;";

                 MySqlDataReader rdr = cmd.ExecuteReader();
                 */
                //  if (!rdr.HasRows)
                //  {
                sql = "CREATE TABLE Password" +
                          "(Id int NOT NULL, " +
                           "Username VARCHAR(100), " +
                           "Password VARCHAR(100), " +
                           "Website  VARCHAR(100), " +
                           "Date    DATE, " +
                           "PRIMARY KEY (id))";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // }

                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

    }

    }

