using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class signature : System.Web.UI.Page
{


    clsmailstore objmail = new clsmailstore();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            bindsignature();
            ViewState["flag"] = "0";
            ViewState["id"] = "0";
        }
    }
    private void bindsignature()
    {
        try {

            DataSet ds = objmail.getcategory(3, 0);
            if (ds.Tables["tbl_signature"].Rows.Count > 0)
            {

                grdsignature.DataSource = ds.Tables["tbl_signature"];
                grdsignature.DataBind();
            }
            else
            {

                grdsignature.DataSource = string.Empty;
                grdsignature.DataBind();
            }
            
        
        
        }
        catch (Exception ex) { }
    
    }
    protected void btnsend_Click(object sender, EventArgs e)
    {

        try {
        
        if(txtcategory.Text.Trim()==string.Empty)
        {

            lblmsg.Text = "Signature category can't be blank";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        if (txtbody.Text.Trim() == string.Empty)
        {
            lblmsg.Text = "Signature can't be blank";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            return;
        
        }
        if (ViewState["flag"].ToString() == "0")
        {
            if (objmail.addcategory(txtcategory.Text.Trim(), txtbody.Text.Trim(), 1) == true)
            {


                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Signature Added successfully";
                txtbody.Text = string.Empty;
                txtcategory.Text = string.Empty;
                bindsignature();
            }
        }
        if (ViewState["flag"].ToString() == "1")
        {

            if (objmail.addcategory(txtcategory.Text.Trim(), txtbody.Text.Trim(), 5,Convert.ToInt32( ViewState["id"])) == true)
            {
                txtbody.Text = string.Empty;
                txtcategory.Text = string.Empty;
                bindsignature();
                ViewState["flag"] = "0";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Signature Updated successfully";
            }
        
        }

        }
        catch (Exception ex) { }


    }
    protected void grdsignature_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try {

            HiddenField hd = (HiddenField)grdsignature.Rows[e.RowIndex].FindControl("hdcategotyid");
            Label lblmailsignature = (Label)grdsignature.Rows[e.RowIndex].FindControl("lblcategoryname");
            Label lblbodyy = (Label)grdsignature.Rows[e.RowIndex].FindControl("lblbody");
            
            txtcategory.Text = lblmailsignature.Text.Trim();
            txtbody.Text = lblbodyy.Text.Trim();
  

            ViewState["flag"] = "1";
            ViewState["id"] = hd.Value;
        
        }
        catch (Exception ex) { }
    }
    protected void grdsignature_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try {
            HiddenField hd = (HiddenField)grdsignature.Rows[e.RowIndex].FindControl("hdcategotyid");

            if (objmail.removecategory(4, Convert.ToInt32(hd.Value)) == true)
            {
                lblmsg.Text = "Signature Deleted successfully";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                bindsignature();
            }

            else
            {
                lblmsg.Text = "Signature not deleted";
                lblmsg.ForeColor = System.Drawing.Color.Red;

            }
        
        
        
        }
        catch (Exception ex) { }
    }
}