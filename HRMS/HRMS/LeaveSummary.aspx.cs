using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using HRMS.DAL;


namespace HRMS
{
    public partial class LeaveSummary : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Leave l = new Leave();
            if (!IsPostBack)
            {

                string employeeID = Session["ID"].ToString();
               
                BindRepeater(Convert.ToInt32(employeeID));
                Bindgraph(Convert.ToInt32(employeeID));
                getpendingcount(Convert.ToInt32(employeeID));
                getMedicalCount(Convert.ToInt32(employeeID));
                getChildcareCount(Convert.ToInt32(employeeID));
                getAnnualCount(Convert.ToInt32(employeeID));
            }
        }


        protected void getAnnualCount(int empid)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn = new SqlConnection(connStr);
            SqlCommand SqlCmd = new SqlCommand("Select leaveAmount from leaveQuota where employeeID=@ID and leaveType='Annual' ", SqlCnn);
            SqlCmd.Parameters.Add("@ID", SqlDbType.Int).Value = empid;

            SqlCnn.Open();
            Int32 count = Convert.ToInt32(SqlCmd.ExecuteScalar());
            if (count > 0)
            {
                annualcount.Text = Convert.ToString(count.ToString()); //For example a Label
            }
            else
            {
                annualcount.Text = "0";
                Annual.Attributes.Add("onclick", "return false;");
                Annual.Enabled = false;
                Annual.Font.Underline = false;
            }
            SqlCnn.Close(); //Remember close the connection
        }
        protected void getChildcareCount(int empid)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn = new SqlConnection(connStr);
            SqlCommand SqlCmd = new SqlCommand("Select leaveAmount from leaveQuota where employeeID=@ID and leaveType='Childcare' ", SqlCnn);
            SqlCmd.Parameters.Add("@ID", SqlDbType.Int).Value = empid;

            SqlCnn.Open();
            Int32 count = Convert.ToInt32(SqlCmd.ExecuteScalar());
            if (count > 0)
            {
                childcarecount.Text = Convert.ToString(count.ToString()); //For example a Label
            }
            else
            {
                childcarecount.Text = "0";
                Childcare.Attributes.Add("onclick", "return false;");
                Childcare.Enabled = false;
                Childcare.Font.Underline = false;
            }
            SqlCnn.Close(); //Remember close the connection
        }
        protected void getMedicalCount(int empid)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn = new SqlConnection(connStr);
            SqlCommand SqlCmd = new SqlCommand("Select leaveAmount from leaveQuota where employeeID=@ID and leaveType='Medical' ", SqlCnn);
            SqlCmd.Parameters.Add("@ID", SqlDbType.Int).Value = empid;

            SqlCnn.Open();
            Int32 count = Convert.ToInt32(SqlCmd.ExecuteScalar());
            if (count > 0)
            {
                medicalcount.Text = Convert.ToString(count.ToString()); //For example a Label
            }
            else
            {
                medicalcount.Text = "0";
                medi.Attributes.Add("onclick", "return false;");
                medi.Enabled = false;
                medi.Font.Underline = false;
            }
            SqlCnn.Close(); //Remember close the connection
        }
        protected void getpendingcount(int empid)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn = new SqlConnection(connStr);
            SqlCommand SqlCmd = new SqlCommand("Select count(*) from Leave where employeeID=@ID and leaveStatus='pending' ", SqlCnn);
            SqlCmd.Parameters.Add("@ID", SqlDbType.Int).Value = empid;

            SqlCnn.Open();
            Int32 count = Convert.ToInt32(SqlCmd.ExecuteScalar());
            if (count > 0)
            {
                pendingcount.Text = Convert.ToString(count.ToString()); //For example a Label
            }
            else
            {
                pendingcount.Text = "0";
            }
            SqlCnn.Close(); //Remember close the connection
        }


        private void BindRepeater(int empid)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection cn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            cmd.CommandText = "SELECT leaveID, leaveType, startDate, endDate, leaveStatus FROM Leave WHERE employeeID = @empid AND leaveStatus='pending'";
            cmd.Parameters.AddWithValue("@empid", empid);

            DataTable dt = new DataTable();
            ad.SelectCommand = cmd;
            ad.Fill(dt);


            PagedDataSource pgitems = new PagedDataSource();
            pgitems.DataSource = dt.DefaultView;
            pgitems.AllowPaging = true;

            //Control page size from here 
            pgitems.PageSize = 2;
            pgitems.CurrentPageIndex = PageNumber;
            if (pgitems.PageCount > 1)
            {
                rptPaging.Visible = true;
                ArrayList pages = new ArrayList();
                for (int i = 0; i <= pgitems.PageCount - 1; i++)
                {
                    pages.Add((i + 1).ToString());
                }
                rptPaging.DataSource = pages;
                rptPaging.DataBind();
            }
            else
            {
                rptPaging.Visible = false;
            }

            //Finally, set the datasource of the repeater
            Repeater1.DataSource = pgitems;
            Repeater1.DataBind();
        }



        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber"] = value; }
        }

        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
            BindRepeater(Convert.ToInt32(employeeID));
        }

        protected void changeback(object sender, CommandEventArgs e)
        {

            Response.Redirect("LeaveSummary.aspx");

        }
        
        protected void trigger(object sender, CommandEventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            HtmlTableRow row = ((sender as LinkButton).NamingContainer.FindControl("row") as HtmlTableRow);
            row.Attributes["style"] = "background-color:#FBEFE4";

            int pass = Convert.ToInt32(e.CommandArgument);
            getid.Text = pass.ToString();


            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn = new SqlConnection(connStr);
            SqlCommand SqlCmd = new SqlCommand("Select * from Leave where leaveID=@ID and employeeID=@empid", SqlCnn);
            SqlCmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Convert.ToInt32(getid.Text);
            SqlCmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = Convert.ToInt32(employeeID);
            SqlCnn.Open();


            using (SqlDataReader dataReader = SqlCmd.ExecuteReader())
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
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"$('#DeleteModal').modal('show');");
            sb.Append(@"</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

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
        protected void deleting(Object sender, CommandEventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            if (e.CommandName == "remove")
            {

                System.Diagnostics.Debug.WriteLine(e.CommandName);
                string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection SqlCnn = new SqlConnection(connStr);
                SqlCommand SqlCmd = new SqlCommand("delete Leave where leaveID=@ID and employeeID=@empid", SqlCnn);
                SqlCmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Convert.ToInt32(getid.Text);
                SqlCmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = Convert.ToInt32(employeeID);

                // SqlCommand sqlcmd1 = new SqlCommand("UPDATE leaveQuota SET leaveAmount=@amt where leaveType=@lt and employeeID = @empid");
                SqlCommand sqlcmd1 = new SqlCommand("UPDATE leaveQuota SET leaveAmount = leaveAmount + @duration WHERE leaveType = @lt AND employeeID = @empid", SqlCnn);
                sqlcmd1.Parameters.Add("@duration", SqlDbType.Int).Value = Convert.ToInt32(duration.Text);
                sqlcmd1.Parameters.Add("@lt", SqlDbType.VarChar).Value = type.Text;
                sqlcmd1.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(employeeID);

                try
                {
                    SqlCnn.Open();
                    sqlcmd1.ExecuteNonQuery();
                    SqlCmd.ExecuteNonQuery();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"$('#statuspopup').modal('show');");
                    sb.Append(@"</script>");
                    Label6.Text = "Successful";
                    Label7.Text = "The deletion is successful";
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
                    Label7.Text = "The deletion is unsuccessful";
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                }
                finally
                {
                    if (SqlCnn.State == ConnectionState.Open)
                        SqlCnn.Close();

                }
                BindRepeater(Convert.ToInt32(employeeID));


            }
        }



        //not working
        protected void Bindgraph(int empid)
        {

            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection SqlCnn = new SqlConnection(connStr);


            ArrayList valuesList = new ArrayList();

            StringBuilder sb = new StringBuilder();
            SqlCnn.Open();
            SqlCommand SqlCmd = new SqlCommand("Select leaveType from leaveQuota where employeeID=@ID", SqlCnn);
            SqlCmd.Parameters.Add("@ID", SqlDbType.Int).Value = empid;

            string idcalrd;
            using (SqlDataReader dataReader = SqlCmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    string leaveType = dataReader["leaveType"].ToString();
                    valuesList.Add(leaveType);
                }
            }
            SqlCnn.Close();
            foreach (object item in valuesList)
            {

            }


        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowAllLeave.aspx");
        }

        protected void medi_Click1(object sender, EventArgs e)
        {
            string type = "Medical";
            Session["type"] = type;
            Response.Redirect("Leave_AddLeave1.aspx?type=" + type);
        }

        protected void Childcare_Click(object sender, EventArgs e)
        {
            string type = "Childcare";
            Session["type"] = type;
            Response.Redirect("Leave_AddLeave1.aspx?type=" + type);
        }

        protected void Annual_Click(object sender, EventArgs e)
        {
            string type = "Annual";
            Session["type"] = type;
            Response.Redirect("Leave_AddLeave1.aspx?type=" + type);
        }
    }
}
