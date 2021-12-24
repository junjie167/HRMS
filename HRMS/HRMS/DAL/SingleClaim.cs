using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace HRMS.DAL
{
    public class SingleClaim
    {
        private int claimID;
        private int employeeID;
        private string status;
        private string fromDate;
        private string toDate;
        private string description;
        private float amount;
        private string attachment;
        private string claimType;

        private string reviewBy;
        private string rejectedReason;

        public SingleClaim() { }

        public SingleClaim(int claimsID, string claimType, string description, float amount, string status)
        {
            this.claimID = claimsID;
            this.claimType = claimType;
            this.description = description;
            this.amount = amount;
            this.status = status;
        }

        public SingleClaim(string claimType, string fromDate, string toDate, string claimDescription, float amount, string attachment)
        {
            this.claimType = claimType;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.description = claimDescription;
            this.amount = amount;
            this.attachment = attachment;
        }

        public int ClaimID
        {
            get
            {
                return claimID;
            }

            set
            {
                claimID = value;
            }
        }

        public string claimStatus
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public string ClaimType
        {
            get
            {
                return claimType;
            }

            set
            {
                claimType = value;
            }
        }

        public string FromDate { get => fromDate; set => fromDate = value; }
        public string ToDate { get => toDate; set => toDate = value; }
        public string Description { get => description; set => description = value; }
        public float claimAmount { get => amount; set => amount = value; }
        public string Attachment { get => attachment; set => attachment = value; }
        public string ReviewBy { get => reviewBy; set => reviewBy = value; }
        public string RejectedReason { get => rejectedReason; set => rejectedReason = value; }

        string DBConnect = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
        public int createSingleClaims(int employeeID, string fromDate, string toDate, string claimType, string description, float amount, string attachment)
        {
            int result = 0;
           
            StringBuilder sqlcomm = new StringBuilder();
            sqlcomm.AppendLine("INSERT INTO Claims(employeeID, status, fromDate, toDate)");
            sqlcomm.AppendLine("VALUES(@paraemployeeID, @parastatus, @parafromdate, @paratodate)");

            try
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlCommand sqlCmd = new SqlCommand(sqlcomm.ToString(), myConn);

                sqlCmd.Parameters.AddWithValue("@paraemployeeID", employeeID);
                sqlCmd.Parameters.AddWithValue("@parastatus", "Pending");
                sqlCmd.Parameters.AddWithValue("@parafromdate", fromDate);
                sqlCmd.Parameters.AddWithValue("@paratodate", toDate);

                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();
                myConn.Close();

                int claim_id = 0;
                string queryStr = "SELECT claimsID FROM Claims WHERE employeeID=@paraemployeeID ORDER BY claimsID DESC";
                SqlCommand cmd = new SqlCommand(queryStr, myConn);

                cmd.Parameters.AddWithValue("@paraemployeeID", employeeID);

                myConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    claim_id = int.Parse(dr["claimsID"].ToString());
                }

                if (claim_id != 0)
                {
                    insertSingleClaim(claim_id, claimType, description, amount, attachment);
                }

            }
            catch (Exception e)
            {
                result = 0;
            }

            return result;
        }

        private int insertSingleClaim(int claimID, string claimType, string description, float amount, string attachment)
        {
            int result = 0;

            StringBuilder sqlcomm = new StringBuilder();
            sqlcomm.AppendLine("INSERT INTO SingleClaim(claimsID, claimType, description, amount, attachment)");
            sqlcomm.AppendLine("VALUES(@paraclaimID, @paraclaimtype, @paradescription, @paraamt, @paraattachment)");

            try
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlCommand sqlCmd = new SqlCommand(sqlcomm.ToString(), myConn);

                sqlCmd.Parameters.AddWithValue("@paraclaimID", claimID);
                sqlCmd.Parameters.AddWithValue("@paraclaimtype", claimType);
                sqlCmd.Parameters.AddWithValue("@paradescription", description);
                sqlCmd.Parameters.AddWithValue("@paraamt", amount);
                sqlCmd.Parameters.AddWithValue("@paraattachment", attachment);

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


        public List<SingleClaim> retrieveSingleClaimsSummary(int employeeID)
        {
            List<SingleClaim> singleClaimsList = new List<SingleClaim>();

            int claimID;
            string claimType;
            float claimAmount;
            string claimStatus;
            string description;


            string queryStr = "SELECT * FROM SingleClaim s INNER JOIN Claims c ON s.claimsID = c.claimsID WHERE c.employeeID = @paraemployeeid ORDER BY s.claimsID DESC";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@paraemployeeid", employeeID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                claimID = int.Parse(dr["claimsID"].ToString());
                claimType = dr["claimType"].ToString();
                description = dr["description"].ToString();
                claimAmount = float.Parse(dr["amount"].ToString());
                claimStatus = dr["status"].ToString();

                SingleClaim claimTypeObject = new SingleClaim(claimID, claimType, description, claimAmount, claimStatus);
                singleClaimsList.Add(claimTypeObject);

            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return singleClaimsList;
        }

        public List<SingleClaim> retrieveSingleClaimToEdit(int claimID)
        {
            List<SingleClaim> singleClaimsList = new List<SingleClaim>();

            string claimType;
            string from_date;
            string to_date;
            string description;
            string attachment;
            float claimAmount;


            string queryStr = "SELECT * FROM SingleClaim s INNER JOIN Claims c ON s.claimsID = c.claimsID WHERE c.claimsID = @paraclaimid";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@paraclaimid", claimID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                claimType = dr["claimType"].ToString();
                from_date = dr["fromDate"].ToString();
                to_date = dr["toDate"].ToString();
                description = dr["description"].ToString();
                claimAmount = float.Parse(dr["amount"].ToString());
                attachment = dr["attachment"].ToString();

                SingleClaim claimTypeObject = new SingleClaim(claimType, from_date, to_date, description, claimAmount, attachment);
                singleClaimsList.Add(claimTypeObject);

            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return singleClaimsList;
        }

        public int updateSingleClaim(string claimType, string fromDate, string toDate, string description, float amount, string attachment, int claimID)
        {
            int result = 0;

            StringBuilder sqlcomm = new StringBuilder();

            sqlcomm.AppendLine("UPDATE Claims SET fromDate= @parafromdate, toDate= @paratodate");
            sqlcomm.AppendLine("WHERE  claimsID= @paraclaimid");

            try
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlCommand sqlCmd = new SqlCommand(sqlcomm.ToString(), myConn);

                sqlCmd.Parameters.AddWithValue("@parafromdate", fromDate);
                sqlCmd.Parameters.AddWithValue("@paratodate", toDate);
                sqlCmd.Parameters.AddWithValue("@paraclaimid", claimID);

                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();
                myConn.Close();

                if (result == 1)
                {
                    updateSingleClaimTable(claimType, description, amount, attachment, claimID);
                }

            }catch (Exception e)
            {
                result = 0;
            }

            return result;
        }

        private int updateSingleClaimTable(string claimType, string description, float amount, string attachment, int claimID)
        {
            int result = 0;

            StringBuilder sqlcomm = new StringBuilder();

            sqlcomm.AppendLine("UPDATE SingleClaim SET claimType= @paraclaimtype, description= @paradescrition, amount= @paraamount, attachment= @paraattachment");
            sqlcomm.AppendLine("WHERE  claimsID= @paraclaimid");

            try
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlCommand sqlCmd = new SqlCommand(sqlcomm.ToString(), myConn);

                sqlCmd.Parameters.AddWithValue("@paraclaimid", claimID);
                sqlCmd.Parameters.AddWithValue("@paraclaimtype", claimType);
                sqlCmd.Parameters.AddWithValue("@paradescrition", description);
                sqlCmd.Parameters.AddWithValue("@paraamount", amount);
                sqlCmd.Parameters.AddWithValue("@paraattachment", attachment);

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

        public int DeleteSingleClaim(int claimID)
        {
            StringBuilder sqlComm = new StringBuilder();
            int result = 0;

            sqlComm.AppendLine("DELETE FROM SingleClaim");
            sqlComm.AppendLine("WHERE claimsID= @paraclaimid");

            try
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlCommand cmd = new SqlCommand(sqlComm.ToString(), myConn);


                cmd.Parameters.AddWithValue("@paraclaimid", claimID);

                myConn.Open();
                result = cmd.ExecuteNonQuery();
                myConn.Close();

                if (result == 1)
                {
                    DeleteClaimRecord_Claimtable(claimID);
                }
            }
            catch (Exception e)
            {
                result = 0;
            }
            return result;
        }

        private int DeleteClaimRecord_Claimtable(int claimID)
        {
            StringBuilder sqlComm = new StringBuilder();
            int result = 0;

            sqlComm.AppendLine("DELETE FROM Claims");
            sqlComm.AppendLine("WHERE claimsID= @paraclaimid");

            try
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlCommand cmd = new SqlCommand(sqlComm.ToString(), myConn);


                cmd.Parameters.AddWithValue("@paraclaimid", claimID);

                myConn.Open();
                result = cmd.ExecuteNonQuery();
                myConn.Close();

            }
            catch (Exception e)
            {
                result = 0;
            }
            return result;
        }

        public float retrieveSingleApprovlAmt(int employeeID)
        {
            float total = 0;
            string queryStr = "SELECT SUM(g.amount) AS total FROM GroupClaim g INNER JOIN Claims c ON g.claimsID = c.claimsID WHERE c.employeeID = " + employeeID + " AND c.status = 'Approved'";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["total"].ToString() != "")
                {
                    total += float.Parse(dr["total"].ToString());
                }

            }

            conn.Close();

            dr.Close();
            dr.Dispose();

            string queryStr1 = "SELECT SUM(s.amount) AS total FROM SingleClaim s INNER JOIN Claims c ON s.claimsID = c.claimsID WHERE c.employeeID = " + employeeID + " AND c.status = 'Approved'";
            SqlConnection conn1 = new SqlConnection(DBConnect);
            SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);
            conn1.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr1.Read())
            {
                if (dr1["total"].ToString() != "")
                {
                    total += float.Parse(dr1["total"].ToString());
                }

            }

            conn1.Close();

            dr1.Close();
            dr1.Dispose();

            return total;
        }

        public List<SingleClaim> filterSingleClaimByStatus(int employeeID, string status)
        {
            List<SingleClaim> singleClaimsList = new List<SingleClaim>();

            int claimID;
            string claimType;
            float claimAmount;
            string claimStatus;
            string description;

            if (status != "All")
            {
                string queryStr = "SELECT * FROM SingleClaim s INNER JOIN Claims c ON s.claimsID = c.claimsID WHERE c.employeeID = @paraemployeeid AND c.status = @parastatus ORDER BY s.claimsID DESC";
                SqlConnection conn = new SqlConnection(DBConnect);
                SqlCommand cmd = new SqlCommand(queryStr, conn);

                cmd.Parameters.AddWithValue("@paraemployeeid", employeeID);
                cmd.Parameters.AddWithValue("@parastatus", status);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    claimID = int.Parse(dr["claimsID"].ToString());
                    claimType = dr["claimType"].ToString();
                    description = dr["description"].ToString();
                    claimAmount = float.Parse(dr["amount"].ToString());
                    claimStatus = dr["status"].ToString();

                    SingleClaim claimTypeObject = new SingleClaim(claimID, claimType, description, claimAmount, claimStatus);
                    singleClaimsList.Add(claimTypeObject);

                }

                conn.Close();
                dr.Close();
                dr.Dispose();
            }
            else
            {
                string queryStr = "SELECT * FROM SingleClaim s INNER JOIN Claims c ON s.claimsID = c.claimsID WHERE c.employeeID = @paraemployeeid ORDER BY s.claimsID DESC";
                SqlConnection conn = new SqlConnection(DBConnect);
                SqlCommand cmd = new SqlCommand(queryStr, conn);

                cmd.Parameters.AddWithValue("@paraemployeeid", employeeID);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    claimID = int.Parse(dr["claimsID"].ToString());
                    claimType = dr["claimType"].ToString();
                    description = dr["description"].ToString();
                    claimAmount = float.Parse(dr["amount"].ToString());
                    claimStatus = dr["status"].ToString();

                    SingleClaim claimTypeObject = new SingleClaim(claimID, claimType, description, claimAmount, claimStatus);
                    singleClaimsList.Add(claimTypeObject);

                }

                conn.Close();
                dr.Close();
                dr.Dispose();
            }


            
            return singleClaimsList;
        }

        public string getSingleRejectReason(int claimID)
        {
            string reason = "";


            string queryStr = "SELECT rejectedReason FROM Claims WHERE claimsID = @paraclaimid";
            SqlConnection conn = new SqlConnection(DBConnect);
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
    }
}