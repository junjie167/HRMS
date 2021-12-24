using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HRMS.DAL
{
    public class ManagerClaim
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;

        private int claimId;
        private int employeeId;
        private float amount;
        private string title;
        private string description;
        private string attachment;
        private string claimType;
        private string status;
        private string dateFrom;
        private string dateTo;
        private string claimOption;
        private string submittedBy;

        public ManagerClaim() { }

        public ManagerClaim(int claimID, string claimType, string status, float amount, string title, string submittedBy)
        {
            this.claimId = claimID;
            this.claimType = claimType;
            this.status = status;
            this.amount = amount;
            this.title = title;
            this.submittedBy = submittedBy;
        }

        public ManagerClaim(string fromdate, string todate, string claimtype, string description, float amount, string attachment, string claimOption, string title, string status)
        {
            this.dateFrom = fromdate;
            this.dateTo = todate;
            this.claimType = claimtype;
            this.description = description;
            this.amount = amount;
            this.attachment = attachment;
            this.ClaimOption = claimOption;
            this.title = title;
            this.status = status;
        }

        public string DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }

        public string DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string ClaimType
        {
            get { return claimType; }
            set { claimType = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Attachment
        {
            get { return attachment; }
            set { attachment = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        public int ClaimId
        {
            get { return claimId; }
            set { claimId = value; }
        }

        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string ClaimOption { get => claimOption; set => claimOption = value; }
        public string SubmittedBy { get => submittedBy; set => submittedBy = value; }

        public List<ManagerClaim> retreiveClaimforReview(int ManagerID)
        {
            List<ManagerClaim> managerClaimsList = new List<ManagerClaim>();

            int claimID;
            string status;
            string claimtype = "";
            string title = "";
            float amount = 0.0f;
            int employeeID = 0;
            string name = "";



            string queryStr = "SELECT * FROM Claims WHERE employeeID != @paraemployeeID  ORDER BY claimsID DESC";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@paraemployeeID", ManagerID);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                claimID = int.Parse(dr["claimsID"].ToString());
                status = dr["status"].ToString();
                employeeID = int.Parse(dr["employeeID"].ToString());

                string queryStr3 = "SELECT employeeName FROM Employee WHERE employeeID =@paraemployeeID";
                SqlConnection conn3 = new SqlConnection(connStr);
                SqlCommand cmd3 = new SqlCommand(queryStr3, conn3);

                cmd3.Parameters.AddWithValue("@paraemployeeID", employeeID);

                

                conn3.Open();
                SqlDataReader dr3 = cmd3.ExecuteReader();

                while (dr3.Read())
                {
                    submittedBy = dr3["employeeName"].ToString();
                }

                string queryStr1 = "SELECT * FROM SingleClaim WHERE claimsID = @paraclaimid";
                SqlConnection conn1 = new SqlConnection(connStr);
                SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);

                cmd1.Parameters.AddWithValue("@paraclaimid", claimID);

                conn1.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        title = "";
                        claimtype = dr1["claimType"].ToString();
                        amount = float.Parse(dr1["amount"].ToString());
                    }

                    conn1.Close();
                    dr1.Close();
                    dr1.Dispose();
                }
                else
                {
                    string queryStr2 = "SELECT * FROM GroupClaim WHERE claimsID = @paraclaimid";
                    SqlConnection conn2 = new SqlConnection(connStr);
                    SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);

                    cmd2.Parameters.AddWithValue("@paraclaimid", claimID);

                    conn2.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    while (dr2.Read())
                    {
                        claimtype = "";
                        title = dr2["title"].ToString();
                        amount += float.Parse(dr2["amount"].ToString());
                    }

                    conn2.Close();
                    dr2.Close();
                    dr2.Dispose();
                }

                ManagerClaim managerClaim = new ManagerClaim(claimID, claimtype, status, amount, title, submittedBy);
                managerClaimsList.Add(managerClaim);

            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return managerClaimsList;


        }


        public List<ManagerClaim> OldestClaimforReview(int ManagerID)
        {
            List<ManagerClaim> managerClaimsList = new List<ManagerClaim>();

            int claimID;
            string status;
            string claimtype = "";
            string title = "";
            float amount = 0.0f;



            string queryStr = "SELECT * FROM Claims WHERE employeeID != @paraemployeeID ORDER BY claimsID ASC";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@paraemployeeID", ManagerID);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                claimID = int.Parse(dr["claimsID"].ToString());
                status = dr["status"].ToString();

                string queryStr1 = "SELECT * FROM SingleClaim WHERE claimsID = @paraclaimid";
                SqlConnection conn1 = new SqlConnection(connStr);
                SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);

                cmd1.Parameters.AddWithValue("@paraclaimid", claimID);

                conn1.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        claimtype = "";
                        title = "";
                        claimtype = dr1["claimType"].ToString();
                        amount = float.Parse(dr1["amount"].ToString());
                    }

                    conn1.Close();
                    dr1.Close();
                    dr1.Dispose();
                }
                else
                {
                    string queryStr2 = "SELECT * FROM GroupClaim WHERE claimsID = @paraclaimid";
                    SqlConnection conn2 = new SqlConnection(connStr);
                    SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);

                    cmd2.Parameters.AddWithValue("@paraclaimid", claimID);

                    conn2.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    while (dr2.Read())
                    {
                        claimtype = "";
                        title = "";
                        title = dr2["title"].ToString();
                        amount += float.Parse(dr2["amount"].ToString());
                    }

                    conn2.Close();
                    dr2.Close();
                    dr2.Dispose();
                }

                ManagerClaim managerClaim = new ManagerClaim(claimID, claimtype, status, amount, title, submittedBy);
                managerClaimsList.Add(managerClaim);

            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return managerClaimsList;

        }

        public List<ManagerClaim> retrieveViewClaim(int claimID)
        {
            List<ManagerClaim> managerClaimsList = new List<ManagerClaim>();

            string fromdate = "";
            string todate = "";
            string description = "";
            string attachment = "";
            string claimtype = "";
            string title = "";
            float amount = 0.0f;
            string claimOption = "";
            string status = "";

            string queryStr1 = "SELECT * FROM Claims WHERE claimsID = @paraclaimid";
            SqlConnection conn1 = new SqlConnection(connStr);
            SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);

            cmd1.Parameters.AddWithValue("@paraclaimid", claimID);

            conn1.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();

            while (dr1.Read())
            {
                fromdate = dr1["fromDate"].ToString();
                todate = dr1["toDate"].ToString();
                status = dr1["status"].ToString();
            }

            string queryStr = "SELECT * FROM SingleClaim WHERE claimsID = @paraclaimid";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@paraclaimid", claimID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    claimtype = dr["claimType"].ToString();
                    description = dr["description"].ToString();
                    amount = float.Parse(dr["amount"].ToString());
                    attachment = dr["attachment"].ToString();
                    claimOption = "Single";
                    

                    ManagerClaim managerClaim = new ManagerClaim(fromdate, todate, claimtype, description, amount, attachment, claimOption, title, status);
                    managerClaimsList.Add(managerClaim);
                }
            }
            else
            {
                string queryStr2 = "SELECT * FROM GroupClaim WHERE claimsID = @paraclaimid";
                SqlConnection conn2 = new SqlConnection(connStr);
                SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);

                cmd2.Parameters.AddWithValue("@paraclaimid", claimID);

                conn2.Open();
                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    claimtype = dr2["claimType"].ToString();
                    title = dr2["title"].ToString();
                    description = dr2["description"].ToString();
                    amount = float.Parse(dr2["amount"].ToString());
                    attachment = dr2["attachment"].ToString();
                    claimOption = "Group";

                    ManagerClaim managerClaim = new ManagerClaim(fromdate, todate, claimtype, description, amount, attachment, claimOption, title, status);
                    managerClaimsList.Add(managerClaim);
                }

                conn2.Close();
                dr2.Close();
                dr2.Dispose();
            }


            conn.Close();
            dr.Close();
            dr.Dispose();

            return managerClaimsList;
        }

        public int updateStatusReject(int employeeID, int claimID, string Reasonrejection)
        {
            int result = 0;

            StringBuilder sqlcomm = new StringBuilder();

            sqlcomm.AppendLine("UPDATE Claims SET status= @parastatus, reviewBy= @parareviewby, rejectedReason= @parareason");
            sqlcomm.AppendLine("WHERE  claimsID= @paraclaimid");

            try
            {
                SqlConnection myConn = new SqlConnection(connStr);
                SqlCommand sqlCmd = new SqlCommand(sqlcomm.ToString(), myConn);

                sqlCmd.Parameters.AddWithValue("@paraclaimid", claimID);
                sqlCmd.Parameters.AddWithValue("@parastatus", "Rejected");
                sqlCmd.Parameters.AddWithValue("@parareviewby", employeeID);
                sqlCmd.Parameters.AddWithValue("@parareason", Reasonrejection);

                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();

                myConn.Close();

            }
            catch (Exception e)
            {
                result = 0;
            }

            return result;
        }

        public int updateStatusApprove(int employeeID, int claimID)
        {
            int result = 0;

            StringBuilder sqlcomm = new StringBuilder();

            sqlcomm.AppendLine("UPDATE Claims SET status= @parastatus, reviewBy= @parareviewby");
            sqlcomm.AppendLine("WHERE  claimsID= @paraclaimid");

            try
            {
                SqlConnection myConn = new SqlConnection(connStr);
                SqlCommand sqlCmd = new SqlCommand(sqlcomm.ToString(), myConn);

                sqlCmd.Parameters.AddWithValue("@paraclaimid", claimID);
                sqlCmd.Parameters.AddWithValue("@parastatus", "Approved");
                sqlCmd.Parameters.AddWithValue("@parareviewby", employeeID);
               

                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();

                myConn.Close();

            }
            catch (Exception e)
            {
                result = 0;
            }

            return result;
        }

        public List<ManagerClaim> filterClaimReviewByStatus(int ManagerID, string statusDropdown, string sort)
        {
            List<ManagerClaim> managerClaimsList = new List<ManagerClaim>();

            int claimID;
            string claimtype = "";
            string title = "";
            float amount = 0.0f;
            int employeeID = 0;
            string name = "";

            if (statusDropdown != "All" && sort == "Latest")
            {
                string queryStr = "SELECT * FROM Claims WHERE employeeID != @paraemployeeID AND status =@parastatus  ORDER BY claimsID DESC";
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);

                cmd.Parameters.AddWithValue("@paraemployeeID", ManagerID);
                cmd.Parameters.AddWithValue("@parastatus", statusDropdown);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    claimID = int.Parse(dr["claimsID"].ToString());
                    status = dr["status"].ToString();
                    employeeID = int.Parse(dr["employeeID"].ToString());

                    string queryStr3 = "SELECT employeeName FROM Employee WHERE employeeID =@paraemployeeID";
                    SqlConnection conn3 = new SqlConnection(connStr);
                    SqlCommand cmd3 = new SqlCommand(queryStr3, conn3);

                    cmd3.Parameters.AddWithValue("@paraemployeeID", employeeID);



                    conn3.Open();
                    SqlDataReader dr3 = cmd3.ExecuteReader();

                    while (dr3.Read())
                    {
                        submittedBy = dr3["employeeName"].ToString();
                    }

                    string queryStr1 = "SELECT * FROM SingleClaim WHERE claimsID = @paraclaimid";
                    SqlConnection conn1 = new SqlConnection(connStr);
                    SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);

                    cmd1.Parameters.AddWithValue("@paraclaimid", claimID);

                    conn1.Open();
                    SqlDataReader dr1 = cmd1.ExecuteReader();

                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            title = "";
                            claimtype = dr1["claimType"].ToString();
                            amount = float.Parse(dr1["amount"].ToString());
                        }

                        conn1.Close();
                        dr1.Close();
                        dr1.Dispose();
                    }
                    else
                    {
                        string queryStr2 = "SELECT * FROM GroupClaim WHERE claimsID = @paraclaimid";
                        SqlConnection conn2 = new SqlConnection(connStr);
                        SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);

                        cmd2.Parameters.AddWithValue("@paraclaimid", claimID);

                        conn2.Open();
                        SqlDataReader dr2 = cmd2.ExecuteReader();

                        while (dr2.Read())
                        {
                            claimtype = "";
                            title = dr2["title"].ToString();
                            amount += float.Parse(dr2["amount"].ToString());
                        }

                        conn2.Close();
                        dr2.Close();
                        dr2.Dispose();
                    }

                    ManagerClaim managerClaim = new ManagerClaim(claimID, claimtype, status, amount, title, submittedBy);
                    managerClaimsList.Add(managerClaim);

                }
            }
            else if(statusDropdown != "All")
            {
                string queryStr = "SELECT * FROM Claims WHERE employeeID != @paraemployeeID AND status =@parastatus  ORDER BY claimsID ASC";
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);

                cmd.Parameters.AddWithValue("@paraemployeeID", ManagerID);
                cmd.Parameters.AddWithValue("@parastatus", statusDropdown);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    claimID = int.Parse(dr["claimsID"].ToString());
                    status = dr["status"].ToString();
                    employeeID = int.Parse(dr["employeeID"].ToString());

                    string queryStr3 = "SELECT employeeName FROM Employee WHERE employeeID =@paraemployeeID";
                    SqlConnection conn3 = new SqlConnection(connStr);
                    SqlCommand cmd3 = new SqlCommand(queryStr3, conn3);

                    cmd3.Parameters.AddWithValue("@paraemployeeID", employeeID);



                    conn3.Open();
                    SqlDataReader dr3 = cmd3.ExecuteReader();

                    while (dr3.Read())
                    {
                        submittedBy = dr3["employeeName"].ToString();
                    }

                    string queryStr1 = "SELECT * FROM SingleClaim WHERE claimsID = @paraclaimid";
                    SqlConnection conn1 = new SqlConnection(connStr);
                    SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);

                    cmd1.Parameters.AddWithValue("@paraclaimid", claimID);

                    conn1.Open();
                    SqlDataReader dr1 = cmd1.ExecuteReader();

                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            title = "";
                            claimtype = dr1["claimType"].ToString();
                            amount = float.Parse(dr1["amount"].ToString());
                        }

                        conn1.Close();
                        dr1.Close();
                        dr1.Dispose();
                    }
                    else
                    {
                        string queryStr2 = "SELECT * FROM GroupClaim WHERE claimsID = @paraclaimid";
                        SqlConnection conn2 = new SqlConnection(connStr);
                        SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);

                        cmd2.Parameters.AddWithValue("@paraclaimid", claimID);

                        conn2.Open();
                        SqlDataReader dr2 = cmd2.ExecuteReader();

                        while (dr2.Read())
                        {
                            claimtype = "";
                            title = dr2["title"].ToString();
                            amount += float.Parse(dr2["amount"].ToString());
                        }

                        conn2.Close();
                        dr2.Close();
                        dr2.Dispose();
                    }

                    ManagerClaim managerClaim = new ManagerClaim(claimID, claimtype, status, amount, title, submittedBy);
                    managerClaimsList.Add(managerClaim);

                }
            }else if (statusDropdown == "All" && sort == "Oldest")
            {
                managerClaimsList = OldestClaimforReview(ManagerID);
            }
            else
            {
                managerClaimsList = retreiveClaimforReview(ManagerID);
            }

            return managerClaimsList;
        }
    }
}