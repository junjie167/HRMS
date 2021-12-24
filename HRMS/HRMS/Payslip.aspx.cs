using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRMS
{
    public partial class Payslip : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //String testID = Session["ID"].ToString();
            //String getPSMth = Session["getMth"].ToString();
            //string getPSYear = Session["getYear"].ToString();
            //int num2 = int.Parse(Session["dashBPS"].ToString());


            if (!Page.IsPostBack)
            {

                if (Session["dashBPS"] != null)
                {
                    String testID = Session["ID"].ToString();
                    String getPSMth = Session["getMth"].ToString();
                    string getPSYear = Session["getYear"].ToString();
                    try
                    {
                        string constr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                        SqlConnection con = new SqlConnection(constr);
                        String sql = "SELECT * FROM Payslip INNER JOIN Employee ON Payslip.employeeID = Employee.employeeID WHERE Payslip.employeeID=@id AND Payslip.month=@mth AND Payslip.year=@year; ";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        cmd.Parameters.AddWithValue("@id", testID);
                        cmd.Parameters.AddWithValue("@mth", getPSMth);
                        cmd.Parameters.AddWithValue("@year", int.Parse(getPSYear));
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        con.Open();
                        lblEmpName.Text = ds.Tables[0].Rows[0]["employeeName"].ToString();
                        lblEmpID.Text = ds.Tables[0].Rows[0]["employeeID"].ToString();
                        lblEmpStat.Text = ds.Tables[0].Rows[0]["employeeStatus"].ToString();
                        lblJobTitle.Text = ds.Tables[0].Rows[0]["position"].ToString();
                        lblBasicPay.Text = ds.Tables[0].Rows[0]["basicPayAmt"].ToString();
                        lblCPF.Text = ds.Tables[0].Rows[0]["employeeCPFAmt"].ToString();
                        lblOT.Text = ds.Tables[0].Rows[0]["overtimeAmt"].ToString();
                        lblTD.Text = lblCPF.Text;
                        String strng1 = lblBasicPay.Text;
                        String strng2 = lblOT.Text;
                        Double takevalue = Convert.ToDouble(strng1) + Convert.ToDouble(strng2);
                        lblTE.Text = takevalue.ToString();
                        lblpsMth.Text = ds.Tables[0].Rows[0]["month"].ToString();
                        string datePS = ds.Tables[0].Rows[0]["date"].ToString();
                        lblpsDateReceived.Text = datePS.Remove(10);
                        con.Close();
                        Session.Remove("dashBPS");

                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), " openErrorModal", " openErrorModal();", true);
                        //Response.Write("FAILED!!!");
                        Session.Remove("dashBPS");

                    }
                }

                else
                {
                    String testID = Session["ID"].ToString();
                    string constr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                    SqlConnection con = new SqlConnection(constr);
                    String sql = "SELECT * FROM Payslip INNER JOIN Employee ON Payslip.employeeID = Employee.employeeID WHERE Payslip.employeeID=@id ORDER BY Payslip.date DESC; ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@id", testID);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Open();
                    lblEmpName.Text = ds.Tables[0].Rows[0]["employeeName"].ToString();
                    lblEmpID.Text = ds.Tables[0].Rows[0]["employeeID"].ToString();
                    lblEmpStat.Text = ds.Tables[0].Rows[0]["employeeStatus"].ToString();
                    lblJobTitle.Text = ds.Tables[0].Rows[0]["position"].ToString();
                    lblBasicPay.Text = ds.Tables[0].Rows[0]["basicPayAmt"].ToString();
                    lblCPF.Text = ds.Tables[0].Rows[0]["employeeCPFAmt"].ToString();
                    lblOT.Text = ds.Tables[0].Rows[0]["overtimeAmt"].ToString();
                    lblTD.Text = lblCPF.Text;
                    String strng1 = lblBasicPay.Text;
                    String strng2 = lblOT.Text;
                    Double takevalue = Convert.ToDouble(strng1) + Convert.ToDouble(strng2);
                    lblTE.Text = takevalue.ToString();
                    lblpsMth.Text = ds.Tables[0].Rows[0]["month"].ToString();
                    string datePS = ds.Tables[0].Rows[0]["date"].ToString();
                    lblpsDateReceived.Text = datePS.Remove(10);
                    con.Close();
                }

            }
            else
            {
            }

        }






        protected void BtnMthYear_Click(object sender, EventArgs e)
        {
            string test = monthYear.Value;
            int getMth;
            int getYear;
            string mth = "";

            getMth = int.Parse(test.Remove(0, 5));
            getYear = int.Parse(test.Remove(4));
            //TextBox1.Text = getYear.ToString();
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

            if (Session["ID"] != null)
            {
                String testID = Session["ID"].ToString();

                string constr = ConfigurationManager.ConnectionStrings["HRMS_Database"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                String sql = "SELECT * FROM Payslip INNER JOIN Employee ON Payslip.employeeID = Employee.employeeID WHERE Payslip.employeeID=@id AND Payslip.month = @mth AND Payslip.year = @yr; ";


                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@id", testID);
                cmd.Parameters.AddWithValue("@mth", mth);
                cmd.Parameters.AddWithValue("@yr", getYear);

                DataSet ds = new DataSet();
                da.Fill(ds);

                con.Open();

                try
                {

                    string isMthExist = ds.Tables[0].Rows[0]["month"].ToString();
                    if (!(isMthExist == mth))
                    {
                        Response.Write("ERROR2");
                    }
                    else
                    {

                        lblBasicPay.Text = ds.Tables[0].Rows[0]["basicPayAmt"].ToString();
                        lblCPF.Text = ds.Tables[0].Rows[0]["employeeCPFAmt"].ToString();
                        lblOT.Text = ds.Tables[0].Rows[0]["overtimeAmt"].ToString();
                        lblTD.Text = lblCPF.Text;

                        String strng1 = lblBasicPay.Text;
                        String strng2 = lblOT.Text;
                        Double takevalue = Convert.ToDouble(strng1) + Convert.ToDouble(strng2);
                        lblTE.Text = takevalue.ToString();

                        lblpsMth.Text = ds.Tables[0].Rows[0]["month"].ToString();
                        string datePS = ds.Tables[0].Rows[0]["date"].ToString();
                        lblpsDateReceived.Text = datePS.Remove(10);

                        con.Close();

                        //Response.Write(isMthExist);

                    }

                }
                catch (Exception)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), " openErrorModal", " openErrorModal();", true);
                    //Response.Write("ERROR3");



                }


            }




        }

        protected void Button_reload(object sender, EventArgs e)
        {
            Response.Redirect("Payslip.aspx");
        }
    }
}