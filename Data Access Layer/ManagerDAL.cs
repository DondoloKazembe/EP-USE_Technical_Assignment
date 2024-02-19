using EPI_USE_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EPI_USE_Web_Application.Data_Access_Layer
{
    public class ManagerDAL
    {
        // Connection string to Azure database
        string ConnectionStringProd = "Server=tcp:epiuse-server.database.windows.net,1433;Initial Catalog=EPI_USE_DB;Persist Security Info=False;User ID=epiuser;Password=Password_123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        // Get all Managers information 
        public IEnumerable<ManagerModel> GetAllManagers()
        {
            List<ManagerModel> ManList = new List<ManagerModel>(); // Creation of list with Manager data type 

            using (SqlConnection Con = new SqlConnection(ConnectionStringProd))  // This is the connection between the database and the application 
            {
                SqlCommand cmd = new SqlCommand("GetAllManagers", Con); // Getting/Calling the stored procedure 
                cmd.CommandType = CommandType.StoredProcedure;
                Con.Open(); // Opening the connection to allow the procedure to be used

                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read()) // Reading of the data
                {
                    ManagerModel Man2 = new ManagerModel();
                    Man2.ManagerNumber = DR["ManagerNumber"].ToString();
                    Man2.ManagerName = DR["ManagerName"].ToString();
                    Man2.ManagerSurname = DR["ManagerSurname"].ToString();
                    Man2.ManagerSalary = int.Parse(DR["ManagerSalary"].ToString());

                    ManList.Add(Man2); // Adding object to list 
                }
                Con.Close(); // Closing of connection 
            }
            return ManList; // returning the list 
        }

        // Insert managers 
        public void AddManager(ManagerModel Man) // Method that accepts a single argument 
        {
            using (SqlConnection Con = new SqlConnection(ConnectionStringProd)) // Method that accepts a single argument 
            {
                SqlCommand cmd = new SqlCommand("CreateManager", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // The user will enter the following values 
                cmd.Parameters.AddWithValue("@ManagerNumber", Man.ManagerNumber.ToString());
                cmd.Parameters.AddWithValue("@ManagerName", Man.ManagerName.ToString());
                cmd.Parameters.AddWithValue("@ManagerSurname", Man.ManagerSurname.ToString());
                cmd.Parameters.AddWithValue("@ManagerSalary", int.Parse(Man.ManagerSalary.ToString()));

                Con.Open(); // Opening of connection 
                cmd.ExecuteNonQuery();
                Con.Close(); // Closing of connection 
            }
        }

        //Get manager by manager number 
        public ManagerModel GetManagerByID(string ManID) // Method that accepts a single argument 
        {
            ManagerModel Man = new ManagerModel(); // Method that accepts a single argument 

            using (SqlConnection Con = new SqlConnection(ConnectionStringProd)) // This is the connection between the database and the application 
            {
                SqlCommand cmd = new SqlCommand("GetManagerByNumber", Con); // Getting/Calling the stored procedure 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ManagerNumber", ManID);

                Con.Open(); // Opening of connection
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                { // Reading of the data 
                    Man.ManagerNumber = dr["ManagerNumber"].ToString();
                    Man.ManagerName = dr["ManagerName"].ToString();
                    Man.ManagerSurname = dr["ManagerSurname"].ToString();
                    Man.ManagerSalary = int.Parse(dr["ManagerSalary"].ToString());
                }
                Con.Close(); // Closing of connection
            }
            return Man; // Returning of object 
        }

        //Update Managers 
        public void UpdateManagers(ManagerModel man)
        {
            using (SqlConnection Con = new SqlConnection(ConnectionStringProd))
            {
                SqlCommand cmd = new SqlCommand("UpdateManager", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ManagerNumber", man.ManagerNumber);
                cmd.Parameters.AddWithValue("@ManagerName", man.ManagerName);
                cmd.Parameters.AddWithValue("@ManagerSurname", man.ManagerSurname);
                cmd.Parameters.AddWithValue("@ManagerSalary", man.ManagerSalary);

                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close(); // Closing of connection 
            }
        }

        //Delete Manager 
        public void DeleteManager(string ManID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStringProd))
            {
                SqlCommand cmd = new SqlCommand("DeleteManagerByNumber", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("ManagerNumber", ManID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close(); // Closing of connection 
            }
        }
    }
}
