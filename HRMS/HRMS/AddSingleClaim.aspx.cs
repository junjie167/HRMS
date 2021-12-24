using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS.DAL;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace HRMS
{
    public partial class AddSingleClaim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                ClaimQuota claimQuota = new ClaimQuota();
                claim_type.DataTextField = "claimType";
                claim_type.DataValueField = "quotaAmount";
                claim_type.DataSource = claimQuota.retrieveClaimQuota();
                claim_type.DataBind();
                claim_type.Items.Insert(0, new ListItem("Please select", "0"));
                claim_type.Items[0].Selected = true;

                int id = int.Parse(Session["ID"].ToString());

                lblmeal.Text = String.Format("{0:n}", claimQuota.GetMEQ(id));
                lblmedical.Text = String.Format("{0:n}", claimQuota.GetMediQ(id));
                lblphone.Text = String.Format("{0:n}", claimQuota.GetPAQ(id));
                lbltransport.Text = String.Format("{0:n}", claimQuota.GetTranQ(id));

            }
 



        }

        protected void submit_Click(object sender, EventArgs e)
        {
            int emptyError = 0;
            StringBuilder emptyField = new StringBuilder();

            if (FileUpload1.HasFile)
            {
                lbtnAtt.Text = FileUpload1.FileName;
                Session["filename"] = FileUpload1;
            }

            if (claim_type.SelectedItem.Text == "Please select")
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
            if (claim_description.Text == "")
            {
                emptyField.AppendLine("- " + "Claim Description");
                emptyError = 1;
            }
            if (amount.Text == "")
            {
                emptyField.AppendLine("- " + "Claim Amount");
                emptyError = 1;
            }
            if (!FileUpload1.HasFile && lbtnAtt.Text == "")
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
                Boolean Amounterror = regex.IsMatch(amount.Text);

                if (!Amounterror)
                {
                    otherErrorMsg.AppendLine("- " + "Invalid Claim amount input");
                    otherError = 1;

                }else if (float.Parse(amount.Text) <= 0.0f)
                {
                    otherErrorMsg.AppendLine("- " + "Claim amount cannot be 0");
                    otherError = 1;
                }
                else if (claim_type.SelectedItem.Text == "Meal Expenses" && float.Parse(amount.Text) > float.Parse(lblmeal.Text))
                {
                    //Claim Quota
                    otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                    otherError = 1;
                }else if (claim_type.SelectedItem.Text == "Medical" && float.Parse(amount.Text) > float.Parse(lblmedical.Text))
                {
                    otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                    otherError = 1;
                }else if (claim_type.SelectedItem.Text == "Phone Allowance" && float.Parse(amount.Text) > float.Parse(lblphone.Text))
                {
                    otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                    otherError = 1;
                }else if (claim_type.SelectedItem.Text == "Transport" && float.Parse(amount.Text) > float.Parse(lbltransport.Text))
                {
                    otherErrorMsg.AppendLine("- " + "The claim amount that going to apply is more than the remaining claim amount");
                    otherError = 1;
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
                    confirm_claimtype.Text = claim_type.SelectedItem.Text;
                    confrim_fromdate.Text = from_date.Text;
                    confirm_todate.Text = to_date.Text;
                    confirm_description.Text = claim_description.Text;
                    confirm_amount.Text = amount.Text;
                    if (FileUpload1.HasFile)
                    {
                        confirm_attachment.Text = FileUpload1.FileName;
                        Session["filename"] = FileUpload1;
                        lbtnAtt.Text = FileUpload1.FileName;
                    }
                    else
                    {
                        FileUpload file = (FileUpload)Session["filename"];
                        confirm_attachment.Text = file.FileName;
                    }          
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "openConfirmationModal", "openConfirmationModal();", true);
                }
                
            }

           

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openConfirmationModal", "$('#openConfirmationModal').newmodal('hide');", true);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Session["ID"].ToString());
            string type = confirm_claimtype.Text;
            string fromDate = confrim_fromdate.Text;
            string toDate = confirm_todate.Text;
            string claimDescription = confirm_description.Text;
            float claimAmount = float.Parse(confirm_amount.Text);
            string claimAttachment = confirm_attachment.Text;

            string appPath = Request.PhysicalApplicationPath;
            string saveDir = "\\SingleClaimAttachment\\";
            FileUpload file = (FileUpload)Session["filename"];
            string savePath = appPath + saveDir + Server.HtmlEncode(file.FileName);
            SingleClaim singleclaim = new SingleClaim();
            int result = singleclaim.createSingleClaims(id, fromDate, toDate, type, claimDescription, claimAmount, claimAttachment);

            if (result == 1)
            {
                file.SaveAs(savePath);

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModal", "openSuccessModal() ;", true);
        }

        protected void btnAcknowledge_Click(object sender, EventArgs e)
        {

            Response.Redirect("ClaimSummary.aspx");

        }

        protected void back_Click(object sender, EventArgs e)
        {
            if (claim_type.SelectedItem.Text != "Please select")
            {
                claim_type.SelectedItem.Selected = false;
                claim_type.Items[0].Selected = true;
            }
            
            from_date.Text = "";
            to_date.Text = "";
            claim_description.Text = "";
            amount.Text = "";
            lbtnAtt.Text = "";
        }

        protected void attachmentLinkbtn_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string appPath = Request.PhysicalApplicationPath;
                string saveDir = "\\SingleClaimAttachment\\";
                string savePath = appPath + saveDir + Server.HtmlEncode(FileUpload1.FileName);
                Session["filename"] = FileUpload1;
                lbtnAtt.Text = FileUpload1.FileName;
                FileUpload1.SaveAs(savePath);
                OpenNewBrowserWindow("\\SingleClaimAttachment\\\\" + FileUpload1.FileName, this);
            }
            else
            {
                FileUpload file = (FileUpload)Session["filename"];
                string appPath = Request.PhysicalApplicationPath;
                string saveDir = "\\SingleClaimAttachment\\";
                string savePath = appPath + saveDir + Server.HtmlEncode(file.FileName);
                lbtnAtt.Text = file.FileName;
                file.SaveAs(savePath);
                OpenNewBrowserWindow("\\SingleClaimAttachment\\\\" + file.FileName, this);
            }
            
            

            
        }

        public static void OpenNewBrowserWindow(string Url, Control control)

        {

            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "');", true);

        }
    }


}