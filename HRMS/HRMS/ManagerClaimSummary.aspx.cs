using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS.DAL;
using System.Web.UI.HtmlControls;

namespace HRMS
{
    public partial class ManagerClaimSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                sortclaim.Items[0].Selected = true;
                filterclaim.Items[0].Selected = true;
                BindClaim();
            }

        }

        private void BindClaim()
        {
            int employeeID = int.Parse(Session["ID"].ToString());
            ManagerClaim managerClaim = new ManagerClaim();
            List<ManagerClaim> managerClaimsList = new List<ManagerClaim>();
            managerClaimsList = managerClaim.filterClaimReviewByStatus(employeeID, filterclaim.SelectedValue, sortclaim.SelectedValue);
            
            if (managerClaimsList.Count > 0)
            {
                noclaimview.Visible = false;
                claimsGV.DataSource = managerClaimsList;
                claimsGV.DataBind();

                for (int i = 0; i < claimsGV.Rows.Count; i++)
                {
                    Label statuslbl = (Label)claimsGV.Rows[i].Cells[0].FindControl("status");
                    if (statuslbl.Text == "Pending")
                    {
                        HtmlGenericControl div = (HtmlGenericControl)claimsGV.Rows[i].Cells[0].FindControl("cardcolor");
                        div.Attributes.Add("class", "card shadow mt-2 border-left-warning");

                    }
                    else if (statuslbl.Text == "Approved")
                    {
                        HtmlGenericControl div = (HtmlGenericControl)claimsGV.Rows[i].Cells[0].FindControl("cardcolor");
                        div.Attributes.Add("class", "card shadow mt-2 border-left-success");
                    }
                    else
                    {
                        HtmlGenericControl div = (HtmlGenericControl)claimsGV.Rows[i].Cells[0].FindControl("cardcolor");
                        div.Attributes.Add("class", "card shadow mt-2 border-left-danger");
                    }

                    Label claimTy = (Label)claimsGV.Rows[i].Cells[0].FindControl("claimType");
                    if (claimTy.Text != "")
                    {
                        Label lblclaimTy = (Label)claimsGV.Rows[i].Cells[0].FindControl("lblclaimType");
                        lblclaimTy.Visible = true;
                    }
                    else
                    {

                        Label lblclaimTi = (Label)claimsGV.Rows[i].Cells[0].FindControl("lblclaimTitle");
                        lblclaimTi.Visible = true;

                    }
                }
            }
            else
            {
                noclaimview.Visible = true;
            }

        }

        protected void claimsGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            claimsGV.PageIndex = e.NewPageIndex;
            BindClaim();
        }

        protected void pageEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            claimsGV.PageSize = int.Parse(pageEntries.SelectedValue);
            BindClaim();
        }

        protected void sortclaim_SelectedIndexChanged(object sender, EventArgs e)
        {


            BindClaim();

        }

        private void filterOldest()
        {
            int employeeID = int.Parse(Session["ID"].ToString());
            ManagerClaim managerClaim = new ManagerClaim();
            List<ManagerClaim> managerClaimsList = new List<ManagerClaim>();

            managerClaimsList = managerClaim.filterClaimReviewByStatus(employeeID,filterclaim.SelectedValue,sortclaim.SelectedValue);
            claimsGV.DataSource = managerClaimsList;
            claimsGV.DataBind();

            for (int i = 0; i < claimsGV.Rows.Count; i++)
            {
                Label statuslbl = (Label)claimsGV.Rows[i].Cells[0].FindControl("status");
                if (statuslbl.Text == "Pending")
                {
                    HtmlGenericControl div = (HtmlGenericControl)claimsGV.Rows[i].Cells[0].FindControl("cardcolor");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-warning");

                }
                else if (statuslbl.Text == "Approved")
                {
                    HtmlGenericControl div = (HtmlGenericControl)claimsGV.Rows[i].Cells[0].FindControl("cardcolor");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-success");
                }
                else
                {
                    HtmlGenericControl div = (HtmlGenericControl)claimsGV.Rows[i].Cells[0].FindControl("cardcolor");
                    div.Attributes.Add("class", "card shadow mt-2 border-left-danger");
                }

                Label claimTy = (Label)claimsGV.Rows[i].Cells[0].FindControl("claimType");
                if (claimTy.Text != "")
                {
                    Label lblclaimTy = (Label)claimsGV.Rows[i].Cells[0].FindControl("lblclaimType");
                    lblclaimTy.Visible = true;
                }
                else
                {

                    Label lblclaimTi = (Label)claimsGV.Rows[i].Cells[0].FindControl("lblclaimTitle");
                    lblclaimTi.Visible = true;

                }
            }
        }

        protected void viewClaim_Click(object sender, EventArgs e)
        {
            Button viewbtn = (Button)sender;
            GridViewRow row = (GridViewRow)viewbtn.NamingContainer;
            int rowIndex = row.RowIndex;
            Label claim_id = (Label)claimsGV.Rows[rowIndex].Cells[0].FindControl("claimID");
            string id = claim_id.Text;
            Session["ViewclaimID"] = id;
            Response.Redirect("ManagerViewClaims.aspx");

        }

        protected void filterclaim_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClaim();
        }
    }
}