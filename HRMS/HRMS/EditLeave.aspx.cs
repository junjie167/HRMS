using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HRMS.DAL;
namespace HRMS
{
    public partial class EditLeave : System.Web.UI.Page
    {
        Leave leaves = null;
        protected DataTable dt;
        protected DataRowCollection drc;
        DataSet ds = new DataSet();
        protected string getsstartdate { get; set; }
        protected string getenddate { get; set; }
        protected string getdduration { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string employeeID = Session["ID"].ToString();
            tbEmpId.Text = employeeID;
            fillCalendarData(Convert.ToInt32(employeeID));
            if (!IsPostBack)
            {
                getCurrentDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                Leave aleave = new Leave();
                int leaveId = Convert.ToInt32(Session["leaveId"].ToString());

                leaves = aleave.getLeaves(leaveId);

                getid.Text = leaves.leaveID.ToString();
                ddlvalue.Text = leaves.leaveType.ToString();
                getsstartdate = leaves.startDate.ToString();
                getenddate = leaves.endDate.ToString();
                this.getdduration = leaves.duration.ToString(); //get the duration for the specific leave id 

                pastQuota.Text = getdduration;
                if (ddlvalue.Text == "Annual")
                {
                    getremaining.Text = aleave.getLeaveCount(Convert.ToInt32(employeeID), "Annual").leaveAmt.ToString();
                    r2.Text = "Annual:";
                    if (aleave.getLeaveCount(Convert.ToInt32(employeeID), "Annual").leaveAmt <= 0)
                    {
                        getremaining.ForeColor = System.Drawing.Color.Red;
                        r1.ForeColor = System.Drawing.Color.Red;
                        r2.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        getremaining.ForeColor = System.Drawing.Color.Green;
                        r1.ForeColor = System.Drawing.Color.Green;
                        r2.ForeColor = System.Drawing.Color.Green;
                    }
                }
                if (ddlvalue.Text == "Medical")
                {
                    getremaining.Text = aleave.getLeaveCount(Convert.ToInt32(employeeID), "Medical").leaveAmt.ToString();
                    r2.Text = "Medical:";
                    if (aleave.getLeaveCount(Convert.ToInt32(employeeID), "Medical").leaveAmt <= 0)
                    {
                        getremaining.ForeColor = System.Drawing.Color.Red;
                        r1.ForeColor = System.Drawing.Color.Red;
                        r2.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        getremaining.ForeColor = System.Drawing.Color.Green;
                        r1.ForeColor = System.Drawing.Color.Green;
                        r2.ForeColor = System.Drawing.Color.Green;

                    }

                }
                if (ddlvalue.Text == "Childcare")
                {
                    getremaining.Text = aleave.getLeaveCount(Convert.ToInt32(employeeID), "Childcare").leaveAmt.ToString();
                    r2.Text = "Childcare:";
                    if (aleave.getLeaveCount(Convert.ToInt32(employeeID), "Childcare").leaveAmt <= 0)
                    {
                        getremaining.ForeColor = System.Drawing.Color.Red;
                        r1.ForeColor = System.Drawing.Color.Red;
                        r2.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        getremaining.ForeColor = System.Drawing.Color.Green;
                        r1.ForeColor = System.Drawing.Color.Green;
                        r2.ForeColor = System.Drawing.Color.Green;

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
                                e.Cell.BackColor = ColorTranslator.FromHtml("#FBEFE4");
                                if (type == "Medical")
                                {
                                    lit.Text = "<br/>" + 'M';
                                    e.Cell.Controls.Add(lit);
                                    
                                }
                                if(type == "Annual")
                                {
                                    lit.Text = "<br/>" + 'A';
                                    e.Cell.Controls.Add(lit);
                                }
                                if(type == "Childcare")
                                {
                                    lit.Text = "<br/>" + 'C';
                                    e.Cell.Controls.Add(lit);
                                }
                                
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

        protected void clear_Click(object sender, EventArgs e)
        {
            Leave aleave = new Leave();
            int leaveId = Convert.ToInt32(Session["leaveId"].ToString());
            leaves = aleave.getLeaves(leaveId);

            getid.Text = leaves.leaveID.ToString();
            ddlvalue.Text = leaves.leaveType.ToString();
            getsstartdate = leaves.startDate.ToString();
            getenddate = leaves.endDate.ToString();
            this.getdduration = leaves.duration.ToString();
        }

        protected void updating(Object sender, CommandEventArgs e) //when click on confirmation
        {
            if (e.CommandName == "updating")
            {
                Leave aleave = new Leave();
                aleave.updatequotaAdd(Convert.ToInt32(tbEmpId.Text), type.Text, Convert.ToInt32(pastQuota.Text));
                aleave.updatequota(Convert.ToInt32(tbEmpId.Text), type.Text, Convert.ToInt32(duration.Text));

                if (Convert.ToInt32(aleave.duration.ToString()) < 0)
                {
                   // System.Diagnostics.Debug.WriteLine("error");
                    aleave.updatequotaAdd(Convert.ToInt32(tbEmpId.Text), type.Text, Convert.ToInt32(duration.Text));
                    aleave.updatequota(Convert.ToInt32(tbEmpId.Text), type.Text, Convert.ToInt32(pastQuota.Text));
                }
                else
                {
                    int result = 0;
                    int prodId = Convert.ToInt32(Session["leaveId"].ToString());

                    result = aleave.LeaveUpdate(prodId, start.Text, end.Text, duration.Text);

                    if (result > 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"$('#statuspopup').modal('show');");
                        sb.Append(@"</script>");
                        Label6.Text = "Successful";
                        label7.Text = "Leave updated successfully";
                        ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                    }
                    else
                    {
                        Response.Write("<script>alert('update unsuccessful'); window.location='EditLeave.aspx';</script>");
                    }
                }
            }
        }

        protected void changeback(object sender, CommandEventArgs e)
        {
            int prodId = Convert.ToInt32(Session["leaveId"].ToString());
            Response.Redirect("EditLeave.aspx?leaveId= " + prodId);

            getid.Text = leaves.leaveID.ToString();
            ddlvalue.Text = leaves.leaveType.ToString();
            getsstartdate = leaves.startDate.ToString();
            getenddate = leaves.endDate.ToString();
            this.getdduration = leaves.duration.ToString();
        }

        protected void update_Click(object sender, EventArgs e)
        {
            Leave leave = new Leave();
            Leave aleave = new Leave();

            int prodId = Convert.ToInt32(Session["leaveId"].ToString());
            string startDate = Page.Request.Form["pickup_date"].ToString();
            string endDate = Page.Request.Form["dropoff_date"].ToString();
            string strValue = Page.Request.Form["numdays"].ToString();

            leave = aleave.getLeaves(prodId);

            DateTime st = Convert.ToDateTime(startDate);
            DateTime en = Convert.ToDateTime(endDate);
            List<string> allDates = new List<string>();
            List<string> compareDatesDB = new List<string>();

            for (DateTime date = st.Date; date <= en.Date; date = date.AddDays(1))
            {
                allDates.Add(Convert.ToString(date.Date)); //correct
            }

            string employeeID = Session["ID"].ToString();

            DataSet dm = new DataSet();
            string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connStr);
            string query = "SELECT startDate, endDate FROM Leave WHERE employeeID=@ID and leaveID=@leaveID";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(employeeID);
            cmd.Parameters.Add("@leaveID", SqlDbType.Int).Value = prodId;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

            dataAdapter.Fill(dm, "Table");

            // check if ds has a table
            if (dm.Tables.Count > 0)
            {
                //check if ds.table[0] has any rows  
                if (dm.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dm.Tables[0].Rows)
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
                result = true; //part of its own leave
            }

            if (result == true) //if part of its own leave
            {
                DataSet dp1 = new DataSet();
                List<string> compareDatesDB2 = new List<string>();

                string connStr2 = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection sqlConn2 = new SqlConnection(connStr2);
                string query2 = "SELECT startDate, endDate FROM Leave WHERE employeeID=@ID1 and NOT LeaveID=@lid1";
                SqlCommand cmd2 = new SqlCommand(query2, sqlConn2);
                cmd2.Parameters.Add("@ID1", SqlDbType.Int).Value = Convert.ToInt32(employeeID);
                cmd2.Parameters.Add("@lid1", SqlDbType.Int).Value = prodId;
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd2);

                dataAdapter1.Fill(dp1, "Table");


                if (dp1.Tables.Count > 0)
                {
                    //check if ds.table[0] has any rows  
                    if (dp1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dp1.Tables[0].Rows)
                        {
                            if ((dr2["startDate"].ToString() != DBNull.Value.ToString()) &&
                                (dr2["endDate"].ToString() != DBNull.Value.ToString()))
                            {
                                DateTime dtEvent = Convert.ToDateTime(dr2["startDate"]);
                                DateTime dtEvent1 = Convert.ToDateTime(dr2["endDate"]);

                                for (DateTime compare = dtEvent.Date; compare <= dtEvent1.Date; compare = compare.AddDays(1))
                                {
                                    compareDatesDB2.Add(Convert.ToString(compare.Date));
                                }
                            }
                        }
                    }
                }

                if (allDates.Any(compareDatesDB2.Contains))
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"$('#fails').modal('show');");
                    sb.Append(@"</script>");
                    Label1.Text = "Error";
                    label2.Text = "Date/(s) has been selected before.";
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());


