using HRMS.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class Leave_AddLeave1 : System.Web.UI.Page
    {
        protected DataTable dt;
        protected DataRowCollection drc;
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            tbEmpId.Text = employeeID;
            getCurrentDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            fillCalendarData(Convert.ToInt32(employeeID));
            
            Leave aleave = new Leave();
            string type = Session["type"].ToString();

            if (!IsPostBack)
            {
                if (type == "Annual")
                {
                    DdlTypeLeave.SelectedValue = "Annual";
                    if (aleave.getLeaveCount(Convert.ToInt32(employeeID), "Annual").leaveAmt > 0)
                    {
                        choosen.Text = "Annual" + ":" + " " + aleave.getLeaveCount(Convert.ToInt32(employeeID), "Annual").leaveAmt;
                        choosen.ForeColor = System.Drawing.Color.Green;
                        remainingText.ForeColor = System.Drawing.Color.Green;
                        choosen.Visible = true;
                        remainingText.Visible = true;

                    }
                    else
                    {
                        choosen.Text = "Annual" + ":" + " " + aleave.getLeaveCount(Convert.ToInt32(employeeID), "Annual").leaveAmt;
                        choosen.ForeColor = System.Drawing.Color.Red;
                        remainingText.ForeColor = System.Drawing.Color.Red;
                        choosen.Visible = true;
                        remainingText.Visible = true;
                    }
                }
                else if (type == "Childcare")
                {
                    DdlTypeLeave.SelectedValue = "Childcare";
                    if (aleave.getLeaveCount(Convert.ToInt32(employeeID), "Childcare").leaveAmt > 0)
                    {
                        choosen.Text = "Childcare" + ":" + " " + aleave.getLeaveCount(Convert.ToInt32(employeeID), "Childcare").leaveAmt;
                        choosen.ForeColor = System.Drawing.Color.Green;
                        remainingText.ForeColor = System.Drawing.Color.Green;
                        choosen.Visible = true;
                        remainingText.Visible = true;


                    }
                    else
                    {
                        choosen.Text = "Childcare" + ":" + " " + aleave.getLeaveCount(Convert.ToInt32(employeeID), "Childcare").leaveAmt;
                        choosen.ForeColor = System.Drawing.Color.Red;
                        remainingText.ForeColor = System.Drawing.Color.Red;
                        choosen.Visible = true;
                        remainingText.Visible = true;
                    }
                }
                else if (type == "Medical")
                {
                    DdlTypeLeave.SelectedValue = "Medical";
                    if (aleave.getLeaveCount(Convert.ToInt32(employeeID), "Medical").leaveAmt > 0)
                    {
                        choosen.Text = "Medical" + ":" + " " + aleave.getLeaveCount(Convert.ToInt32(employeeID), "Medical").leaveAmt;
                        choosen.ForeColor = System.Drawing.Color.Green;
                        remainingText.ForeColor = System.Drawing.Color.Green;
                        choosen.Visible = true;
                        remainingText.Visible = true;

                    }
                    else
                    {
                        choosen.Text = "Medical" + ":" + " " + aleave.getLeaveCount(Convert.ToInt32(employeeID), "Medical").leaveAmt;
                        choosen.ForeColor = System.Drawing.Color.Red;
                        remainingText.ForeColor = System.Drawing.Color.Red;
                        choosen.Visible = true;
                        remainingText.Visible = true;
                    }
                }
            }
            
        }

        protected void fillCalendarData(int empid)
        {
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connStr);
            string query = "SELECT startDate, endDate, leaveStatus, leaveType FROM Leave WHERE employeeID=@ID and (leaveStatus='pending' or leaveStatus='Approved')";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = empid;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);


            dataAdapter.Fill(ds, "Table");
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            Literal lit = new Literal();
            // check if ds has a table
            if (ds.Tables.Count > 0)
            {
                //check if ds.table[0] has any rows  
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((dr["startDate"].ToString() != DBNull.Value.ToString()) &&
                            (dr["endDate"].ToString() != DBNull.Value.ToString()))
                        {
                            DateTime dtEvent = Convert.ToDateTime(dr["startDate"]);
                            DateTime dtEvent1 = Convert.ToDateTime(dr["endDate"]);
                            string type = dr["leaveType"].ToString();

                            if (e.Day.Date >= dtEvent.Date && e.Day.Date <= dtEvent1)
                            {
                                if (type == "Medical")
                                {
                                    lit.Text = "<br/>" + 'M';
                                    e.Cell.Controls.Add(lit);

                                }
                                if (type == "Annual")
                                {
                                    lit.Text = "<br/>" + 'A';
                                    e.Cell.Controls.Add(lit);
                                }
                                if (type == "Childcare")
                                {
                                    lit.Text = "<br/>" + 'C';
                                    e.Cell.Controls.Add(lit);
                                }
                                e.Cell.BackColor = ColorTranslator.FromHtml("#FBEFE4");
                                DayOfWeek day = e.Day.Date.DayOfWeek;
                                if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
                                {
                                    e.Cell.BackColor = System.Drawing.Color.White;
                                    lit.Text = "<br/>" + "";
                                    e.Cell.Controls.Add(lit);
                                }
                            }
                        }
                    }
                }
            }
        }


        protected void DdlTypeLeave_SelectedIndexChanged(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            string selectedValue = DdlTypeLeave.SelectedItem.ToString();

            Leave lv = new Leave();

            if (selectedValue == "Choose:")
            {
                remainingText.Visible = false;
                choosen.Visible = false;
                error.Visible = true;
                error.Text = "Please select a choice";
                error.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                error.Visible = false;
                remainingText.Visible = true;
                choosen.Visible = true;
                if (lv.getLeaveCount(Convert.ToInt32(employeeID), selectedValue).leaveAmt > 0)
                {
                    choosen.Text = selectedValue + ":" + " " + lv.getLeaveCount(Convert.ToInt32(employeeID), selectedValue).leaveAmt;
                    choosen.ForeColor = System.Drawing.Color.Green;
                    remainingText.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    choosen.Text = selectedValue + ":" + " " + lv.getLeaveCount(Convert.ToInt32(employeeID), selectedValue).leaveAmt;
                    choosen.ForeColor = System.Drawing.Color.Red;
                    remainingText.ForeColor = System.Drawing.Color.Red;
                }
            }

        }


        protected void insert(Object sender, CommandEventArgs e)
        {
            if (e.CommandName == "insert")
            {
                if (Session["Data"] != null)
                {
                    Leave detail = (Leave)Session["Data"];

                    Leave lv = new Leave(detail.leaveType, Convert.ToInt32(detail.employeeID), detail.startDate, detail.endDate, Convert.ToInt32(detail.duration), "pending", 45, "No comments");

                    int result;
                    result = lv.leaveInsert();
                    if (result > 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"$('#statuspopup').modal('show');");
                        sb.Append(@"</script>");
                        Label6.Text = "Successful";
                        label7.Text = "Leave added successfully";
                        ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                        lv.updatequota(Convert.ToInt32(detail.employeeID), detail.leaveType, detail.duration);
                    }
                    else
                    {
                        Response.Write("<script>alert('Insert NOT successful'); window.location= 'Leave_addLeave.aspx';</script>");
                    }

                    // Clear the session.
                    Session["Data"] = null;

                }
            }
        }


        protected void submit_Click(object sender, EventArgs e)
        {
            Leave l = new Leave();

            string leaveType = null;
            string employeeID = Session["ID"].ToString();

            if (DdlTypeLeave.SelectedIndex > -1)
            {
                if (DdlTypeLeave.Items[0].Selected)
                {
                    leaveType = DdlTypeLeave.Items[0].Text;
                    remainingText.Visible = false;
                    choosen.Visible = false;
                    error.Visible = true;
                    error.Text = "Please select a choice";
                    error.ForeColor = System.Drawing.Color.Red;
                    error.Visible = true;
                }
                else
                {
                    if (DdlTypeLeave.Items[1].Selected)
                    {
                        leaveType = DdlTypeLeave.Items[1].Text;
                    }
                    if (DdlTypeLeave.Items[2].Selected)
                    {
                        leaveType = DdlTypeLeave.Items[2].Text;
                    }
                    if (DdlTypeLeave.Items[3].Selected)
                    {
                        leaveType = DdlTypeLeave.Items[3].Text;
                    }
                    string startDate = Page.Request.Form["pickup_date"].ToString();
                    string endDate = Page.Request.Form["dropoff_date"].ToString();
                    string strValue = Page.Request.Form["numdays"].ToString();

                    if (string.IsNullOrEmpty(startDate) == true || string.IsNullOrEmpty(endDate) || string.IsNullOrEmpty(strValue))
                    {
                        requiredStartEnd.Visible = true;
                        requiredStartEnd.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        //get form input to list 
                        DateTime st = Convert.ToDateTime(startDate);
                        DateTime en = Convert.ToDateTime(endDate);
                        List<string> allDates = new List<string>();
                        List<string> compareDatesDB = new List<string>();

                        for (DateTime date = st.Date; date <= en.Date; date = date.AddDays(1))
                        {
                            allDates.Add(Convert.ToString(date.Date));
                        }

                        fillCalendarData(Convert.ToInt32(employeeID)); //just to find db date

                        // check if ds has a table
                        if (ds.Tables.Count > 0)
                        {
                            //check if ds.table[0] has any rows  
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    if ((dr["startDate"].ToString() != DBNull.Value.ToString()) &&
                                        (dr["endDate"].ToString() != DBNull.Value.ToString()))
                                    {
                                        DateTime dtEvent = Convert.ToDateTime(dr["startDate"]);
                                        DateTime dtEvent1 = Convert.ToDateTime(dr["endDate"]);

                                        for (DateTime compare = dtEvent.Date; compare <= dtEvent1.Date; compare = compare.AddDays(1))
                                        {
                                            compareDatesDB.Add(Convert.ToString(compare.Date));
                                        }
                                    }
                                }
                            }
                        }
                        bool result = false;
                        if (allDates.Any(compareDatesDB.Contains))
                        {
                            result = true;
                        }

                        if (result == false)
                        {
                            if (l.getLeaveCount(Convert.ToInt32(employeeID), DdlTypeLeave.SelectedValue).leaveAmt < int.Parse(strValue))
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script language='javascript'>");
                                sb.Append(@"$('#fails').modal('show');");
                                sb.Append(@"</script>");
                                Label1.Text = "Error";
                                label2.Text = "You do not have enough leave";
                                ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                            }
                            else if (Convert.ToInt32(strValue) <= 0)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script language='javascript'>");
                                sb.Append(@"$('#fails').modal('show');");
                                sb.Append(@"</script>");
                                Label1.Text = "Error";
                                label2.Text = "Number of days for leave must be at least one day";
                                ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                            }
                            else
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script language='javascript'>");
                                sb.Append(@"$('#Submitmodal').modal('show');");
                                sb.Append(@"</script>");

                                Leave detail = new Leave();
                                detail.leaveType = leaveType;
                                detail.employeeID = int.Parse(tbEmpId.Text);
                                detail.duration = int.Parse(strValue);
                                detail.endDate = endDate;
                                detail.startDate = startDate;
                                detail.leaveStatus = "pending";
                                detail.reviewBy = 45;
                                detail.rejectedReason = "No Comments";
                                Session["Data"] = detail;

                                id.Text = tbEmpId.Text;
                                type.Text = leaveType;
                                start.Text = startDate;
                                start.Text = startDate;
                                end.Text = endDate;
                                duration.Text = strValue;
                                ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                            }
                        }
                        else if (result == true)
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script language='javascript'>");
                            sb.Append(@"$('#fails').modal('show');");
                            sb.Append(@"</script>");
                            Label1.Text = "Error";
                            label2.Text = "Date/(s) has been selected before.";
                            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                        }
                    }
                }
            }
        }



        protected void clear_Click(object sender, EventArgs e)
        {
            Response.Redirect("Leave_addLeave.aspx");
        }
    }
}