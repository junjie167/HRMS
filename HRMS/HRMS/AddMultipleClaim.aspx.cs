using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HRMS.DAL;
using System.Text.RegularExpressions;

namespace HRMS
{
    public partial class AddMultipleClaim : System.Web.UI.Page
    {
        public static int count = 0;
        GroupClaim aGroupClaim = new GroupClaim();
        ClaimQuota aClaimQuota = new ClaimQuota();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IDictionary<int, HttpPostedFile> filepath = new Dictionary<int, HttpPostedFile>();
                Session["filepath"] = filepath;
                SetInitialRow();

                int employeeID = int.Parse(Session["ID"].ToString());

                float mealEx = aClaimQuota.GetMEQ(employeeID);
                lblMealEx.Text = String.Format("{0:n}", aClaimQuota.GetMEQ(employeeID));
                float medi = aClaimQuota.GetMediQ(employeeID);
                lblMedical.Text = String.Format("{0:n}", aClaimQuota.GetMediQ(employeeID));
                float pA = aClaimQuota.GetPAQ(employeeID);
                lblPA.Text = String.Format("{0:n}", aClaimQuota.GetPAQ(employeeID));
                float tran = aClaimQuota.GetTranQ(employeeID);
                lblTransport.Text = String.Format("{0:n}", aClaimQuota.GetTranQ(employeeID));

                DropDownList dropDownList = (DropDownList)gvMultiClaim.Rows[0].Cells[0].FindControl("ddlCliamType");
                dropDownList.SelectedValue = "Meal Expenses";


               

                //int meal = aClaimQuota.GetClaimId();
                //int medi = aClaimQuota.GetClaimId();
                //int ot = aClaimQuota.GetClaimId();
                //int pA = aClaimQuota.GetClaimId();
                //int tran = aClaimQuota.GetClaimId();

            }
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //dt.Columns.Add(new DataColumn("dateFrom", typeof(string)));
            //dt.Columns.Add(new DataColumn("dateTo", typeof(string)));
            dt.Columns.Add(new DataColumn("claimType", typeof(string)));
            dt.Columns.Add(new DataColumn("description", typeof(string)));
            dt.Columns.Add(new DataColumn("amt", typeof(string)));
            //dt.Columns.Add(new DataColumn("amt", typeof(int)));
            dt.Columns.Add(new DataColumn("attachment", typeof(string)));
            dt.Columns.Add(new DataColumn("fileUpload", typeof(string)));

            dr = dt.NewRow();
            //dr["dateFrom"] = string.Empty;
            //dr["dateTo"] = string.Empty;
            dr["claimType"] = string.Empty;
            dr["description"] = string.Empty;
            //dr["amt"] = 0;
            dr["amt"] = string.Empty;
            dr["attachment"] = string.Empty;
            dr["fileUpload"] = string.Empty;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference 

            ViewState["CurrentTable"] = dt;

