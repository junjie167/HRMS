using HRMS.DAL;
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

namespace HRMS
{
    public partial class ManagerLeaveSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            if (!IsPostBack)
            {
                BindRepeator(Convert.ToInt32(employeeID));
                DropDownList1.BorderWidth = 2;
                DropDownList1.BorderColor = System.Drawing.Color.Orange;
            }
        }

        private void BindRepeator(int empid)
        {
            string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT l.leaveType, l.leaveID,  l.startDate, l.endDate, l.leaveStatus, l.duration, e.employeeName FROM Leave l" +
                    " INNER JOIN Employee e ON l.employeeID = e.employeeID WHERE l.employeeID!=@ID and l.leaveStatus='pending'", con);

                cmd.Parameters.AddWithValue("@ID", empid);
                con.Open();
                Repeater1.DataSource = cmd.ExecuteReader();
                Repeater1.DataBind();
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                Label AppAmmount = (Label)item.FindControl("lblStatus");
                string color = "background-color: #FBEFE4";
                string color1 = "background-color: #DAF7A6";
                string color2 = "background-color: #fadadd";
                HtmlTableRow row = item.FindControl("row") as HtmlTableRow;
                if (AppAmmount.Text == "pending")
                {
                    row.Attributes["style"] = color;
                }
                if (AppAmmount.Text == "Approved")
                {
                    row.Attributes["style"] = color1;
                }
                if (AppAmmount.Text == "Rejected")
                {
                    row.Attributes["style"] = color2;
                }
                if (AppAmmount.Text != "pending")
                {
                    System.Diagnostics.Debug.WriteLine("shuuu");
                    LinkButton chb = (LinkButton)item.FindControl("btnapprove");
                    LinkButton chb1 = (LinkButton)item.FindControl("btnreject");
                    chb.Visible = false;
                    chb1.Visible = false;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("heyy");
                    LinkButton chb = (LinkButton)item.FindControl("btnapprove");
                    LinkButton chb1 = (LinkButton)item.FindControl("btnreject");
                    chb.Visible = true;
                    chb1.Visible = true;
                }
            }
        }
        protected void Approve(object sender, CommandEventArgs e)
        {
            string employeeID = Session["ID"].ToString();

            if (e.CommandName == "Approve")
            {
                string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection SqlCnn = new SqlConnection(connStr);
                SqlCommand sqlcmd1 = new SqlCommand("UPDATE Leave SET leaveStatus = 'Approved' WHERE leaveID = @lid", SqlCnn);
                sqlcmd1.Parameters.Add("@lid", SqlDbType.Int).Value = Convert.ToInt32(id.Text);

                try
                {
                    SqlCnn.Open();
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
            }

            BindRepeator(Convert.ToInt32(employeeID));
        }
        protected void approvepop(object sender, CommandEventArgs e)
        {
            //string employeeID = Session["ID"].ToString();
            HtmlTableRow row = ((sender as LinkButton).NamingContainer.FindControl("row") as HtmlTableRow);
            row.Attributes["style"] = "background-color:#FBEFE4";

            int pass = Convert.ToInt32(e.CommandArgument);
            getid.Text = pass.ToString(); //leave id

            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn = new SqlConnection(connStr);
            SqlCommand SqlCmd = new SqlCommand("SELECT l.leaveType, l.leaveID,  l.startDate, l.endDate, l.duration, l.leaveStatus, e.employeeName FROM Leave l" +
                    " INNER JOIN Employee e ON l.employeeID = e.employeeID WHERE l.leaveID=@ID", SqlCnn);
            SqlCmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(getid.Text);
            SqlCnn.Open();

            using (SqlDataReader dataReader = SqlCmd.ExecuteReader()) //store session
            {
                while (dataReader.Read())
                {
                    id.Text = dataReader["leaveID"].ToString();
                    type.Text = dataReader["leaveType"].ToString();
                    start.Text = dataReader["startDate"].ToString();
                    end.Text = dataReader["endDate"].ToString();
                    status.Text = dataReader["leaveStatus"].ToString();
                    employeeName.Text = dataReader["employeeName"].ToString();
                    duration.Text = dataReader["duration"].ToString();

                }
            }
            SqlCnn.Close();

            System.Text.StringBuilder sb = new System.Text.StringBuilder(); //to show popup with table
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"$('#approveModal').modal('show');");
            sb.Append(@"</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

        }
        protected void rejectpop(object sender, CommandEventArgs e)
        {
            int pass = Convert.ToInt32(e.CommandArgument);
            Label1.Text = pass.ToString(); //leave id

            string connStr1 = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn1 = new SqlConnection(connStr1);
            SqlCommand SqlCmd1 = new SqlCommand("SELECT l.employeeID, l.leaveType, l.leaveID,  l.startDate, l.endDate, l.duration, l.leaveStatus, e.employeeName FROM Leave l" +
                    " INNER JOIN Employee e ON l.employeeID = e.employeeID WHERE l.leaveID=@ID", SqlCnn1);
            SqlCmd1.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(Label1.Text);
            SqlCnn1.Open();
            using (SqlDataReader dataReader = SqlCmd1.ExecuteReader()) //store session
            {
                while (dataReader.Read())
                {
                    du.Text = dataReader["duration"].ToString();
                    empid.Text = dataReader["employeeID"].ToString();
                    type1.Text = dataReader["leaveType"].ToString();
                    System.Diagnostics.Debug.WriteLine(type1.Text);
                }
            }
            SqlCnn1.Close();

            System.Text.StringBuilder sb = new System.Text.StringBuilder(); //to show popup with table
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"$('#rejectModal').modal('show');");
            sb.Append(@"</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
        }
        protected void Reject(object sender, CommandEventArgs e)
        {
            string employeeID = Session["ID"].ToString();



            if (e.CommandName == "Reject")
            {
                string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection SqlCnn = new SqlConnection(connStr);
                SqlCommand sqlcmd1 = new SqlCommand("UPDATE Leave SET leaveStatus='Rejected', rejectedReason =@rr WHERE leaveID = @lid", SqlCnn);
                sqlcmd1.Parameters.Add("@rr", SqlDbType.VarChar).Value = TextBox1.Text;
                sqlcmd1.Parameters.Add("@lid", SqlDbType.Int).Value = Convert.ToInt32(Label1.Text);

                Leave aleave = new Leave();
                aleave.updatequotaAdd(Convert.ToInt32(empid.Text), type1.Text, Convert.ToInt32(du.Text));
                try
                {
                    SqlCnn.Open();
                    sqlcmd1.ExecuteNonQuery();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>"); //success
                    sb.Append(@"$('#statuspopup1').modal('show');");
                    sb.Append(@"</script>");
                    Label2.Text = "Successful";
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"$('#statuspopup').modal('show');");
                    sb.Append(@"</script>");
                    Label2.Text = "Failed";
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                }
                finally
                {
                    if (SqlCnn.State == ConnectionState.Open)
                        SqlCnn.Close();

                    TextBox1.Text = "";
                }
            }

            BindRepeator(Convert.ToInt32(employeeID));
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid = Session["ID"].ToString();
            string selectedValue = DropDownList1.SelectedItem.ToString();
            if (selectedValue == "Pending List")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT l.leaveType, l.leaveID,  l.startDate, l.endDate, l.leaveStatus,l.duration, e.employeeName FROM Leave l" +
                        " INNER JOIN Employee e ON l.employeeID = e.employeeID WHERE l.employeeID!=@ID and l.leaveStatus='pending' ", con);

                    cmd.Parameters.AddWithValue("@ID", empid);
                    con.Open();
                    Repeater1.DataSource = cmd.ExecuteReader();
                    Repeater1.DataBind();
                }
                DropDownList1.BorderWidth = 2;
                DropDownList1.BorderColor = System.Drawing.Color.Orange;
            }
            else if (selectedValue == "Approved List")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT l.leaveType, l.leaveID,  l.startDate, l.endDate, l.leaveStatus, l.duration, e.employeeName FROM Leave l" +
                        " INNER JOIN Employee e ON l.employeeID = e.employeeID WHERE l.employeeID!=@ID and l.leaveStatus='Approved'", con);

                    cmd.Parameters.AddWithValue("@ID", empid);
                    con.Open();

                    Repeater1.DataSource = cmd.ExecuteReader();
                    Repeater1.DataBind();
                }
                DropDownList1.BorderWidth = 2;
                DropDownList1.BorderColor = System.Drawing.Color.Green;
            }
            else if (selectedValue == "Rejected List")
            {
                string CS = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT l.leaveType, l.leaveID,  l.startDate, l.endDate, l.leaveStatus,  l.duration, e.employeeName FROM Leave l" +
                        " INNER JOIN Employee e ON l.employeeID = e.employeeID WHERE l.employeeID!=@ID and l.leaveStatus='Rejected'", con);

                    cmd.Parameters.AddWithValue("@ID", empid);
                    con.Open();
                    Repeater1.DataSource = cmd.ExecuteReader();
                    Repeater1.DataBind();
                    
                }
                
                DropDownList1.BorderWidth = 2;
                DropDownList1.BorderColor = System.Drawing.Color.Crimson;
            }
        }


    }
}
