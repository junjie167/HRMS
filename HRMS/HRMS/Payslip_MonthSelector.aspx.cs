using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class Payslip_MonthSelector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            getDaysToPayDay();

        }
        protected void getDaysToPayDay()
        {
            DateTime today = DateTime.Today;
            DateTime payday = today;
            if (today.Year != 12 && today.Month == 1)
            {
                payday = new DateTime(today.Year, today.Month, 3);
            }
            else if (today.Month != 12 && today.Month != 1)
            {
                payday = new DateTime(today.Year, today.Month + 1, 3);
            }
            else
            {
                payday = new DateTime(today.Year + 1, today.Month - 11, 3);
            }

            TimeSpan ts = payday - today;
            numOfleaves.Text = ts.Days.ToString();
        }
        protected void nextPage_payslip(object sender, EventArgs e)
        {
            string payslip = payslipmonth.Value;
            int getMth;
            int getYear;
            string mth;

            getMth = int.Parse(payslip.Remove(0, 5));
            getYear = int.Parse(payslip.Remove(4));

            Session["getMth"] = getMth;
            Session["getYear"] = getYear;
            Session["dashBPS"] = 2;

            switch (getMth)
            {
                case 1:
                    //TextBox1.Text = "Jan";
                    mth = "January";
                    break;

                case 2:
                    //TextBox1.Text = "Feb";
                    mth = "February";
                    break;

                case 3:
                    //TextBox1.Text = "Mar";
                    mth = "March";
                    break;
                case 4:
                    //TextBox1.Text = "Apr";
                    mth = "April";
                    break;

                case 5:
                    //TextBox1.Text = "May";
                    mth = "May";
                    break;

                case 6:
                    //TextBox1.Text = "Jun";
                    mth = "June";
                    break;
                case 7:
                    //TextBox1.Text = "Jul";
                    mth = "July";
                    break;

                case 8:
                    //TextBox1.Text = "Aug";
                    mth = "August";
                    break;

                case 9:
                    //TextBox1.Text = "Sep";
                    mth = "September";
                    break;

                case 10:
                    //TextBox1.Text = "Oct";
                    mth = "October";
                    break;

                case 11:
                    //TextBox1.Text = "Nov";
                    mth = "November";
                    break;

                case 12:
                    //TextBox1.Text = "Dec";
                    mth = "December";
                    break;

                default:
                    Console.WriteLine($"broken");
                    break;
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"$('#verify').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
            //ScriptManager.RegisterStartupScript(this, this.GetType(), " verificationModal", " openverificationModal();", true);
        }
        protected void goToPaySlip(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            string pw = verifypw.Text; ;

            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connStr);
            string query = "SELECT * FROM Employee WHERE employeeID=@username AND password=@word;";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.Parameters.AddWithValue("@username", employeeID);
            cmd.Parameters.AddWithValue("@word", pw);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Response.Redirect("Payslip.aspx");
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");
                sb.Append(@"$('#fail').modal('show');");
                sb.Append(@"</script>");
                errorMsg.Text = "Invalid Password.";
                errorMsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
            }
        }
        protected void checkForPaySlip(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            string pw = cfmpw.Text;

            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connStr);
            string query = "SELECT * FROM Employee WHERE employeeID=@username AND password=@word;";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.Parameters.AddWithValue("@username", employeeID);
            cmd.Parameters.AddWithValue("@word", pw);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Response.Redirect("Payslip.aspx");
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");
                sb.Append(@"$('#fail').modal('show');");
                sb.Append(@"</script>");
                errorMsg.Text = "Invalid Password.";
                errorMsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
            }
        }
    }
}