            //Bind the Gridview 
            gvMultiClaim.DataSource = dt;
            gvMultiClaim.DataBind();

        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            AddNewRowToGrid();
        }

        private void AddNewRowToGrid()
        {

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    string[] filesP = new string[dtCurrentTable.Rows.Count - 1];
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        DropDownList claimType = (DropDownList)gvMultiClaim.Rows[i].Cells[0].FindControl("ddlCliamType");
                        dtCurrentTable.Rows[i]["claimType"] = claimType.SelectedItem.Text;
                        TextBox description = (TextBox)gvMultiClaim.Rows[i].Cells[1].FindControl("tbDes");
                        dtCurrentTable.Rows[i]["description"] = description.Text;
                        TextBox amt = (TextBox)gvMultiClaim.Rows[i].Cells[2].FindControl("tbAmt");
                        dtCurrentTable.Rows[i]["amt"] = amt.Text;
                        HtmlInputFile attachment = gvMultiClaim.Rows[i].Cells[3].FindControl("fulAtt") as HtmlInputFile;

                        if (attachment.PostedFile.FileName != "")
                        {
                            Label att = (Label)gvMultiClaim.Rows[i].Cells[3].FindControl("lblAtt");
                            IDictionary<int, HttpPostedFile> test = (Dictionary<int, HttpPostedFile>)Session["filepath"];


                            test[i] = attachment.PostedFile;
                            Session["filepath"] = test;
                            dtCurrentTable.Rows[i]["attachment"] = attachment.PostedFile.FileName;
                        }
                    }
                    //add new row to DataTable 
                    if (count == 0)
                    {
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        count = 0;
                    }
                    //Store the current data to ViewState for future reference 
                    ViewState["CurrentTable"] = dtCurrentTable;
                    //Rebind the Grid with the current data to reflect changes 
                    gvMultiClaim.DataSource = dtCurrentTable;
                    gvMultiClaim.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks 
            //Session["fileUpload"] = filesP;
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList claimType = (DropDownList)gvMultiClaim.Rows[i].Cells[0].FindControl("ddlCliamType");
                        TextBox description = (TextBox)gvMultiClaim.Rows[i].Cells[1].FindControl("tbDes");
                        TextBox amt = (TextBox)gvMultiClaim.Rows[i].Cells[2].FindControl("tbAmt");
                        //Label att = (Label)gvMultiClaim.Rows[i].Cells[3].FindControl("lblAtt");
                        LinkButton att = (LinkButton)gvMultiClaim.Rows[i].Cells[3].FindControl("lbtnAtt");

                        drCurrentRow = dt.NewRow();
                        claimType.SelectedValue = dt.Rows[i]["claimType"].ToString();
                        description.Text = dt.Rows[i]["description"].ToString();
                        amt.Text = dt.Rows[i]["amt"].ToString();
                        att.Text = dt.Rows[i]["attachment"].ToString();

                        rowIndex++;
                    }

                }
            }

        }

        protected void btnDel_Click(object sender, ImageClickEventArgs e)
        {
            count = 1;
            ImageButton b = (ImageButton)sender;
            GridViewRow row = (GridViewRow)b.NamingContainer;

            if (ViewState["CurrentTable"] != null)
            {
                int rowIndex = row.RowIndex;
                TextBox amt = (TextBox)gvMultiClaim.Rows[rowIndex].Cells[2].FindControl("tbAmt");
                if (amt.Text != "")
                {
                    AddNewRowToGrid();

                }
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    gvMultiClaim.DataSource = dt;
                    gvMultiClaim.DataBind();

                    IDictionary<int, HttpPostedFile> test = (Dictionary<int, HttpPostedFile>)Session["filepath"];
                    test.Remove(rowIndex);
                    for (int i = rowIndex; i < test.Count; i++)
                    {
                        test[rowIndex] = test[rowIndex + 1];
                    }
                    Session["filepath"] = test;

                    SetPreviousData();
                }
                else
                {
                    SetInitialRow();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            count = 1;
            AddNewRowToGrid();

            //count = 1;
            //AddNewRowToGrid();

            float mEClaim = 0;
            float mClaim = 0;
            float pAClaim = 0;
            float tClaim = 0;
            int eVal = 0;
            StringBuilder sb1 = new StringBuilder();
            if (txtGroupTitle.Text == "")
            {
                sb1.AppendLine("- Group Title");
                eVal = 1;
            }
            if (txtDateFrom.Text == "")
            {
                sb1.AppendLine("- Date From");
                eVal = 1;
            }

            if (txtDateTo.Text == "")
            {
                sb1.AppendLine("- Date To");
                eVal = 1;
            }

            //need to check the claim amount
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["description"].ToString() == "")
                        {
                            if (sb1.ToString().Contains("- Description"))
                            {

                            }
                            else
                            {
                                sb1.AppendLine("- Description");
                            }
                            eVal = 1;

                        }
                        if (dt.Rows[i]["amt"].ToString() == "")
                        {
                            if (sb1.ToString().Contains("- Amount"))
                            {

                            }
                            else
                            {
                                sb1.AppendLine("- Amount");
                            }
                            eVal = 1;
                        }
                        if (dt.Rows[i]["attachment"].ToString() == "")
                        {
                            if (sb1.ToString().Contains("- Attachment"))
                            {

                            }
                            else
                            {
                                sb1.AppendLine("- Attachment");
                            }
                            eVal = 1;
                        }
                    }

                    lblEmptyField.Text = sb1.ToString().Replace(Environment.NewLine, "<br />");

                    if (eVal == 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["claimType"].ToString() == "Meal Expenses")
                            {
                                mEClaim += float.Parse(dt.Rows[i]["amt"].ToString());
                            }
                            else if (dt.Rows[i]["claimType"].ToString() == "Medical")
                            {
                                mClaim += float.Parse(dt.Rows[i]["amt"].ToString());
                            }
                            else if (dt.Rows[i]["claimType"].ToString() == "Phone Allowance")
                            {
                                pAClaim += float.Parse(dt.Rows[i]["amt"].ToString());
                            }
                            else
                            {
                                tClaim += float.Parse(dt.Rows[i]["amt"].ToString());
                            }

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "openEmptyFieldModal", "openEmptyFieldModal();", true);
                    }

                }
            }



            if (eVal == 0)
            {
                if (gvMultiClaim.Rows.Count < 2)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "openOneRowModal", "openOneRowModal();", true);
                }
                else
                {
                    DateTime from = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", null);
                    DateTime to = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", null);

                    int check = DateTime.Compare(from, to);

                    if (check > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "openDateFromToModal", "openDateFromToModal();", true);
                    }
                    else
                    {
                        if (mEClaim <= float.Parse(lblMealEx.Text) && mClaim <= float.Parse(lblMedical.Text) && pAClaim <= float.Parse(lblPA.Text) && tClaim <= float.Parse(lblTransport.Text))
                        {
                            lblDateFrom.Text = txtDateFrom.Text;
                            lblDateTo.Text = txtDateTo.Text;
                            lblTitle.Text = txtGroupTitle.Text;
                            float total = 0;
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dt = (DataTable)ViewState["CurrentTable"];
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        total += float.Parse(dt.Rows[i]["amt"].ToString());

                                    }

                                }
                                gv_Details.DataSource = dt;
                                gv_Details.DataBind();
                            }

                            lblTAmt.Text = total.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "openConfirmationModal", "openConfirmationModal();", true);
                        }
                        else
                        {
                            StringBuilder sb2 = new StringBuilder();



                            if (mEClaim >= float.Parse(lblMealEx.Text))
                            {
                                if (sb2.ToString().Contains("- Meal Expenses"))
                                {

                                }
                                else
                                {
                                    sb2.AppendLine("- Meal Expenses");
                                }
                            }
                            if (mClaim >= float.Parse(lblMedical.Text))
                            {
                                if (sb2.ToString().Contains("- Medical"))
                                {

                                }
                                else
                                {
                                    sb2.AppendLine("- Medical");
                                }
                            }
                            if (pAClaim >= float.Parse(lblPA.Text))
                            {
                                if (sb2.ToString().Contains("- Phone Allowance"))
                                {

                                }
                                else
                                {
                                    sb2.AppendLine("- Phone Allowance");
                                }
                            }
                            if (tClaim >= float.Parse(lblTransport.Text))
                            {
                                if (sb2.ToString().Contains("- Transport"))
                                {

                                }
                                else
                                {
                                    sb2.AppendLine("- Transport");
                                }
                            }

                            lblErrorMessage.Text = sb2.ToString().Replace(Environment.NewLine, "<br />");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "openErrorModal", "openErrorModal();", true);
                        }
                    }
                }
            }





        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openConfirmationModal", "$('#openConfirmationModal').newmodal('hide');", true);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int employeeId = int.Parse(Session["ID"].ToString());
            string groupTitle = lblTitle.Text;
            string dateFrom = lblDateFrom.Text;
            string dateTo = lblDateTo.Text;
            IDictionary<int, HttpPostedFile> test = (Dictionary<int, HttpPostedFile>)Session["filepath"];
            int result = 0;
            GroupClaim c = new GroupClaim(employeeId, dateFrom, dateTo, "Pending");
            result = c.claimInsert();
            if (result > 0)
            {
                int claimId = aGroupClaim.GetClaimId();
                int result1 = 0;
                for (int i = 0; i < gv_Details.Rows.Count; i++)
                {
                    Label claimType = (Label)gv_Details.Rows[i].Cells[0].FindControl("lblType");
                    Label amt = (Label)gv_Details.Rows[i].Cells[0].FindControl("lblAmt");
                    Label description = (Label)gv_Details.Rows[i].Cells[0].FindControl("lblDescription");
                    string claimT = claimType.Text;
                    string des = description.Text;
                    float amount = float.Parse(amt.Text);
                    HttpPostedFile file = test[i];
                    string filename = file.FileName;

                    //HtmlInputFile attachment = gvMultiClaim.Rows[0].Cells[3].FindControl("fulAtt") as HtmlInputFile;

                    string appPath = Request.PhysicalApplicationPath;
                    string saveDir = "\\GroupClaim\\";
                    string savePath = appPath + saveDir + Server.HtmlEncode(file.FileName);

                    file.SaveAs(savePath);        // file path where you want to upload

                    GroupClaim g = new GroupClaim(claimId, claimT, groupTitle, des, filename, amount);
                    result1 = g.groupClaimInsert();
                }
                if (result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModal", "openSuccessModal() ;", true);
                }
            }


        }

        protected void btnAcknowledge_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimSummary.aspx");
        }


        protected void lblAttOnClick(object sender, EventArgs e)
        {
            count = 1;
            AddNewRowToGrid();

            LinkButton attbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)attbtn.NamingContainer;
            int rowIndex = row.RowIndex;

            IDictionary<int, HttpPostedFile> test = (Dictionary<int, HttpPostedFile>)Session["filepath"];
            HtmlInputFile attachment = gvMultiClaim.Rows[rowIndex].Cells[3].FindControl("fulAtt") as HtmlInputFile;
            if (attachment.PostedFile.FileName != "")
            {
                string appPath = Request.PhysicalApplicationPath;
                string saveDir = "\\GroupClaim\\";
                string savePath = appPath + saveDir + Server.HtmlEncode(attachment.PostedFile.FileName);

                attachment.PostedFile.SaveAs(savePath);
                //Response.Redirect("\\GroupClaim\\" + attachment.PostedFile.FileName);
                //Response.Redirect("window.open('\\GroupClaim\\'"+ attachment.PostedFile.FileName + ", '_newtab');");
                //Response.Write("</script>");
                OpenNewBrowserWindow("\\GroupClaim\\\\" + attachment.PostedFile.FileName, this);
            }
            else
            {
                HttpPostedFile file = test[rowIndex];
                string appPath = Request.PhysicalApplicationPath;
                string saveDir = "\\GroupClaim\\";
                string savePath = appPath + saveDir + Server.HtmlEncode(file.FileName);

                file.SaveAs(savePath);
                //Response.Redirect("\\GroupClaim\\" + file.FileName);
                //Response.Redirect("window.open('\\GroupClaim\\'" + file.FileName + ", '_newtab');");
                OpenNewBrowserWindow("\\GroupClaim\\\\" + file.FileName, this);
            }


        }

        public static void OpenNewBrowserWindow(string Url, Control control)

        {

            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "');", true);

        }
    }
}