                    int leaveId = Convert.ToInt32(Session["leaveId"].ToString());
                    leaves = aleave.getLeaves(leaveId);

                    getid.Text = leaves.leaveID.ToString();
                    ddlvalue.Text = leaves.leaveType.ToString();
                    getsstartdate = leaves.startDate.ToString();
                    getenddate = leaves.endDate.ToString();
                    this.getdduration = leaves.duration.ToString();
                }
                else
                {
                    int leaveId1 = Convert.ToInt32(Session["leaveId"].ToString());
                    leaves = aleave.getLeaves(leaveId1);

                    int compare = Convert.ToInt32(getremaining.Text) + Convert.ToInt32(leaves.duration.ToString()) - Convert.ToInt32(strValue);
                    if (compare < 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"$('#fails').modal('show');");
                        sb.Append(@"</script>");
                        Label1.Text = "Error";
                        label2.Text = "You do not have enough leave";
                        ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                        int leaveId = Convert.ToInt32(Session["leaveId"].ToString());
                        leaves = aleave.getLeaves(leaveId);

                        getid.Text = leaves.leaveID.ToString();
                        ddlvalue.Text = leaves.leaveType.ToString();
                        getsstartdate = leaves.startDate.ToString();
                        getenddate = leaves.endDate.ToString();
                        this.getdduration = leaves.duration.ToString();
                    }
                    if (int.Parse(strValue) <= 0)
                    {
                        
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"$('#fails').modal('show');");
                        sb.Append(@"</script>");
                        Label1.Text = "Error";
                        label2.Text = "Number of days for leave must be at least one day";
                        ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                        int leaveId = Convert.ToInt32(Session["leaveId"].ToString());
                        leaves = aleave.getLeaves(leaveId);

