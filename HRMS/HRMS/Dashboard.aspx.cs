using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace HRMS
{
    public partial class Dashboard : System.Web.UI.Page
    {
        //DataTable dt = new DataTable();
        protected DataTable dt;
        protected DataRowCollection drc;

        protected void Page_Load(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            getPendingCLaimsCount(employeeID);
            getRemainingLeaves(employeeID);
            getDaysToPayDay();
            fillCalendarData(employeeID);

            // dt.Columns.Add("EventName");
            //dt.Columns.Add("Date", typeof(DateTime));
            //dt.Rows.Add("Party", DateTime.Now.Date);
            //dt.Rows.Add("Annual", DateTime.Now.Date.AddDays(8));
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
                payday = new DateTime(today.Year, today.Month + 1 , 3);
            }
            else
            {
                payday = new DateTime(today.Year + 1, today.Month - 11, 3);
            }

            TimeSpan ts = payday - today;
            numOfleaves.Text = ts.Days.ToString();
        }

        protected void getPendingCLaimsCount(string employeeID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connStr);

            string query = "SELECT COUNT(*) FROM Claims WHERE employeeID=@ID and status='pending' ";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = employeeID;
            sqlConn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int result = Convert.ToInt32(reader[0].ToString());
                if (result != 0)
                {
                    //change and update label value
                    numoOfPendingClaims.Text = result.ToString();
                    numoOfPendingClaims.ForeColor = System.Drawing.Color.Orange;
                }
                else
                {
                    //if no value then 0
                    numoOfPendingClaims.Text = "0";
                }
            }
            //close the connection
            sqlConn.Close();
        }

        protected void getRemainingLeaves(string employeeID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connStr);

            string query = "Select leaveType, leaveAmount FROM LeaveQuota where employeeID=@ID";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = employeeID;
            sqlConn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string resultType = reader.GetValue(0).ToString();
                    int resultAmount = Convert.ToInt32(reader.GetValue(1));

                    if (resultType == "Annual")
                    {
                        numAnnual.Text = resultAmount.ToString();
                        Annual.Text = resultType;
                    }
                    else if (resultType == "Medical")
                    {
                        numMedical.Text = resultAmount.ToString();
                        Medical.Text = resultType;
                    }
                    else
                    {
                        numChildcare.Text = resultAmount.ToString();
                        Childcare.Text = resultType;
                    }
                    Debug.WriteLine(resultType);
                }
                reader.NextResult();
            }
            //close the connection
            sqlConn.Close();
        }

        protected void fillCalendarData(string employeeID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connStr);
            string query = "SELECT startDate, endDate, leaveStatus, leaveType FROM Leave WHERE employeeID=@ID and (leaveStatus='pending' or leaveStatus='Approved')";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = employeeID;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

            dt = new DataTable();
            dt.Columns.AddRange(new System.Data.DataColumn[] {
                new DataColumn("startDate", typeof(DateTime)), new DataColumn("endDate", typeof(DateTime))
            });
            dt.Columns.Add("EventName");
            dataAdapter.Fill(dt);
            drc = dt.Rows;
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            string pendingHex = "#FFB485";
            string approveHex = "#8FFDA6";
            if (drc.Count > 0)
            {
                Literal lit = new Literal();

                foreach (DataRow row in drc)
                {
                    DateTime startDate = Convert.ToDateTime(row["startDate"]);
                    DateTime endDate = Convert.ToDateTime(row["endDate"]);
                    string status = row["leaveStatus"].ToString();
                    string type = row["leaveType"].ToString();

                    //if (Convert.ToDateTime(e.Day.Date) == startDate)
                    if (e.Day.Date >= startDate.Date && e.Day.Date <= endDate)
                    {
                        if ((e.Day.Date.DayOfWeek == DayOfWeek.Saturday) || (e.Day.Date.DayOfWeek == DayOfWeek.Sunday))
                        {
                            e.Cell.BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            if (status == "pending")
                            {
                                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml(pendingHex);
                                lit.Text = "<br/>" + type;
                                e.Cell.Controls.Add(lit);
                            }
                            else if (status == "Approved")
                            {
                                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml(approveHex);
                                lit.Text = "<br/>" + type;
                                e.Cell.Controls.Add(lit);
                            }
                        }


                        // e.Cell.Text = e.Day.DayNumberText + "<br/>" + row["EventName"].ToString();
                    }

                }
            }




        }

        protected void addLeaves(object sender, EventArgs e)
        {
            Response.Redirect("Leave_addLeave.aspx");
        }

        protected void nextPage_leaves(object sender, EventArgs e)
        {
            Response.Redirect("LeaveSummary.aspx");
        }

        protected void nextPage_claims(object sender, EventArgs e)
        {
            Response.Redirect("claimSummary.aspx");
        }

        /* protected void nextPage_payslip(object sender, EventArgs e) {
             string payslipmth = payslipmonth.Value;
             int getMth;
             int getYear;
             string mth = "";

             getMth = int.Parse(payslipmth.Remove(0, 5));
             getYear = int.Parse(payslipmth.Remove(4));


             // Response.Redirect("Dashboard.aspx");
         }
        */
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

        protected void managerBtn_Click(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            bool access = false;

            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            string queryStr = "SELECT * FROM Employee WHERE employeeID =@paraemployeeid";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@paraemployeeid", employeeID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                access = bool.Parse(dr["managerAccess"].ToString());
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            if (access)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");
                sb.Append(@"$('#statuspopup').modal('show');");
                sb.Append(@"</script>");
             
                ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                // Response.Redirect("ManagerClaimSummary.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), " openManagerAccessModal", " openManagerAccessModal();", true);
            }
        }

        protected void pendingLeave_manager_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerLeaveSummary.aspx");
        }

        protected void pendingClaims_manager_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerClaimSummary.aspx");
        }
    }
}