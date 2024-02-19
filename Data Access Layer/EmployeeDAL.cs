using EPI_USE_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EPI_USE_Web_Application.Data_Access_Layer
{
    public class EmployeeDAL
    {
        // Connection to Azure SQL database
        string ConnectionStringProd = "Server=tcp:epiuse-server.database.windows.net,1433;Initial Catalog=EPI_USE_DB;Persist Security Info=False;User ID=epiuser;Password=Password_123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Get all Employees information
        public IEnumerable<EmployeeModel> AllEmployees()
        {
            List<EmployeeModel> EmpList = new List<EmployeeModel>(); // Creation of list with Employee data type

            using (SqlConnection Con = new SqlConnection(ConnectionStringProd))  // This is the connection between the database and the application
            {
                SqlCommand cmd = new SqlCommand("GetAllEmployees", Con); // Getting/Calling the stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                Con.Open(); // Opening the connection to allow the procedure to be used

                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read()) // Reading of the data
                {
                    EmployeeModel Emp2 = new EmployeeModel();
                    Emp2.EmployeeNumber = DR["EmployeeNumber"].ToString();
                    Emp2.EmployeeName = DR["EmployeeName"].ToString();
                    Emp2.EmployeeSurname = DR["EmployeeSurname"].ToString();
                    Emp2.BirthDate = DateTime.Parse(DR["BirthDate"].ToString());
                    Emp2.EmployeePosition = DR["EmployeePosition"].ToString();
                    Emp2.EmployeeSalary = int.Parse(DR["EmployeeSalary"].ToString());
                    Emp2.ManagerNumber= DR["ManagerNumber"].ToString();
                    EmpList.Add(Emp2); // Adding object to list
                }
                Con.Close(); // Closing of connection
            }
            return EmpList; // returning the list
        }

        // Insert/create employees
        public void AddEmployee(EmployeeModel emp) // Method that accepts a single argument
        {
            using (SqlConnection Con = new SqlConnection(ConnectionStringProd)) // Method that accepts a single argument
            {
                SqlCommand cmd = new SqlCommand("InsertEmployeeInfo", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // The user will enter the following values
                cmd.Parameters.AddWithValue("@EmployeeNumber", emp.EmployeeNumber.ToString());
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName.ToString());
                cmd.Parameters.AddWithValue("@EmployeeSurname", emp.EmployeeSurname.ToString());
                cmd.Parameters.AddWithValue("@EmployeePosition", emp.EmployeePosition.ToString());
                cmd.Parameters.AddWithValue("@BirthDate", DateTime.Parse(emp.BirthDate.ToString()));
                cmd.Parameters.AddWithValue("@EmployeeSalary", int.Parse(emp.EmployeeSalary.ToString()));
                cmd.Parameters.AddWithValue("@ManagerNumber", emp.ManagerNumber.ToString());

                Con.Open(); // Opening of connection
                cmd.ExecuteNonQuery();
                Con.Close(); // Closing of connection
            }
        }

        //Get employees by employee number 
        public EmployeeModel GetEmployeeByID(string empID) // Method that accepts a single argument
        {
            EmployeeModel emp = new EmployeeModel(); // Method that accepts a single argument

            using (SqlConnection Con = new SqlConnection(ConnectionStringProd)) // This is the connection between the database and the application
            {
                SqlCommand cmd = new SqlCommand("GetEmployeeByNumber", Con); // Getting/Calling the stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeNumber", empID);

                Con.Open(); // Opening of connection 
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                { // Reading of the data

                    emp.EmployeeNumber = dr["EmployeeNumber"].ToString();
                    emp.EmployeeName = dr["EmployeeName"].ToString();
                    emp.EmployeeSurname = dr["EmployeeSurname"].ToString();
                    emp.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
                    emp.EmployeePosition = dr["EmployeePosition"].ToString();
                    emp.EmployeeSalary = int.Parse(dr["EmployeeSalary"].ToString());
                    emp.ManagerNumber = dr["ManagerNumber"].ToString();
                }
                Con.Close(); // Closing of connection 
            }
            return emp; // Returning of object
        }

        //Update Employees
        public void UpdateEmployees(EmployeeModel emp)
        {
            using (SqlConnection Con = new SqlConnection(ConnectionStringProd))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployee", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeNumber", emp.EmployeeNumber.ToString());
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName.ToString());
                cmd.Parameters.AddWithValue("@EmployeeSurname", emp.EmployeeSurname.ToString());
                cmd.Parameters.AddWithValue("@EmployeePosition", emp.EmployeePosition.ToString());
                cmd.Parameters.AddWithValue("@BirthDate", DateTime.Parse(emp.BirthDate.ToString()));
                cmd.Parameters.AddWithValue("@EmployeeSalary", int.Parse(emp.EmployeeSalary.ToString()));
                cmd.Parameters.AddWithValue("@ManagerNumber", emp.ManagerNumber.ToString());

                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close(); // Closing of connection
            }
        }

        //Delete Employee
        public void DeleteEmployee(string empID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStringProd))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployeeByNumber", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("EmployeeNumber", empID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close(); // Closing of connection
            }
        }
    }
}

