using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS.DAL;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.IO;

namespace HRMS
{
    public partial class EditSingleClaim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                int employeeID = int.Parse(Session["ID"].ToString());
                ClaimQuota claimQuota = new ClaimQuota();
                claimTypelist.DataTextField = "claimType";
                claimTypelist.DataValueField = "quotaAmount";
                claimTypelist.DataSource = claimQuota.retrieveClaimQuota();
                claimTypelist.DataBind();

                lblmeal.Text = String.Format("{0:n}", claimQuota.GetMEQ(employeeID));
                lblmedical.Text = String.Format("{0:n}", claimQuota.GetMediQ(employeeID));
                lblphone.Text = String.Format("{0:n}", claimQuota.GetPAQ(employeeID));
                lbltransport.Text = String.Format("{0:n}", claimQuota.GetTranQ(employeeID));



                int cid = int.Parse(Session["claimID"].ToString());
                SingleClaim singleclaim = new SingleClaim();
                List<SingleClaim> singleclaimList = new List<SingleClaim>();
                singleclaimList = singleclaim.retrieveSingleClaimToEdit(cid);
                for (int i=0; i < claimTypelist.Items.Count; i++)
                {
                    if (claimTypelist.Items[i].Text == singleclaimList[0].ClaimType)
                    {
                        claimTypelist.Items[i].Selected = true;
                        Session["ct"] = singleclaimList[0].ClaimType;
                    }
                }
                from_date.Text = singleclaimList[0].FromDate;
                to_date.Text = singleclaimList[0].ToDate;
                description.Text = singleclaimList[0].Description;
                claim_amount.Text = singleclaimList[0].claimAmount.ToString();
                Session["amount"] = singleclaimList[0].claimAmount.ToString();
                claim_attachment.Text = singleclaimList[0].Attachment;

            }

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            int emptyError = 0;
            StringBuilder emptyField = new StringBuilder();

            if (claimTypelist.SelectedItem.Text == "Please select")
            {
                emptyField.AppendLine("- " + "Claim type");
                emptyError = 1;
            }
            if (from_date.Text == "")
            {
                emptyField.AppendLine("- " + "Date From");
                emptyError = 1;
            }
            if (to_date.Text == "")
            {
                emptyField.AppendLine("- " + "Date to");
                emptyError = 1;
            }
            if (description.Text == "")
            {
                emptyField.AppendLine("- " + "Claim Description");
                emptyError = 1;
            }
            if (claim_amount.Text == "")
            {
                emptyField.AppendLine("- " + "Claim Amount");
                emptyError = 1;
            }
            if (claim_attachment.Text == "")
            {
                emptyField.AppendLine("- " + "Claim attachment");
                emptyError = 1;
            }

            if (emptyError == 1)
            {
                lblEmptyField.Text = emptyField.ToString().Replace(Environment.NewLine, "<br />");
                ScriptManager.RegisterStartupScript(this, this.GetType(), " openEmptyFieldModal", " openEmptyFieldModal();", true);
            }
            else
            {
                int otherError = 0;
                StringBuilder otherErrorMsg = new StringBuilder();

                //Claim format
                string strRegex = "[+-]?([0-9]*[.])?[0-9]+";
                Regex regex = new Regex(strRegex);
                Boolean Amounterror = regex.IsMatch(claim_amount.Text);

                if (!Amounterror)
                {
                    otherErrorMsg.AppendLine("- " + "Invalid Claim amount input");
                    otherError = 1;

                }
                else if (float.Parse(claim_amount.Text) <= 0.0f)
                {
                    otherErrorMsg.AppendLine("- " + "Claim amount cannot be 0");
                    otherError = 1;
                }
                else
                {
                    if (Session["ct"].ToString() == "Meal Expenses" && float.Parse(claim_amount.Text) > float.Parse(Session["amount"].ToString()) && float.Parse(claim_amount.Text) > float.Parse(lblmeal.Text))
                    {
                        //Claim Quota
                        otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                        otherError = 1;
                    }else if (claimTypelist.SelectedItem.Text == "Meal Expenses" && Session["ct"].ToString() != "Meal Expenses" && float.Parse(claim_amount.Text) > float.Parse(lblmeal.Text))
                    {
                        otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                        otherError = 1;
                    }else if (Session["ct"].ToString() == "Medical" && float.Parse(claim_amount.Text) > float.Parse(Session["amount"].ToString()) && float.Parse(claim_amount.Text) > float.Parse(lblmedical.Text))
                    {
                        otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                        otherError = 1;
                    }
                    else if (claimTypelist.SelectedItem.Text == "Medical" && Session["ct"].ToString() != "Medical" && float.Parse(claim_amount.Text) > float.Parse(lblmedical.Text))
                    {
                        otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                        otherError = 1;
                    }else if (Session["ct"].ToString() == "Phone Allowance" && float.Parse(claim_amount.Text) > float.Parse(Session["amount"].ToString()) && float.Parse(claim_amount.Text) > float.Parse(lblphone.Text))
                    {
                        otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                        otherError = 1;
                    }
                    else if (claimTypelist.SelectedItem.Text == "Phone Allowance" && Session["ct"].ToString() != "Phone Allowance" && float.Parse(claim_amount.Text) > float.Parse(lblphone.Text))
                    {
                        otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                        otherError = 1;
                    }else if (Session["ct"].ToString() == "Transport" && float.Parse(claim_amount.Text) > float.Parse(Session["amount"].ToString()) && float.Parse(claim_amount.Text) > float.Parse(lbltransport.Text))
                    {
                        otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                        otherError = 1;
                    }
                    else if (claimTypelist.SelectedItem.Text == "Transport" && Session["ct"].ToString() != "Transport" && float.Parse(claim_amount.Text) > float.Parse(lbltransport.Text))
                    {
                        otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                        otherError = 1;
                    }
                }

                
                
               

                //Date Format
                string dateRegx = @"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$";
                Regex dateregex = new Regex(dateRegx);
                Boolean fromDateFormatError = dateregex.IsMatch(from_date.Text);
                Boolean toDateFormatError = dateregex.IsMatch(to_date.Text);

                if (!fromDateFormatError || !toDateFormatError)
                {
                    otherErrorMsg.AppendLine("- " + "Invalid Date format");
                    otherError = 1;
                }
                else
                {
                    //Check whether the to_date is bigger than from_date
                    DateTime from = DateTime.ParseExact(from_date.Text, "dd/MM/yyyy", null);
                    DateTime to = DateTime.ParseExact(to_date.Text, "dd/MM/yyyy", null);

                    int check = DateTime.Compare(from, to);

                    if (check > 0)
                    {
                        otherErrorMsg.AppendLine("- " + "Date To cannot be smaller than Date From");
                        otherError = 1;
                    }
                }

                

                if (otherError == 1)
                {
                    lblErrorMessage.Text = otherErrorMsg.ToString().Replace(Environment.NewLine, "<br />");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), " openErrorModal", " openErrorModal();", true);
                }
                else
                {
                    confirm_claimtype.Text = claimTypelist.SelectedItem.Text;
                    confrim_fromdate.Text = from_date.Text;
                    confirm_todate.Text = to_date.Text;
                    confirm_description.Text = description.Text;
                    confirm_amount.Text = claim_amount.Text;
                    if (fulAtt.PostedFile.FileName != "")
                    {
                        confirm_attachment.Text = fulAtt.PostedFile.FileName;
                        Session["filename"] = fulAtt;
                    }
                    else
                    {
                        confirm_attachment.Text = claim_attachment.Text;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "openConfirmationModal", "openConfirmationModal();", true);
                }

            }


        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int cid = int.Parse(Session["claimID"].ToString());
            string type = confirm_claimtype.Text;
            string fromDate = confrim_fromdate.Text;
            string toDate = confirm_todate.Text;
            string claimDescription = confirm_description.Text;
            float claimAmount = float.Parse(confirm_amount.Text);
            string claimAttachment = confirm_attachment.Text;
            string appPath = Request.PhysicalApplicationPath;
            string saveDir = "\\SingleClaimAttachment\\";
            string checkpath = appPath + saveDir + claimAttachment;
            SingleClaim singleClaim = new SingleClaim();
            if (File.Exists(checkpath)){
                singleClaim.updateSingleClaim(type, fromDate, toDate, claimDescription, claimAmount, claimAttachment, cid);
            }
            else
            {
                HtmlInputFile file = (HtmlInputFile)Session["filename"];
                string savePath = appPath + saveDir + Server.HtmlEncode(file.PostedFile.FileName);
                int result = singleClaim.updateSingleClaim(type, fromDate, toDate, claimDescription, claimAmount, claimAttachment, cid);
                if (result == 1)
                {
                    file.PostedFile.SaveAs(savePath);

                }
            }
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModal", "openSuccessModal() ;", true);
        }

        protected void btnAcknowledge_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimSummary.aspx");
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimSummary.aspx");
        }

        protected void claim_attachment_Click(object sender, EventArgs e)
        {
            HtmlInputFile file = (HtmlInputFile)fulAtt;
            if (fulAtt.PostedFile.FileName == "")
            {
                OpenNewBrowserWindow("\\SingleClaimAttachment\\\\" + claim_attachment.Text, this);
               
            }
            else
            {
                string appPath = Request.PhysicalApplicationPath;
                string saveDir = "\\SingleClaimAttachment\\";
                string savePath = appPath + saveDir + Server.HtmlEncode(file.PostedFile.FileName);
                claim_attachment.Text = file.PostedFile.FileName;
                file.PostedFile.SaveAs(savePath);
                OpenNewBrowserWindow("\\SingleClaimAttachment\\\\" + file.PostedFile.FileName, this);

            }
        }

        public static void OpenNewBrowserWindow(string Url, Control control)

        {

            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "');", true);

        }


    }
}