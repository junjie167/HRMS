using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace HRMS.DAL
{
    
    public class GroupClaim
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

        public GroupClaim()
        {

        }

        public GroupClaim(int employeeId, string dateFrom, string dateTo, string status)
        {
            this.employeeId = employeeId;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.status = status;
        }

        public GroupClaim(int claimId, string claimType, string title, string description, string attachment, float amount)
        {
            this.claimId = claimId;
            this.claimType = claimType;
            this.title = title;
            this.description = description;
            this.attachment = attachment;
            this.amount = amount;
        }

        public GroupClaim(int claimId, string dateFrom, string dateTo)
        {
            this.claimId = claimId; 
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
        }

        public GroupClaim(int claimId, string title, string status, float amount)
        {
            this.claimId = claimId;
            this.title = title;
            this.status = status;
            this.amount = amount;
        }

        public GroupClaim(int claimId, float amount, string claimType, string description)
        {
            this.claimId = claimId;
            this.amount = amount;
            this.claimType = claimType;
            this.description = description;
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

        public int groupClaimInsert()
        {
            int result = 0;
            string queryStr = "INSERT INTO GroupClaim(claimsID, claimType, title, description, attachment, amount)" + "values (@claimId, @claimType, @title, @description, @attachment, @amount)";
            //try
            //{
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@claimId", this.claimId);
            cmd.Parameters.AddWithValue("@claimType", this.claimType);
            cmd.Parameters.AddWithValue("@title", this.title);
            cmd.Parameters.AddWithValue("@description", this.description);
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@attachment", this.attachment);

            conn.Open();
            result += cmd.ExecuteNonQuery(); // Returns no. of rows affected. Must be > 0
            conn.Close();
            return result;
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
        }

        public int GetClaimId()
        {
            int claimIdVal = 0;
            string id;
            string queryStr = "SELECT claimsID FROM Claims ORDER BY claimsID DESC";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            if (dr.Read())
            {
                id = dr["claimsID"].ToString();
                claimIdVal = Int32.Parse(id);
            }

                conn.Close();

                dr.Close();
                dr.Dispose();

                return claimIdVal;
            
        }

        public int claimInsert()
        {
            int result = 0;
            string queryStr = "INSERT INTO Claims(employeeID, fromDate, toDate, status)" + "values (@employeeId, @dateFrom, @dateTo, @status)";
            //try
            //{
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@employeeId", this.employeeId);
            cmd.Parameters.AddWithValue("@dateFrom", this.dateFrom);
            cmd.Parameters.AddWithValue("@dateTo", this.dateTo);
            cmd.Parameters.AddWithValue("@status", this.status);

            conn.Open();
            result += cmd.ExecuteNonQuery(); // Returns no. of rows affected. Must be > 0
            conn.Close();
            return result;
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
        }

        public DataSet getClaimDetails(int claimId)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet questsData;

            SqlConnection conn = new SqlConnection(connStr);
            questsData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM GroupClaim WHERE claimsID = '" + claimId + "'");

            da = new SqlDataAdapter(sql.ToString(), conn);
            da.Fill(questsData);

            conn.Close();


            return questsData;
        }


        public string GetDateFrom(int claimId)
        {
            string date = "";
            string queryStr = "SELECT fromDate FROM Claims WHERE claimsID = '"+ claimId +"'";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                    date = dr["fromDate"].ToString();
                

            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return date;

        }

        public string GetDateTo(int claimId)
        {
            string date = "";
            string queryStr = "SELECT toDate FROM Claims WHERE claimsID = '" + claimId + "'";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                date = dr["toDate"].ToString();


            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return date;

        }

        public string GetGTitle(int claimId)
        {
            string title = "";
            string queryStr = "SELECT title FROM GroupClaim WHERE claimsID = '" + claimId + "'";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                title = dr["title"].ToString();


            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return title;

        }

        public int UpdateClaim(int claimId, string dateTo, string dateFrom)
        {
            string queryStr = "UPDATE Claims SET " + "fromDate = @datefrom ," + "toDate = @dateTo " + " WHERE claimsID = @claimId";
            int nofRow = 0;

            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@claimId", claimId);
                cmd.Parameters.AddWithValue("@dateTo", dateTo);
                cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                conn.Open();

                nofRow = cmd.ExecuteNonQuery();
                conn.Close();
                return nofRow;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int claimDelete(int cliamId)
        {
            int nofRow = 0;
            string queryStr = "DELETE FROM GroupClaim WHERE claimsID=@cliamId";

            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@cliamId", cliamId);
                conn.Open();

                nofRow = cmd.ExecuteNonQuery();
                //cmd.ExecuteNonQuery();

                conn.Close();
                return nofRow;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public List<GroupClaim> GetGroupClaims(int employeeID)
        {
            List<GroupClaim> gClaimsList = new List<GroupClaim>();

            int claimId;
            string title, status;
            float amt;


            string queryStr = "SELECT status, claimsID FROM Claims WHERE employeeID = @employeeid";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@employeeid", employeeID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                claimId = int.Parse(dr["claimsID"].ToString());
                
                status = dr["status"].ToString();

                string queryStr1 = "SELECT SUM(amount) as total, title FROM GroupClaim WHERE claimsID = @claimID Group BY title";
                SqlConnection conn1 = new SqlConnection(connStr);
                SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);

                cmd1.Parameters.AddWithValue("@claimID", claimId);

                conn1.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();

                while (dr1.Read())
                {
                    amt = float.Parse(dr1["total"].ToString());
                    title = dr1["title"].ToString();
                    GroupClaim c = new GroupClaim(claimId, title, status, amt);
                    gClaimsList.Add(c);
                    break;
                }
                conn1.Close();
                dr1.Close();
                dr1.Dispose();
            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return gClaimsList;
        }


        public List<GroupClaim> GetGroupClaimsDetails(int claimId)
        {
            List<GroupClaim> gClaimsList = new List<GroupClaim>();

            string claimType, description;
            float amount;


            string queryStr = "SELECT claimType, description, amount FROM GroupClaim WHERE claimsID = @claimId";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@claimId", claimId);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                claimType = dr["claimType"].ToString();
                description = dr["description"].ToString();
                amount = float.Parse(dr["amount"].ToString());
                
                GroupClaim c = new GroupClaim(claimId, amount, claimType, description);
                gClaimsList.Add(c);
                    
                
            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return gClaimsList;
        }


        public int DeleteClaim(int claimID)
        {
            int result1 = 0;
            string queryStr = "DELETE FROM GroupClaim WHERE claimsID = @claimID";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@claimID", claimID);

            conn.Open();

            int result = cmd.ExecuteNonQuery();
            conn.Close();

            if (result > 0)
            {
                string queryStr1 = "DELETE FROM Claims WHERE claimsID = @claimID";
                SqlConnection conn1 = new SqlConnection(connStr);
                SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);

                cmd1.Parameters.AddWithValue("@claimID", claimID);

                conn1.Open();

                result1 = cmd1.ExecuteNonQuery();
                conn1.Close();
            }
            
            return result1;
        }

        public List<GroupClaim> FilterGroupByStatus(int employeeID, string claimstatus)
        {
            List<GroupClaim> gClaimsList = new List<GroupClaim>();

            int claimId;
            string title, status;
            float amt;

            if (claimstatus != "All")
            {
                string queryStr = "SELECT status, claimsID FROM Claims WHERE employeeID = @employeeid AND status = @parastatus";
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);

                cmd.Parameters.AddWithValue("@employeeid", employeeID);
                cmd.Parameters.AddWithValue("@parastatus", claimstatus);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    claimId = int.Parse(dr["claimsID"].ToString());

                    status = dr["status"].ToString();

                    string queryStr1 = "SELECT SUM(amount) as total, title FROM GroupClaim WHERE claimsID = @claimID Group BY title";
                    SqlConnection conn1 = new SqlConnection(connStr);
                    SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);

                    cmd1.Parameters.AddWithValue("@claimID", claimId);

                    conn1.Open();
                    SqlDataReader dr1 = cmd1.ExecuteReader();

                    while (dr1.Read())
                    {
                        amt = float.Parse(dr1["total"].ToString());
                        title = dr1["title"].ToString();
                        GroupClaim c = new GroupClaim(claimId, title, status, amt);
                        gClaimsList.Add(c);
                        break;
                    }
                    conn1.Close();
                    dr1.Close();
                    dr1.Dispose();
                }

                conn.Close();
                dr.Close();
                dr.Dispose();
            }
            else
            {
               gClaimsList =  GetGroupClaims(employeeID);
            }


            
            return gClaimsList;
        }


        public string getGroupRejectReason(int claimID)
        {
            string reason = "";


            string queryStr = "SELECT rejectedReason FROM Claims WHERE claimsID = @paraclaimid";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@paraclaimid", claimID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                reason = dr["rejectedReason"].ToString();
            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return reason;
        }

        public float GetMEClaimAmount(int claimId)
        {
            float amt = 0;
            string queryStr = "SELECT SUM(amount) as type FROM GroupClaim WHERE claimsID = '" + claimId + "' AND claimType = 'Meal Expenses'";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["type"].ToString() != "")
                {
                    amt = float.Parse(dr["type"].ToString());
                }
            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return amt;

        }

        public float GetMClaimAmount(int claimId)
        {
            float amt = 0;
            string queryStr = "SELECT SUM(amount) as type FROM GroupClaim WHERE claimsID = '" + claimId + "' AND claimType = 'Medical'";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["type"].ToString() != "")
                {
                    amt = float.Parse(dr["type"].ToString());
                }

            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return amt;

        }

        public float GetPAClaimAmount(int claimId)
        {
            float amt = 0;
            string queryStr = "SELECT SUM(amount) as type FROM GroupClaim WHERE claimsID = '" + claimId + "' AND claimType = 'Phone Allowance'";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["type"].ToString() != "")
                {
                    amt = float.Parse(dr["type"].ToString());
                }

            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return amt;

        }

        public float GetTClaimAmount(int claimId)
        {
            float amt = 0;
            string queryStr = "SELECT SUM(amount) as type FROM GroupClaim WHERE claimsID = '" + claimId + "' AND claimType = 'Transport'";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["type"].ToString() != "")
                {
                    amt = float.Parse(dr["type"].ToString());
                }


            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return amt;

        }

    }
}