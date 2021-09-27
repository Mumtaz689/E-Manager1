using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class mailcategory : System.Web.UI.Page
{
    cls_category objcategory = new cls_category();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            bindgrid();
            ViewState["flag"] = 0;
            ViewState["id"] = 0;
        }

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try {

            if( txtcategory.Text.Trim()==string.Empty)
            {
                lblmessage.Text = "Mail Category should not be blank";
            return;
            }


            if (ViewState["flag"].ToString() == "0")
            {

                if (objcategory.add_update_category(1, txtcategory.Text.Trim(), Convert.ToInt32(rdactive.SelectedValue), 0) == true)
                {
                    lblmessage.Text = "Mail category created successfully";

                }
                else
                {
                    lblmessage.Text = "Mail category not created";
                }
            }
            if (ViewState["flag"].ToString() == "1")
            {
                if (objcategory.add_update_category(2, txtcategory.Text.Trim(),Convert.ToInt32(rdactive.SelectedValue), Convert.ToInt32(ViewState["id"].ToString())) == true)
                {
                    lblmessage.Text = "Mail category updated successfully";

                }
                else
                {
                    lblmessage.Text = "Mail category not update";
                }
                ViewState["flag"] = 0;
            }
            txtcategory.Text = "";
            bindgrid();

        }
        catch (Exception ex) { }
    }
    private void bindgrid()
    {
        try {

            DataSet ds = objcategory.Getcategory("exec usp_category 3,'',0,0", "tbl_category");
            if (ds.Tables["tbl_category"].Rows.Count > 0)
            {

                grdmailcategory.DataSource = ds.Tables["tbl_category"];
                grdmailcategory.DataBind();
                counter.InnerText = ds.Tables["tbl_category"].Rows.Count + " Mail category found";

            }
            else {
                counter.InnerText = "No Record found";
                grdmailcategory.DataSource = string.Empty;
                grdmailcategory.DataBind();

            }


        
        }
        catch (Exception ex) { }
    
    }
    protected void grdmailcategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblcategory = (Label)grdmailcategory.Rows[e.RowIndex].FindControl("lblcategory");
            HiddenField hdid = (HiddenField)grdmailcategory.Rows[e.RowIndex].FindControl("hdid");
            HiddenField hdsatus = (HiddenField)grdmailcategory.Rows[e.RowIndex].FindControl("hdstatus");

            ViewState["id"] = hdid.Value;
            txtcategory.Text = lblcategory.Text.Trim();
            btnadd.Text = "Update";
            ViewState["flag"] = 1;
            rdactive.Items.FindByValue(hdsatus.Value).Selected = true;

        
        }
        catch (Exception ex) { }

    }
}