                        getid.Text = leaves.leaveID.ToString();
                        ddlvalue.Text = leaves.leaveType.ToString();
                        getsstartdate = leaves.startDate.ToString();
                        getenddate = leaves.endDate.ToString();
                        this.getdduration = leaves.duration.ToString();
                    }
                    else
                    {
                        //trigger confirmation
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"$('#Submitmodal').modal('show');");
                        sb.Append(@"</script>");

                        id.Text = prodId.ToString();
                        type.Text = leave.leaveType;
                        start.Text = startDate;
                        end.Text = endDate;
                        duration.Text = strValue;

                        ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                    }
                }

                
            }
            else if (result == false) //not part of its own leave
            {

                DataSet dp = new DataSet();
                List<string> compareDatesDB1 = new List<string>();

                string connStr1 = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection sqlConn1 = new SqlConnection(connStr1);
                string query1 = "SELECT startDate, endDate FROM Leave WHERE employeeID=@ID1 and NOT LeaveID=@lid1";
                SqlCommand cmd1 = new SqlCommand(query1, sqlConn1);
                cmd1.Parameters.Add("@ID1", SqlDbType.Int).Value = Convert.ToInt32(employeeID);
                cmd1.Parameters.Add("@lid1", SqlDbType.Int).Value = prodId;
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);

                dataAdapter1.Fill(dp, "Table");


                // check if ds has a table
                if (dp.Tables.Count > 0)
                {
                    //check if ds.table[0] has any rows  
                    if (dp.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dp.Tables[0].Rows)
                        {
                            if ((dr1["startDate"].ToString() != DBNull.Value.ToString()) &&
                                (dr1["endDate"].ToString() != DBNull.Value.ToString()))
                            {
                                DateTime dtEvent = Convert.ToDateTime(dr1["startDate"]);
                                DateTime dtEvent1 = Convert.ToDateTime(dr1["endDate"]);

                                for (DateTime compare = dtEvent.Date; compare <= dtEvent1.Date; compare = compare.AddDays(1))
                                {
                                    compareDatesDB1.Add(Convert.ToString(compare.Date));
                                }
                            }
                        }
                    }
                }

                if (allDates.Any(compareDatesDB1.Contains)) //if contain from db
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"$('#fails').modal('show');");
                    sb.Append(@"</script>");
                    Label1.Text = "Error";
                    label2.Text = "Date/(s) has been selected before.";
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());


                    int leaveId = Convert.ToInt32(Session["leaveId"].ToString());
                    leaves = aleave.getLeaves(leaveId);

                    getid.Text = leaves.leaveID.ToString();
                    ddlvalue.Text = leaves.leaveType.ToString();
                    getsstartdate = leaves.startDate.ToString();
                    getenddate = leaves.endDate.ToString();
                    this.getdduration = leaves.duration.ToString();
                }
                else
                {
                    int leaveId1 = Convert.ToInt32(Session["leaveId"].ToString());
                    leaves = aleave.getLeaves(leaveId1);

                    int compare = Convert.ToInt32(getremaining.Text) + Convert.ToInt32(leaves.duration.ToString()) - Convert.ToInt32(strValue);

                    if (compare < 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"$('#fails').modal('show');");
                        sb.Append(@"</script>");
                        Label1.Text = "Error";
                        label2.Text = "You do not have enough leave";
                        ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                        int leaveId = Convert.ToInt32(Session["leaveId"].ToString());
                        leaves = aleave.getLeaves(leaveId);

                        getid.Text = leaves.leaveID.ToString();
                        ddlvalue.Text = leaves.leaveType.ToString();
                        getsstartdate = leaves.startDate.ToString();
                        getenddate = leaves.endDate.ToString();
                        this.getdduration = leaves.duration.ToString();
                    }
                    if (int.Parse(strValue) <= 0)
                    {

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"$('#fails').modal('show');");
                        sb.Append(@"</script>");
                        Label1.Text = "Error";
                        label2.Text = "Number of days for leave must be at least one day";
                        ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                        int leaveId = Convert.ToInt32(Session["leaveId"].ToString());
                        leaves = aleave.getLeaves(leaveId);

                        getid.Text = leaves.leaveID.ToString();
                        ddlvalue.Text = leaves.leaveType.ToString();
                        getsstartdate = leaves.startDate.ToString();
                        getenddate = leaves.endDate.ToString();
                        this.getdduration = leaves.duration.ToString();
                    }
                    else
                    {
                        //trigger confirmation
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"$('#Submitmodal').modal('show');");
                        sb.Append(@"</script>");

                        id.Text = prodId.ToString();
                        type.Text = leave.leaveType;
                        start.Text = startDate;
                        end.Text = endDate;
                        duration.Text = strValue;

                        ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                    }
                }

            }


        }

    }
}