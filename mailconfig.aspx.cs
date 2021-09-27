using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class mailconfig : System.Web.UI.Page
{
    cls_account objac = new cls_account();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            getconfigmail();
            ViewState["id"] = 0;
            ViewState["flag"] = 0;

        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try {

            if(txtemail.Text==string.Empty || txtpassword.Text.Trim() ==string.Empty || txtport.Text.Trim()==string.Empty|| txtserver.Text.Trim()==string.Empty)
            {
            
            lblmessage.Text="Missing information please enter value in all fields";
            return;
            }


            if (ViewState["flag"].ToString() == "0")
            {

                if (objac.config_email(1, txtemail.Text.Trim(), txtpassword.Text.Trim(), txtserver.Text.Trim(), txtport.Text.Trim(), Convert.ToString(rdstatus.SelectedValue), Convert.ToInt32(ViewState["id"].ToString()),ddlenablessl.SelectedValue.ToString().Trim()) == true)
                {
                    lblmessage.Text = "Record saved successfully";
                }
                else
                {
                    lblmessage.Text = "Getting some problem Please contact to administrator";
                
                }
            }
            if (ViewState["flag"].ToString() == "1")
            {
                if (objac.config_email(2, txtemail.Text.Trim(), txtpassword.Text.Trim(), txtserver.Text.Trim(), txtport.Text.Trim(), Convert.ToString(rdstatus.SelectedValue),Convert.ToInt32(ViewState["id"].ToString()),ddlenablessl.SelectedValue.ToString().Trim()) == true)
                {
                    lblmessage.Text = "Record updated successfully";
                }
                else
                {
                    lblmessage.Text = "Getting some problem Please contact to administrator";
                }
                ViewState["flag"] = 0;
                btnadd.Text = "Add mail";
            }
            txtemail.Text = string.Empty;
            txtpassword.Text = string.Empty;
            txtport.Text = string.Empty;
            txtserver.Text = string.Empty;
            getconfigmail();
        
        }
        catch (Exception ex) { }
    }
    private void getconfigmail() {
        try {
            DataSet ds = objac.Getconfigemail("exec usp_mail_config 3,'','','','','','',0","tblmail");
            if (ds.Tables["tblmail"].Rows.Count > 0)
            {
                grdmail.DataSource = ds.Tables["tblmail"];
                grdmail.DataBind();
                lblcount.Text = ds.Tables["tblmail"].Rows.Count.ToString() + " Records found";
            }
            else
            {
                grdmail.DataSource = string.Empty;
                grdmail.DataBind();
                lblcount.Text = "Record not found";
            }
       


        }
        catch (Exception ex) { }
    
    }
    protected void grdmail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try {

            Label lblmail = (Label)grdmail.Rows[e.RowIndex].FindControl("lblemail");
            Label lblpassword = (Label)grdmail.Rows[e.RowIndex].FindControl("lblpassword");
            Label lblEnableSsl = (Label)grdmail.Rows[e.RowIndex].FindControl("lblEnableSsl");
            
            Label lblsmtp = (Label)grdmail.Rows[e.RowIndex].FindControl("lblsmtp");
            Label lblport = (Label)grdmail.Rows[e.RowIndex].FindControl("lblport");
            HiddenField hdid = (HiddenField)grdmail.Rows[e.RowIndex].FindControl("hdid");
            HiddenField hdstatus = (HiddenField)grdmail.Rows[e.RowIndex].FindControl("hdstatus");
            txtemail.Text = string.Empty;
            txtemail.Text = lblmail.Text.Trim();
            txtpassword.Text = string.Empty;
            txtpassword.Text = security.Decrypt(lblpassword.Text);
            txtserver.Text = lblsmtp.Text;
            txtport.Text = lblport.Text;
            ViewState["id"] = hdid.Value;
            ViewState["flag"] = 1;
            btnadd.Text = "Update";
            rdstatus.Items.FindByValue(hdstatus.Value).Selected = true;

            ddlenablessl.ClearSelection();
            ddlenablessl.Items.FindByValue(lblEnableSsl.Text.Trim()).Selected = true;

            
        
        }
        catch (Exception ex) { }
    }
}