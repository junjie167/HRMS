using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS.DAL;
using System.Web.UI.HtmlControls;

namespace HRMS
{
    public partial class ClaimSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int id = int.Parse(Session["ID"].ToString());
                ClaimQuota claimQuota = new ClaimQuota();
                float medical = claimQuota.GetMediQ(id);
                float meal = claimQuota.GetMEQ(id);
                float phone = claimQuota.GetPAQ(id);
                float transport = claimQuota.GetTranQ(id);

                float total_remaining = medical + meal + phone + transport;
                lbltotalclaim.Text = String.Format("{0:n}", total_remaining);

                SingleClaim singleClaim = new SingleClaim();
                float total_approve = singleClaim.retrieveSingleApprovlAmt(id);
                if (total_approve == 0.0)
                {
                    lbltotalAvailable.Text = "0.00";
                }
                else
                {
                    lbltotalAvailable.Text = String.Format("{0:n}", total_approve);
                }
                

                BindSingleClaim();

                

                

                BindGroupClaim();
                int buttonClick = 0;
                Session["button"] = buttonClick;

                gvGroupClaim.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                gvGroupClaim.Columns[0].ItemStyle.VerticalAlign = VerticalAlign.Middle;
            }


            

        }

        private void BindSingleClaim()
        {
            int id = int.Parse(Session["ID"].ToString());
            SingleClaim singleClaim = new SingleClaim();
            List<SingleClaim> singleClaimList = new List<SingleClaim>();
            singleClaimList = singleClaim.retrieveSingleClaimsSummary(id);

            if (singleClaimList.Count > 0)
            {
                nosingleclaim.Visible = false;
                gvSingleClaim.DataSource = singleClaimList;
                gvSingleClaim.DataBind();

                gvSingleClaim.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.
                gvSingleClaim.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
               
                //Adds THEAD and TBODY to GridView.
                gvSingleClaim.HeaderRow.TableSection = TableRowSection.TableHeader;


            }
            else
            {
                nosingleclaim.Visible = true;
            }
            

            for (int i = 0; i < gvSingleClaim.Rows.Count; i++)
            {
                Label SingleStatus = (Label)gvSingleClaim.Rows[i].Cells[4].FindControl("lblclaimStatus");
                if (SingleStatus.Text == "Approved")
                {
                    Button updatebtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("singleupdatebtn");
                    Button cancelbtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("c");

                    updatebtn.Visible = false;
                    cancelbtn.Visible = false;

                }else if (SingleStatus.Text == "Rejected")
                {
                    Button updatebtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("singleupdatebtn");
                    Button cancelbtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("c");
                    updatebtn.Visible = false;
                    cancelbtn.Visible = false;
                    Button viewbtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("viewSingleReject");
                    viewbtn.Visible = true;
                }
            }
        }

        private void BindGroupClaim()
        {
            int employeeID = int.Parse(Session["ID"].ToString());
            GroupClaim gClaim = new GroupClaim();
            List<GroupClaim> gClaimList = new List<GroupClaim>();
            gClaimList = gClaim.GetGroupClaims(employeeID);

            if (gClaimList.Count > 0)
            {
                nogroupclaim.Visible = false;
                gvGroupClaim.DataSource = gClaimList;
                gvGroupClaim.DataBind();

            }
            else
            {
                nogroupclaim.Visible = true;
            }
           
            for (int i = 0; i < gvGroupClaim.Rows.Count; i++)
            {
                Label GroupStatus = (Label)gvGroupClaim.Rows[i].Cells[0].FindControl("lblStatus");
                if (GroupStatus.Text == "Approved")
                {
                    Button updatebtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("btnUpdate");
                    Button cancelbtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("btnCancel");

                    updatebtn.Visible = false;
                    cancelbtn.Visible = false;

                    HtmlGenericControl div = (HtmlGenericControl)gvGroupClaim.Rows[i].Cells[0].FindControl("groupgrid");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-success");

                }
                else if(GroupStatus.Text == "Rejected")
                {
                    Button updatebtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("btnUpdate");
                    Button cancelbtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("btnCancel");
                    Button viewbtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("viewGroupReject");

                    updatebtn.Visible = false;
                    cancelbtn.Visible = false;
                    viewbtn.Visible = true;

                    HtmlGenericControl div = (HtmlGenericControl)gvGroupClaim.Rows[i].Cells[0].FindControl("groupgrid");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-danger");

                }else if (GroupStatus.Text == "Pending")
                {
                    HtmlGenericControl div = (HtmlGenericControl)gvGroupClaim.Rows[i].Cells[0].FindControl("groupgrid");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-warning");
                }
            }
        }

        protected void imgDetail_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton b = (ImageButton)sender;
            GridViewRow row = (GridViewRow)b.NamingContainer;
            int rowIndex = row.RowIndex;
            Label claimId = (Label)gvGroupClaim.Rows[rowIndex].Cells[0].FindControl("lblCliamId");
            int buttonClick = int.Parse(Session["button"].ToString());
            if (b.ImageUrl == "images/dropdown.png")
            {
                b.ImageUrl = "images/dropup.png";
                Session["button"] = 1;
                GroupClaim gClaim = new GroupClaim();
                List<GroupClaim> gClaimList = new List<GroupClaim>();
                gClaimList = gClaim.GetGroupClaimsDetails(int.Parse(claimId.Text));
                GridView details = (GridView)gvGroupClaim.Rows[rowIndex].Cells[0].FindControl("gvDetails");
                details.DataSource = gClaimList;
                details.DataBind();

            }
            else
            {
                b.ImageUrl = "images/dropdown.png";
                Session["button"] = 0;
                GridView details = (GridView)gvGroupClaim.Rows[rowIndex].Cells[0].FindControl("gvDetails");
                details.DataSource = null;
                details.DataBind();

            }

            if (gvSingleClaim.Rows.Count > 0)
            {
                gvSingleClaim.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.
                gvSingleClaim.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
           
                //Adds THEAD and TBODY to GridView.
                gvSingleClaim.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        protected void gvSingleClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSingleClaim.PageIndex = e.NewPageIndex;
            BindSingleClaim();
        }

        protected void btnApplyClaim_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openConfirmationModal", "openConfirmationModal();", true);
        }

        protected void applySingle_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddSingleClaim.aspx");
        }

        protected void SingleUpdate_Click(object sender, EventArgs e)
        {
            Button updatebtn = (Button)sender;
            GridViewRow row = (GridViewRow)updatebtn.NamingContainer;
            int rowIndex = row.RowIndex;
            Label claim_id = (Label)gvSingleClaim.Rows[rowIndex].Cells[0].FindControl("lblclaimID");
            string id = claim_id.Text;
            Session["claimID"] = id;
            Response.Redirect("EditSingleClaim.aspx");
        }

        protected void SingleDelete_Click1(object sender, EventArgs e)
        {
            Button deletebtn = (Button)sender;
            GridViewRow row = (GridViewRow)deletebtn.NamingContainer;
            int rowIndex = row.RowIndex;
            Label claim_id = (Label)gvSingleClaim.Rows[rowIndex].Cells[0].FindControl("lblclaimID");
            string id = claim_id.Text;
            Session["claimID"] = id;
            /*  Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteConfirmationModal", "deleteConfirmationModal();", true);*/
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "deleteConfirmationModal", "deleteConfirmationModal();", true);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Session["claimID"].ToString());
            SingleClaim singleClaim = new SingleClaim();
            singleClaim.DeleteSingleClaim(id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModal", "openSuccessModal() ;", true);
        }

        protected void btnAcknowledge_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimSummary.aspx");
        }

        protected void gvGroupClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroupClaim.PageIndex = e.NewPageIndex;
            BindGroupClaim();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Button deletebtn = (Button)sender;
            GridViewRow row = (GridViewRow)deletebtn.NamingContainer;
            int rowIndex = row.RowIndex;
            Label claim_id = (Label)gvGroupClaim.Rows[rowIndex].Cells[0].FindControl("lblCliamId");
            string id = claim_id.Text;
            Session["claimID"] = id;
            Response.Redirect("EditMultipleClaim.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Button deletebtn = (Button)sender;
            GridViewRow row = (GridViewRow)deletebtn.NamingContainer;
            int rowIndex = row.RowIndex;
            Label claim_id = (Label)gvGroupClaim.Rows[rowIndex].Cells[0].FindControl("lblCliamId");
            string id = claim_id.Text;
            Session["claimID"] = id;

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "deleteGroupConfirmationModal", "deleteGroupConfirmationModal();", true);

        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Session["claimID"].ToString());
            GroupClaim gClaim = new GroupClaim();
            gClaim.DeleteClaim(id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModal", "openSuccessModal() ;", true);
        }

        protected void applyMultiple_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddMultipleClaim.aspx");
        }

        protected void singleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int employeeID = int.Parse(Session["ID"].ToString());
            SingleClaim singleClaim = new SingleClaim();
            List<SingleClaim> singleClaimList = new List<SingleClaim>();
            singleClaimList = singleClaim.filterSingleClaimByStatus(employeeID, singleFilter.SelectedValue);

            if (singleFilter.SelectedValue == "Approved" && singleClaimList.Count <= 0)
            {
                noSingleAprove.Visible = true;
                norejectedsingle.Visible = false;
                noPending.Visible = false;
            }
            else if (singleFilter.SelectedValue == "Pending" && singleClaimList.Count <= 0)
            {
                noPending.Visible = true;
                noSingleAprove.Visible = false;
                norejectedsingle.Visible = false;
            }
            else if (singleFilter.SelectedValue == "Rejected" && singleClaimList.Count <= 0)
            {
                noSingleAprove.Visible = false;
                norejectedsingle.Visible = true;
                noPending.Visible = false;
            }
            else
            {
                norejectedsingle.Visible = false;
                noPending.Visible = false;
                noSingleAprove.Visible = false;
            }

            gvSingleClaim.DataSource = singleClaimList;
            gvSingleClaim.DataBind();

            for (int i = 0; i < gvSingleClaim.Rows.Count; i++)
            {
                Label SingleStatus = (Label)gvSingleClaim.Rows[i].Cells[4].FindControl("lblclaimStatus");
                if (SingleStatus.Text == "Approved")
                {
                    Button updatebtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("singleupdatebtn");
                    Button cancelbtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("c");

                    updatebtn.Visible = false;
                    cancelbtn.Visible = false;

                }
                else if (SingleStatus.Text == "Rejected")
                {
                    Button updatebtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("singleupdatebtn");
                    Button cancelbtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("c");
                    updatebtn.Visible = false;
                    cancelbtn.Visible = false;
                    Button viewbtn = (Button)gvSingleClaim.Rows[i].Cells[5].FindControl("viewSingleReject");
                    viewbtn.Visible = true;
                }
            }

            if (singleClaimList.Count > 0)
            {
                gvSingleClaim.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.
                gvSingleClaim.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvSingleClaim.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            


        }

        protected void groupFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int employeeID = int.Parse(Session["ID"].ToString());
            GroupClaim groupClaim = new GroupClaim();
            List<GroupClaim> groupClaimsList = new List<GroupClaim>();
            groupClaimsList = groupClaim.FilterGroupByStatus(employeeID, groupFilter.SelectedValue);

            if (groupFilter.SelectedValue == "Approved" && groupClaimsList.Count <= 0)
            {
                noGroupApprove.Visible = true;
                noGroupPending.Visible = false;
                noGroupReject.Visible = false;

            }
            else if (groupFilter.SelectedValue == "Pending" && groupClaimsList.Count <= 0)
            {
                noGroupPending.Visible = true;
                noGroupApprove.Visible = false;
                noGroupReject.Visible = false;

            }
            else if (groupFilter.SelectedValue == "Rejected" && groupClaimsList.Count <= 0)
            {
                noGroupApprove.Visible = false;
                noGroupPending.Visible = false;
                noGroupReject.Visible = true;
            }
            else
            {
                noGroupApprove.Visible = false;
                noGroupPending.Visible = false;
                noGroupReject.Visible = false;
            }

            gvGroupClaim.DataSource = groupClaimsList;
            gvGroupClaim.DataBind();

            for (int i = 0; i < gvGroupClaim.Rows.Count; i++)
            {
                Label GroupStatus = (Label)gvGroupClaim.Rows[i].Cells[0].FindControl("lblStatus");
                if (GroupStatus.Text == "Approved")
                {
                    Button updatebtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("btnUpdate");
                    Button cancelbtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("btnCancel");

                    updatebtn.Visible = false;
                    cancelbtn.Visible = false;

                    HtmlGenericControl div = (HtmlGenericControl)gvGroupClaim.Rows[i].Cells[0].FindControl("groupgrid");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-success");
                }
                else if (GroupStatus.Text == "Rejected")
                {
                    Button updatebtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("btnUpdate");
                    Button cancelbtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("btnCancel");
                    Button viewbtn = (Button)gvGroupClaim.Rows[i].Cells[0].FindControl("viewGroupReject");

                    updatebtn.Visible = false;
                    cancelbtn.Visible = false;
                    viewbtn.Visible = true;

                    HtmlGenericControl div = (HtmlGenericControl)gvGroupClaim.Rows[i].Cells[0].FindControl("groupgrid");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-danger");
                }
                else if (GroupStatus.Text == "Pending")
                {
                    HtmlGenericControl div = (HtmlGenericControl)gvGroupClaim.Rows[i].Cells[0].FindControl("groupgrid");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-warning");
                }
            }

            if (gvSingleClaim.Rows.Count > 0)
            {
                gvSingleClaim.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.
                gvSingleClaim.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvSingleClaim.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvSingleClaim.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void viewSingleReject_Click(object sender, EventArgs e)
        {
            SingleClaim singleClaim = new SingleClaim();
            Button viewbtn = (Button)sender;
            GridViewRow row = (GridViewRow)viewbtn.NamingContainer;
            int rowIndex = row.RowIndex;
            Label claim_id = (Label)gvSingleClaim.Rows[rowIndex].Cells[0].FindControl("lblclaimID");
            string reason = singleClaim.getSingleRejectReason(int.Parse(claim_id.Text));
            singleRejectReason.Text = reason;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opensingleRejectModal", "opensingleRejectModal() ;", true);
            
        }

        protected void viewGroupReject_Click(object sender, EventArgs e)
        {
            Button viewbtn = (Button)sender;
            GridViewRow row = (GridViewRow)viewbtn.NamingContainer;
            int rowIndex = row.RowIndex;
            Label claim_id = (Label)gvGroupClaim.Rows[rowIndex].Cells[0].FindControl("lblCliamId");
            GroupClaim groupClaim = new GroupClaim();
            string reason = groupClaim.getGroupRejectReason(int.Parse(claim_id.Text));
            groupRejectReason.Text = reason;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opengroupRejectModal", "opengroupRejectModal() ;", true);
            
        }
    }
}