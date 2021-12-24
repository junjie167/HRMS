using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace HRMS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            int outputValue = 0;
            bool isNumber = false;
            isNumber = int.TryParse(TbID.Text, out outputValue);

            if (isNumber)
            {
                string constr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("select * from Employee where employeeID=@username and password=@word;", con);
                cmd.Parameters.AddWithValue("@username", TbID.Text);
                cmd.Parameters.AddWithValue("@word", TbPass.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    Session["ID"] = TbID.Text;
                    Session["pass"] = TbPass.Text;
                    Response.Redirect("Dashboard.aspx");
                    //Response.Write("PASSED");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), " openErrorModal", " openErrorModal();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), " openErrorModal", " openErrorModal();", true);
            }

        }

    }

}
