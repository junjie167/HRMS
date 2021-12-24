using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS.DAL;

namespace HRMS
{
    public partial class ManagerViewClaims : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string claimID = Session["ViewclaimID"].ToString();
                lblclaimID.Text = "Claim ID: #" + claimID;
                lblgroupClaimID.Text = "Claim ID: #" + claimID;
                int id = int.Parse(claimID);

                ManagerClaim managerClaim = new ManagerClaim();
                List<ManagerClaim> managerClaimsList = new List<ManagerClaim>();
                managerClaimsList = managerClaim.retrieveViewClaim(id);
                if (managerClaimsList[0].ClaimOption == "Single")
                {
                    singlePanel.Visible = true;
                    claimoption.Text = managerClaimsList[0].ClaimOption;
                    claimType.Text = managerClaimsList[0].ClaimType;
                    fromdate.Text = managerClaimsList[0].DateFrom;
                    todate.Text = managerClaimsList[0].DateTo;
                    description.Text = managerClaimsList[0].Description;
                    amount.Text = String.Format("{0:.00}", managerClaimsList[0].Amount);
                    attachment.Text = managerClaimsList[0].Attachment;
                    if (managerClaimsList[0].Status != "Pending")
                    {
                        btnReject.Visible = false;
                        btnApprove.Visible = false;
                        btnBackView.Visible = true;
                    }
                }
                else
                {
                    groupPanel.Visible = true;
                    singlePanel.Visible = false;
                    groupclaimOption.Text = managerClaimsList[0].ClaimOption;
                    title.Text = managerClaimsList[0].Title;
                    groupFromDate.Text = managerClaimsList[0].DateFrom;
                    groupToDate.Text = managerClaimsList[0].DateTo;
                    groupGV.DataSource = managerClaimsList;
                    groupGV.DataBind();
                    if (managerClaimsList[0].Status != "Pending")
                    {
                        btnReject.Visible = false;
                        btnApprove.Visible = false;
                        btnBackView.Visible = true;
                    }
                    groupGV.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    //Attribute to hide column in Phone.
                    groupGV.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                    groupGV.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                    //Adds THEAD and TBODY to GridView.
                    groupGV.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

        }


        protected void attachment_Click(object sender, EventArgs e)
        {

            OpenNewBrowserWindow("\\SingleClaimAttachment\\\\" + attachment.Text, this);
        }

        public static void OpenNewBrowserWindow(string Url, Control control)

        {

            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "');", true);

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openRejectModal", "openRejectModal() ;", true);
        }

        protected void submitReject_Click(object sender, EventArgs e)
        {
            confirmReject.Text = rejectTxtbox.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openRejectConfrimModal", "openRejectConfrimModal() ;", true);
        }

        protected void confirmRejectBtn_Click(object sender, EventArgs e)
        {
            ManagerClaim managerClaim = new ManagerClaim();
            int claimID = int.Parse(Session["ViewclaimID"].ToString());
            int employeeID = 300;
            int result = managerClaim.updateStatusReject(employeeID, claimID, confirmReject.Text);
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openRejectSuccessModal", "openRejectSuccessModal();", true);
                
            }
        }

        protected void btnAcknowledgeReject_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerClaimSummary.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            ManagerClaim managerClaim = new ManagerClaim();
            int claimID = int.Parse(Session["ViewclaimID"].ToString());
            int employeeID = 300;
            int result = managerClaim.updateStatusApprove(employeeID, claimID);
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessApproveModalModal()", "openSuccessApproveModalModal();", true);
                
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openWarningApproveModalModal", "openWarningApproveModalModal();", true);
        }

        protected void btnBackView_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerClaimSummary.aspx");
        }

        protected void viewGroupAttachment_Click(object sender, EventArgs e)
        {

            LinkButton groupLinkBtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)groupLinkBtn.NamingContainer;
            int rowIndex = row.RowIndex;
            LinkButton groupAttachment = (LinkButton)groupGV.Rows[rowIndex].Cells[0].FindControl("viewGroupAttachment");
            OpenNewBrowserWindow("\\SingleClaimAttachment\\\\" + groupAttachment.Text, this);
            groupGV.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
            //Attribute to hide column in Phone.
            groupGV.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            groupGV.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            groupGV.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}