using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace HRMS.Master
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            displayName(employeeID);
        }
        ///*
        protected void displayName(string employeeID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connStr);

            string query = "SELECT employeeName FROM Employee WHERE employeeID=@ID";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = employeeID;
            sqlConn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string result = reader[0].ToString();
                if (result != "" && result != null)
                {
                    employeeName.Text = result;
                }
            }
            //close the connection
            sqlConn.Close();
        }
      
    }
}