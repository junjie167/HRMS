using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HRMS.DAL
{
    public class Leave
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;

        private int _leaveID = 0;
        private string _leaveType = "";
        private int _employeeID = 0;
        private string _startDate;
        private string _endDate;
        private int _duration = 0;
        private string _leaveStatus = "";
        private int _reviewBy = 0;
        private string _rejectedReason = "";
        private int _leaveAmt = 0;

        public Leave()
        {
            // empty constructor//
        }

        public Leave(int leaveID, string leaveType, int employeeID, string startDate, string endDate, int duration, string leaveStatus,
            int reviewBy, string rejectedReason)
        {
            _leaveID = leaveID;
            _leaveType = leaveType;
            _employeeID = employeeID;
            _startDate = startDate;
            _endDate = endDate;
            _duration = duration;
            _leaveStatus = leaveStatus;
            _reviewBy = reviewBy;
            _rejectedReason = rejectedReason;
        }

        public Leave(string leaveType, int employeeID, string startDate, string endDate, int duration, string leaveStatus,
            int reviewBy, string rejectedReason)
        : this(0, leaveType, employeeID, startDate, endDate, duration, leaveStatus, reviewBy, rejectedReason)
        {
        }

        public Leave(string leaveType, string startDate, string endDate, int duration, string leaveStatus)
        {
            _leaveType = leaveType;
            _startDate = startDate;
            _endDate = endDate;
            _duration = duration;
            _leaveStatus = leaveStatus;
        }

        public Leave(string leaveType, string startDate, string endDate, string leaveStatus)
        {
            _leaveType = leaveType;
            _startDate = startDate;
            _endDate = endDate;
            _leaveStatus = leaveStatus;
        }

        public Leave(int leaveID, string leaveType, string startDate, string endDate, int duration)
        {
            _leaveID = leaveID;
            _leaveType = leaveType;
            _startDate = startDate;
            _endDate = endDate;
            _duration = duration;
        }

        public Leave(string startDate, string endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }
        public Leave(int leaveAmount)
        {
            _leaveAmt = leaveAmount;
        }
        public int leaveAmt
        {
            get { return _leaveAmt; }
            set { _leaveAmt = value; }
        }
        public int leaveID
        {
            get { return _leaveID; }
            set { _leaveID = value; }
        }
        public string leaveType
        {
            get { return _leaveType; }
            set { _leaveType = value; }
        }
        public int employeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }
        public string startDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public string endDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        public int duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        public string leaveStatus
        {
            get { return _leaveStatus; }
            set { _leaveStatus = value; }
        }
        public int reviewBy
        {
            get { return _reviewBy; }
            set { _reviewBy = value; }
        }
        public string rejectedReason
        {
            get { return _rejectedReason; }
            set { _rejectedReason = value; }
        }


        //storing into database//
        public int leaveInsert()
        {

            int result = 0;

            string queryStr = "INSERT INTO Leave( leaveType, employeeID, startDate, endDate, duration, leaveStatus, reviewBy, rejectedReason)" +
                "values (@leaveType,@employeeID, @startDate, @endDate, @duration, @leaveStatus, @reviewBy, @rejectedReason)";
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);

                cmd.Parameters.AddWithValue("@leaveType", leaveType);
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@leaveStatus", leaveStatus);
                cmd.Parameters.AddWithValue("@reviewBy", reviewBy);
                cmd.Parameters.AddWithValue("@rejectedReason", rejectedReason);

                conn.Open();

                result += cmd.ExecuteNonQuery(); // Returns no. of rows affected. Must be > 0
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /*public List<Leave> getdatebasedonemp(int empid)
        {
            List<Leave> summaryList = new List<Leave>();

            string startDate, endDate;
             
            string queryStr = "SELECT startDate, endDate FROM Leave WHERE employeeID = @employeeID ";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@employeeID", empid);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                startDate = dr["startDate"].ToString();
                endDate = dr["endDate"].ToString();
                summaryList.Add(new Leave()
                {
                    startDate = dr.GetString(dr.GetOrdinal("startDate"))
                    
                });
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return summaryList;

        }*/


        public List<Leave> getSummaryLeave(int employeeID)
        {
            List<Leave> summaryList = new List<Leave>();

            string leaveType, startDate, endDate,
                leaveStatus;



            string queryStr = "SELECT leaveType, startDate, endDate, leaveStatus FROM Leave WHERE employeeID = @employeeID ";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@employeeID", employeeID);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            //Continue to read the resultsets row by row if not the end
            while (dr.Read())
            {
                leaveType = dr["leaveType"].ToString();
                startDate = dr["startDate"].ToString();
                endDate = dr["endDate"].ToString();
                leaveStatus = dr["leaveStatus"].ToString();

                Leave a = new Leave(leaveType, startDate, endDate, leaveStatus);
                summaryList.Add(a);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return summaryList;
        }


        public Leave updatedMedicalCount(int empid)
        {
            Leave detail = null;
            int Medicalamt;
            string queryStr = "SELECT LeaveAmount FROM [leaveQuota] WHERE employeeID = @empid and leaveType = 'Medical'";
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@empid", empid);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                //Check if there are any resultsets
                if (dr.Read())
                {

                    Medicalamt = int.Parse(dr["LeaveAmount"].ToString());

                    detail = new Leave(Medicalamt);

                }
                else
                {
                    detail = null;
                }

                conn.Close();
                dr.Close();
                dr.Dispose();
                return detail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Leave getLeaveCount(int empid, string ddlValue)
        {
            Leave detail = null;


            int leaveAmt;


            string queryStr = "SELECT LeaveAmount FROM [leaveQuota] WHERE employeeID = @empid and leaveType = @ddlValue";



            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@empid", empid);
                cmd.Parameters.AddWithValue("@ddlValue", ddlValue);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                //Check if there are any resultsets
                if (dr.Read())
                {

                    leaveAmt = int.Parse(dr["LeaveAmount"].ToString());

                    detail = new Leave(leaveAmt);

                }
                else
                {
                    detail = null;
                }

                conn.Close();
                dr.Close();
                dr.Dispose();
                return detail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public Leave getLeaves(int ID)
        {
            Leave lDetail = null;
            int duration;
            string leaveType, startDate, endDate;


            string queryStr = "SELECT * FROM Leave WHERE leaveID = @leaveID";


            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@leaveID", ID);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Check if there are any resultsets
            if (dr.Read())
            {
                // leaveID = Convert.ToInt32(dr["leaveID"].ToString());
                leaveType = dr["leaveType"].ToString();
                startDate = dr["startDate"].ToString();
                endDate = dr["endDate"].ToString();
                duration = int.Parse(dr["duration"].ToString());


                lDetail = new Leave(int.Parse(dr["leaveID"].ToString()), leaveType, startDate, endDate, duration);
            }
            else
            {
                lDetail = null;
            }
            conn.Close();
            dr.Close();
            dr.Dispose();
            return lDetail;


        }
        public int updatequota(int empid, string lt, int duration)
        {
            string queryStr = "UPDATE leaveQuota SET" +
                " leaveAmount = leaveAmount - @duration " +
                " WHERE leaveType = @lt AND employeeID = @empid";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@empid", empid);
            cmd.Parameters.AddWithValue("@lt", lt);
            cmd.Parameters.AddWithValue("@duration", duration);

            conn.Open();
            int nofRow = 0;
            nofRow = cmd.ExecuteNonQuery();
            conn.Close();
            return nofRow;
        }

        //update quota by adding back before minus 

        public int updatequotaAdd(int empid, string lt, int duration)
        {
            string queryStr = "UPDATE leaveQuota SET" +
                " leaveAmount = leaveAmount + @duration " +
                " WHERE leaveType = @lt AND employeeID = @empid";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@empid", empid);
            cmd.Parameters.AddWithValue("@lt", lt);
            cmd.Parameters.AddWithValue("@duration", duration);

            conn.Open();
            int nofRow = 0;
            nofRow = cmd.ExecuteNonQuery();
            conn.Close();
            return nofRow;
        }

        public int LeaveUpdate(int pId, string pstartDate, string pendDate, string pduration)
        {
            string queryStr = "UPDATE Leave SET" +
            //" Product_ID = @productID, " +
            " startDate = @startDate, " +
            " endDate = @endDate, " +
            " duration =@duration " +

            " WHERE leaveID =@leaveID"
            ;


            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@leaveID", pId);
            cmd.Parameters.AddWithValue("@startDate", pstartDate);
            cmd.Parameters.AddWithValue("@endDate", pendDate);
            cmd.Parameters.AddWithValue("@duration", pduration);

            conn.Open();
            int nofRow = 0;
            nofRow = cmd.ExecuteNonQuery();
            conn.Close();
            return nofRow;

        }//end Update
    }
}