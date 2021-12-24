using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace HRMS.DAL
{
    public class ClaimQuota
    {
        private string claimType;
        private float quotaAmount;
        private int employeeID;

        public ClaimQuota() { }

        public ClaimQuota(string claimType, float quotaAmount)
        {
            this.claimType = claimType;
            this.quotaAmount = quotaAmount;
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

        public float QuotaAmount
        {
            get
            {
                return quotaAmount;
            }

            set
            {
                quotaAmount = value;
            }
        }

        public int EmployeeID
        {
            get
            {
                return employeeID;
            }

            set
            {
                employeeID = value;
            }
        }

        string _connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;

        public float GetMediQ(int employeeID)
        {
            float total = 0;
            string queryStr = "SELECT SUM(GroupClaim.amount) AS total FROM GroupClaim, Claims WHERE Claims.employeeID = " + employeeID + " AND Claims.claimsID = GroupClaim.claimsID AND GroupClaim.claimType = 'Medical' AND Claims.status = 'Pending'";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["total"].ToString() != "")
                {
                    total -= float.Parse(dr["total"].ToString());
                }
            }

            conn.Close();

            dr.Close();
            dr.Dispose();

            string queryStr1 = "SELECT SUM(SingleClaim.amount) AS total FROM SingleClaim, Claims WHERE Claims.employeeID = " + employeeID + " AND Claims.claimsID = SingleClaim.claimsID AND SingleClaim.claimType = 'Medical' AND Claims.status = 'Pending'";
            SqlConnection conn1 = new SqlConnection(_connStr);
            SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);
            conn1.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr1.Read())
            {
                if (dr1["total"].ToString() != "")
                {
                    total -= float.Parse(dr1["total"].ToString());
                }
            }

            conn1.Close();

            dr1.Close();
            dr1.Dispose();

            string queryStr2 = "SELECT quotaAmount FROM ClaimQuota WHERE ClaimType = 'Medical'";
            SqlConnection conn2 = new SqlConnection(_connStr);
            SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);
            conn2.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr2.Read())
            {
                total += float.Parse(dr2["quotaAmount"].ToString());
            }

            conn2.Close();

            dr2.Close();
            dr2.Dispose();

            return total;

        }

        public float GetMEQ(int employeeID)
        {
            float total = 0;
            string id;
            string queryStr = "SELECT SUM(GroupClaim.amount) AS total FROM GroupClaim, Claims WHERE Claims.employeeID = " + employeeID + " AND Claims.claimsID = GroupClaim.claimsID AND GroupClaim.claimType = 'Meal Expenses' AND Claims.status = 'Pending'";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["total"].ToString() != "")
                {
                    total -= float.Parse(dr["total"].ToString());
                }

            }

            conn.Close();

            dr.Close();
            dr.Dispose();

            string queryStr1 = "SELECT SUM(SingleClaim.amount) AS total FROM SingleClaim, Claims WHERE Claims.employeeID = " + employeeID + " AND Claims.claimsID = SingleClaim.claimsID AND SingleClaim.claimType = 'Meal Expenses' AND Claims.status = 'Pending'";
            SqlConnection conn1 = new SqlConnection(_connStr);
            SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);
            conn1.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr1.Read())
            {
                if (dr1["total"].ToString() != "")
                {
                    total -= float.Parse(dr1["total"].ToString());
                }

            }

            conn1.Close();

            dr1.Close();
            dr1.Dispose();

            string queryStr2 = "SELECT quotaAmount FROM ClaimQuota WHERE ClaimType = 'Meal Expenses'";
            SqlConnection conn2 = new SqlConnection(_connStr);
            SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);
            conn2.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr2.Read())
            {
                total += float.Parse(dr2["quotaAmount"].ToString());
            }

            conn2.Close();

            dr2.Close();
            dr2.Dispose();

            return total;

        }

        public float GetPAQ(int employeeID)
        {
            float total = 0;
            string queryStr = "SELECT SUM(GroupClaim.amount) AS total FROM GroupClaim, Claims WHERE Claims.employeeID = " + employeeID + " AND Claims.claimsID = GroupClaim.claimsID AND GroupClaim.claimType = 'Phone Allowance' AND Claims.status = 'Pending'";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["total"].ToString() != "")
                {
                    total -= float.Parse(dr["total"].ToString());
                }

            }

            conn.Close();

            dr.Close();
            dr.Dispose();

            string queryStr1 = "SELECT SUM(SingleClaim.amount) AS total FROM SingleClaim, Claims WHERE Claims.employeeID = " + employeeID + " AND Claims.claimsID = SingleClaim.claimsID AND SingleClaim.claimType = 'Phone Allowance' AND Claims.status = 'Pending'";
            SqlConnection conn1 = new SqlConnection(_connStr);
            SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);
            conn1.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr1.Read())
            {
                if (dr1["total"].ToString() != "")
                {
                    total -= float.Parse(dr1["total"].ToString());
                }

            }

            conn1.Close();

            dr1.Close();
            dr1.Dispose();

            string queryStr2 = "SELECT quotaAmount FROM ClaimQuota WHERE ClaimType = 'Phone Allowance'";
            SqlConnection conn2 = new SqlConnection(_connStr);
            SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);
            conn2.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr2.Read())
            {
                total += float.Parse(dr2["quotaAmount"].ToString());
            }

            conn2.Close();

            dr2.Close();
            dr2.Dispose();

            return total;

        }

        public float GetTranQ(int employeeID)
        {
            float total = 0;
            string queryStr = "SELECT SUM(GroupClaim.amount) AS total FROM GroupClaim, Claims WHERE Claims.employeeID = " + employeeID + " AND Claims.claimsID = GroupClaim.claimsID AND GroupClaim.claimType = 'Transport' AND Claims.status = 'Pending'";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                if (dr["total"].ToString() != "")
                {
                    total -= float.Parse(dr["total"].ToString());
                }

            }

            conn.Close();

            dr.Close();
            dr.Dispose();

            string queryStr1 = "SELECT SUM(SingleClaim.amount) AS total FROM SingleClaim, Claims WHERE Claims.employeeID = " + employeeID + " AND Claims.claimsID = SingleClaim.claimsID AND SingleClaim.claimType = 'Transport' AND Claims.status = 'Pending'";
            SqlConnection conn1 = new SqlConnection(_connStr);
            SqlCommand cmd1 = new SqlCommand(queryStr1, conn1);
            conn1.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr1.Read())
            {
                if (dr1["total"].ToString() != "")
                {
                    total -= float.Parse(dr1["total"].ToString());
                }

            }

            conn1.Close();

            dr1.Close();
            dr1.Dispose();

            string queryStr2 = "SELECT quotaAmount FROM ClaimQuota WHERE ClaimType = 'Transport'";
            SqlConnection conn2 = new SqlConnection(_connStr);
            SqlCommand cmd2 = new SqlCommand(queryStr2, conn2);
            conn2.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            //Continue to read the resultsets row by row if not the end
            while (dr2.Read())
            {
                total += float.Parse(dr2["quotaAmount"].ToString());
            }

            conn2.Close();

            dr2.Close();
            dr2.Dispose();

            return total;

        }

        public List<ClaimQuota> retrieveClaimQuota()
        {
            List<ClaimQuota> claimQuotaList = new List<ClaimQuota>();

            string type;
            float amount;

            string queryStr = "SELECT * FROM ClaimQuota";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                type = dr["claimType"].ToString();
                amount = float.Parse(dr["quotaAmount"].ToString());

                ClaimQuota claimTypeObject = new ClaimQuota(type, amount);
                claimQuotaList.Add(claimTypeObject);

            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return claimQuotaList;

        }

    }
}