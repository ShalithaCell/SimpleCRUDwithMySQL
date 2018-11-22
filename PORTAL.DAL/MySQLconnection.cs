using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.DAL
{
    public class MySQLconnection
    {
        public MySqlConnection connection;

        public MySqlConnection CreateConnection() //create mysql connection
        {
            string connectionString = ConfigurationManager.AppSettings["AppConnection"];
            connection = new MySqlConnection(connectionString);

            return connection;
        }

        private bool OpenConnection() //connect open
        {
            try
            {
                connection = CreateConnection();
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        private bool CloseConnection() //conne ction close
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Executing(string query) //execute sql query
        {

            try
            {
                if (OpenConnection())
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet LoadData(string Query) //get data using sql query
        {
            try
            {
                DataSet ds = new DataSet();
                if (OpenConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(Query, connection);
                    cmd.CommandType = CommandType.Text;

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }

                    CloseConnection();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExucteSP(Dictionary<string, string> Params, string spName) // execute sql stored procedure 
        {
            try
            {
                DataSet ds = new DataSet();
                if (OpenConnection()) //open connection
                {
                    using (MySqlCommand cmd = new MySqlCommand(spName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (KeyValuePair<string, string> item in Params)
                        {
                            //Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
                            cmd.Parameters.AddWithValue("@" + item.Key, item.Value);


                        }
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(ds);
                        }
                    }
                    CloseConnection(); //close connection
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
