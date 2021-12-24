using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HRMS.DAL;
namespace HRMS
{
    public partial class ShowAllLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            if (!IsPostBack)
            {
                BindRepeator(Convert.ToInt32(employeeID));
            }

        }

        //binding
        private void BindRepeator(int empid)
        {
            string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Leave WHERE employeeID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", empid);
                con.Open();
                RepeatInformation.DataSource = cmd.ExecuteReader();
                RepeatInformation.DataBind();
            }
        }


        protected void RepeatInformation_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            foreach (RepeaterItem item in RepeatInformation.Items)
            {
                Label AppAmmount = (Label)item.FindControl("lblStatus");
                //System.Diagnostics.Debug.WriteLine(AppAmmount.Text.ToString());
                string color = "background-color: #FBEFE4";
                HtmlTableRow row = item.FindControl("row") as HtmlTableRow;
                if(AppAmmount.Text == "pending")
                {
                    row.Attributes["style"] = color;
                }
                if (AppAmmount.Text != "pending")
                {
                    System.Diagnostics.Debug.WriteLine("shuuu");
                    LinkButton chb = (LinkButton)item.FindControl("editbtn");
                    LinkButton chb1 = (LinkButton)item.FindControl("btndel");
                    chb.Visible = false;
                    chb1.Visible = false;

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("heyy");
                    LinkButton chb = (LinkButton)item.FindControl("editbtn");
                    LinkButton chb1 = (LinkButton)item.FindControl("btndel");
                    chb.Visible = true;
                    chb1.Visible = true;
                }
            }

          
        }


        protected void latest_Click(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();

            if (ddlfilter.SelectedValue == "All")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Leave] where employeeID=@ID ORDER BY leaveID DESC", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (ddlfilter.SelectedValue == "Pending")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Leave] where employeeID=@ID and leaveStatus = 'pending' ORDER BY leaveID DESC", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (ddlfilter.SelectedValue == "Approved")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Leave] where employeeID=@ID and leaveStatus = 'Approved' ORDER BY leaveID DESC", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (ddlfilter.SelectedValue == "Rejected")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Leave] where employeeID=@ID and leaveStatus = 'Rejected' ORDER BY leaveID DESC", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }

        }
        protected void oldest_Click(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            if (ddlfilter.SelectedValue == "All")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Leave WHERE employeeID=@ID ORDER BY leaveID ASC", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (ddlfilter.SelectedValue == "Pending")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Leave] where employeeID=@ID and leaveStatus = 'pending' ORDER BY leaveID ASC", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (ddlfilter.SelectedValue == "Approved")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Leave] where employeeID=@ID and leaveStatus = 'Approved' ORDER BY leaveID ASC", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (ddlfilter.SelectedValue == "Rejected")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Leave] where employeeID=@ID and leaveStatus = 'Rejected' ORDER BY leaveID ASC", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
        }
        protected void ddlfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlfilter.SelectedItem.ToString();
            string employeeID = Session["ID"].ToString();

            if (selectedValue == "All")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Leave WHERE employeeID=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (selectedValue == "Pending")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Leave WHERE employeeID=@ID and leaveStatus= 'pending'", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (selectedValue == "Approved")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Leave WHERE employeeID=@ID and leaveStatus= 'Approved'", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
            else if (selectedValue == "Rejected")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Leave WHERE employeeID=@ID and leaveStatus= 'Rejected'", con);
                    cmd.Parameters.AddWithValue("@ID", employeeID);
                    con.Open();
                    RepeatInformation.DataSource = cmd.ExecuteReader();
                    RepeatInformation.DataBind();
                }
            }
        }

        protected void edit(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                int leaveid = Convert.ToInt32(e.CommandArgument);
                Session["leaveId"] = leaveid;
                Response.Redirect("Editleave.aspx?leaveId=" + leaveid);
            }
        }
        protected void trigger(object sender, CommandEventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            HtmlTableRow row = ((sender as LinkButton).NamingContainer.FindControl("row") as HtmlTableRow);
            row.Attributes["style"] = "background-color:#FBEFE4";


            int pass = Convert.ToInt32(e.CommandArgument);
            getid.Text = pass.ToString(); //leave id


            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn = new SqlConnection(connStr);
            SqlCommand SqlCmd = new SqlCommand("Select * from Leave where leaveID=@ID and employeeID=@empid", SqlCnn);
            SqlCmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(getid.Text);
            SqlCmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(employeeID);
            SqlCnn.Open();


            using (SqlDataReader dataReader = SqlCmd.ExecuteReader()) //store session
            {
                while (dataReader.Read())
                {
                    id.Text = dataReader["leaveID"].ToString();
                    type.Text = dataReader["leaveType"].ToString();
                    start.Text = dataReader["startDate"].ToString();
                    end.Text = dataReader["endDate"].ToString();
                    duration.Text = dataReader["Duration"].ToString();
                    status.Text = dataReader["leaveStatus"].ToString();

                }
            }
            SqlCnn.Close();

            System.Text.StringBuilder sb = new System.Text.StringBuilder(); //to show popup with table
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"$('#DeleteModal').modal('show');");
            sb.Append(@"</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
        }
        protected void changeback(object sender, CommandEventArgs e)
        {
            Response.Redirect("ShowAllLeave.aspx");
        }
        protected void deleting(Object sender, CommandEventArgs e)
        {

            string employeeID = Session["ID"].ToString();

            if (e.CommandName == "remove")
            {
                string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection SqlCnn = new SqlConnection(connStr);
                SqlCommand SqlCmd = new SqlCommand("delete Leave where leaveID=@ID and employeeID=@emppid", SqlCnn);
                SqlCmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Convert.ToInt32(getid.Text); //leave id
                SqlCmd.Parameters.Add("@emppid", SqlDbType.VarChar).Value = Convert.ToInt32(employeeID); //emp id

                //upon delete change back the count
                SqlCommand sqlcmd1 = new SqlCommand("UPDATE leaveQuota SET leaveAmount = leaveAmount + @duration WHERE leaveType = @lt AND employeeID = @empid", SqlCnn);
                sqlcmd1.Parameters.Add("@duration", SqlDbType.Int).Value = Convert.ToInt32(duration.Text);
                sqlcmd1.Parameters.Add("@lt", SqlDbType.VarChar).Value = type.Text;
                sqlcmd1.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(employeeID);

                try
                {
                    SqlCnn.Open();
                    SqlCmd.ExecuteNonQuery();
                    sqlcmd1.ExecuteNonQuery();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>"); //success
                    sb.Append(@"$('#statuspopup').modal('show');");
                    sb.Append(@"</script>");
                    Label6.Text = "Successful";
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"$('#statuspopup').modal('show');");
                    sb.Append(@"</script>");
                    Label6.Text = "Failed";
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                }
                finally
                {
                    if (SqlCnn.State == ConnectionState.Open)
                        SqlCnn.Close();

                }
                BindRepeator(Convert.ToInt32(employeeID));
            }
        }
    }
}