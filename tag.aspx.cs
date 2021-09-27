using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class tag : System.Web.UI.Page
{
    clsmailstore objtag = new clsmailstore();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            bindgrid();
        }
    }

      private void bindgrid() {
        try {
            DataSet ds = objtag.getdata(txtemail.Text.Trim());
            if (ds.Tables["tagemail"].Rows.Count > 0)
            {
                grdtag.DataSource = ds.Tables["tagemail"];
                grdtag.DataBind();
                lblcount.Text = ds.Tables["tagemail"].Rows.Count + " Tag Mails";

            }
            else {

                grdtag.DataSource = string.Empty;
                lblcount.Text = "Tag mails not found";
                grdtag.DataBind();

            }
        
        
        }
        catch (Exception ex) { }
    
    
    }
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)grdtag.HeaderRow.FindControl("chkall");

             if (chk.Checked == true)
             {

                 foreach (GridViewRow grd in grdtag.Rows)
                 {

                     CheckBox chkr = (CheckBox)(grd.FindControl("chk"));
                     chkr.Checked = true;


                 }

             }
             else
             {


                 foreach (GridViewRow grd in grdtag.Rows)
                 {

                     CheckBox chkr = (CheckBox)(grd.FindControl("chk"));
                     chkr.Checked = false ;


                 }

             }

        }
        catch (Exception ex) { }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }
    protected void btnsend_Click(object sender, EventArgs e)
    {
        try
        {

            string selectedmails = "";
            foreach (GridViewRow grd in grdtag.Rows)
            {
                CheckBox chkr = (CheckBox)(grd.FindControl("chk"));
                Label lblemail = (Label)(grd.FindControl("lblmail"));

                if (chkr.Checked == true)
                {


                    selectedmails = selectedmails + lblemail.Text.Trim() + ",";
                }

            }

            if (selectedmails != "")
            {
                try
                {

                    Session.Add("selectedmails", selectedmails);
                    Response.Redirect("sendmail.aspx?type=0");

                }
                catch (Exception ex)
                {


                }


            }
            else {
                lblcount.Text = "Select emails";
            
            }








        }
        catch (Exception ex)
        { }
    }
    protected void btnaddscheduler_Click(object sender, EventArgs e)
    {
        try
        {

            string selectedmails = "";
            foreach (GridViewRow grd in grdtag.Rows)
            {
                CheckBox chkr = (CheckBox)(grd.FindControl("chk"));
                Label lblemail = (Label)(grd.FindControl("lblmail"));

                if (chkr.Checked == true)
                {


                    selectedmails = selectedmails + lblemail.Text.Trim() + ",";
                }

            }

            if (selectedmails != "")
            {
                try
                {

                    Session.Add("selectedmails", selectedmails);
                    Response.Redirect("scheduler.aspx?type=0");

                }
                catch (Exception ex)
                {


                }


            }
            else
            {
                lblcount.Text = "Select emails";

            }








        }
        catch (Exception ex)
        { }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow grd in grdtag.Rows)
            {
                CheckBox chkr = (CheckBox)(grd.FindControl("chk"));
                Label lblemail = (Label)(grd.FindControl("lblmail"));

                if (chkr.Checked == true)
                {

                    objtag.removemailstore(lblemail.Text.Trim());
                }

            }
             bindgrid();


        }
        catch (Exception ex) { }
    }
}