using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace EContactApp.econtactAppClasses
{
    class ContactClass
    {
        //Getter Setter Properties
        //Acts as data carrier in our App
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        /*static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;*/
        static string server = "localhost";
        static string database = "contactappdb";
        static string username = "root";
        static string password = "Kavita123#";
        static string myconnstrng = "server=" + server + ";DATABASE=" + database + ";UID=" + username + ";PASSWORD=" + password + ";";

        //Selecting data from database
        public DataTable Select()
        {
            // Step1: Database connection
            MySqlConnection conn = new MySqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // Step 2: Writing SQL Query
                string sql = "SELECT * FROM tbl_contact";
                //creating cmd using sql and conn
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //creating SQL DataAdapter using cmd
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        
        // Inserting the data into the database
        public Boolean Insert(ContactClass c)
        {
            // Creating a default return type and setting its value to false
            bool isSuccess = false;

            // Step 1: Connect database
            MySqlConnection conn = new MySqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // Step 2: Writing SQL Query to insert the data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //creating cmd using sql and conn
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //Creating parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                // Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();              
                // If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        // Method to update data in database from our application
        public bool Update(ContactClass c)
        {
            // creating a default return type and set its default value to false
            bool isSuccess = false;

            // Step 1: Connect database
            MySqlConnection conn = new MySqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // Step 2: Writing SQL Query to updat the data
                string sql = "Update tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                //creating cmd using sql and conn
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //Creating parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactId);
                // Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        // Method to delete data in database from our application
        public bool Delete(ContactClass c)
        {
            // creating a default return type and set its default value to false
            bool isSuccess = false;

            // Step 1: Connect database
            MySqlConnection conn = new MySqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // Step 2: Writing SQL Query to Delete the data
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
                //creating cmd using sql and conn
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //Creating parameters to add data
                cmd.Parameters.AddWithValue("@ContactId", c.ContactId);
                // Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Searching the data from database
        public DataTable Search(string keyword)
        {
            // Step1: Database connection
            MySqlConnection conn = new MySqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // Step 2: Writing SQL Query
                string sql = "SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword+"%'";

                //creating cmd using sql and conn
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //creating SQL DataAdapter using cmd
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

    }
}
