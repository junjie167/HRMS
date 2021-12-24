using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace HRMS
{
    public partial class Profile : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] != null)
            {
                String testID = Session["ID"].ToString();

                string constr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE employeeID=@id; ", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        cmd.Parameters.AddWithValue("@id", testID);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (!Page.IsPostBack)
                        {
                            tbHome.Text = ds.Tables[0].Rows[0]["homeAddress"].ToString();
                            tbPhone.Text = ds.Tables[0].Rows[0]["phoneNumber"].ToString();
                            String dobDate = ds.Tables[0].Rows[0]["dateOfBirth"].ToString();
                            LabelDOB.Text = dobDate.Remove(10);
                            LabelEmpNum.Text = ds.Tables[0].Rows[0]["employeeNumber"].ToString();
                            LabelBankAccNum.Text = ds.Tables[0].Rows[0]["bankAccountNumber"].ToString();
                            LabelBankName.Text = ds.Tables[0].Rows[0]["bankName"].ToString();
                            LabelBranch.Text = ds.Tables[0].Rows[0]["branchName"].ToString();
                            LabelAccStatus.Text = ds.Tables[0].Rows[0]["accountStatus"].ToString();
                        }

                    }
                }

            }

        }


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            int outputValue = 0;
            bool isNumber = false;
            isNumber = int.TryParse(tbPhone.Text, out outputValue);

            if (isNumber) {
                StringBuilder otherErrorMsg = new StringBuilder();
                String testID = Session["ID"].ToString();

                string constr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                string sql = "";
                SqlDataAdapter adapter = new SqlDataAdapter();
                sql = "UPDATE Employee Set homeAddress=@homeAdd, phoneNumber=@phoneNo WHERE employeeID=@id;";
                con.Open();

                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@homeAdd", tbHome.Text);
                command.Parameters.AddWithValue("@phoneNo", tbPhone.Text);
                command.Parameters.AddWithValue("@id", testID);
                command.ExecuteNonQuery();
                command.Dispose();
                con.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), " openUpdateModal", " openUpdateModal();", true);
            }
            else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), " openErrorModal", " openErrorModal();", true);
                //Response.Redirect("Profile.aspx");

            }


        }

        protected void BtnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }



        
    }

}