using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
public partial class scheduler : System.Web.UI.Page
{
    cls_category objcategory = new cls_category();
    cls_account objac = new cls_account();
    cls_mail objmail = new cls_mail();
    cls_scheduler objsch = new cls_scheduler();
    clsmailstore objtag = new clsmailstore();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bindcategory();
            bindmailfrom();
            txtscheduleddate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");


            if (Session["selectedmails"] != null)
            {
                if (Page.Request.QueryString["type"] != null)
                {
                    if (Page.Request.QueryString["type"].ToString() == "1")
                    {
                        try
                        {
                            ddlmailcategory.Items.FindByValue(Convert.ToString(Session["selectedmails"])).Selected = true;
                            txtBCC.ReadOnly = true;
                        }
                        catch (Exception ex) { }

                    }

                    if (Page.Request.QueryString["type"].ToString() == "0")
                    {
                        try
                        {
                            txtBCC.Text = Session["selectedmails"].ToString();
                        }
                        catch (Exception ex) { }
                    }


                }


            }








            // btnsearch.Visible = false;
        }

    }
    private void bindcategory()
    {
        try
        {
            DataSet ds = objcategory.Getcategory("exec usp_category 3,'',1,0", "tbl_category");
            if (ds.Tables["tbl_category"].Rows.Count > 0)
            {
                ddlmailcategory.DataSource = ds.Tables["tbl_category"];
                ddlmailcategory.DataTextField = "mailcategory";
                ddlmailcategory.DataValueField = "ID";
                ddlmailcategory.DataBind();

                ddlmailcategory.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        catch (Exception ex) { }

    }
    private void bindmailfrom()
    {
        try
        {
            DataSet ds = objac.Getconfigemail("exec usp_mail_config 3,'','','','','',1,0", "tblmail");
            if (ds.Tables["tblmail"].Rows.Count > 0)
            {

                ddlfrommail.DataSource = ds.Tables["tblmail"];
                ddlfrommail.DataTextField = "email";
                ddlfrommail.DataValueField = "ID";
                ddlfrommail.DataBind();



            }
            else
            {


            }



        }
        catch (Exception ex) { }

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {

        try {
            if (txtto.Text == "")
            {

                lblmessage.Text = "To mail should not be blank";
                return;

            }

            string attachment = "";
            if (fffileupload.HasFile)
            {

                HttpFileCollection fileCollection = Request.Files;
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile uploadfile = fileCollection[i];
                    string fileName = Path.GetFileName(uploadfile.FileName);


                    if (uploadfile.ContentLength > 3307788)
                    {



                        lblmessage.Text = "File size should not be greater than MB";

                        return;

                    }
                }


                for (int i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile uploadfile = fileCollection[i];
                    string fileName = Path.GetFileName(uploadfile.FileName);


                    if (uploadfile.ContentLength > 0)
                    {

                        uploadfile.SaveAs(Server.MapPath("~/mail_attchment/schedule/") + fileName);
                   
                        attachment = attachment + fileName + ",";

                    }
                }

            }
            int bulk = 0;
            if(txtbulk.Text!=string.Empty)
             {

                 bulk = Convert.ToInt32(txtbulk.Text);
            }
            setmail(txtto.Text.Trim() + "," + txtcc.Text.Trim() + "," + txtBCC.Text.Trim());

            if (objsch.saveschedulerdata(1, Convert.ToInt32(ddlfrommail.SelectedValue), Convert.ToInt32(ddlmailcategory.SelectedValue), txtto.Text.Trim(), txtcc.Text.Trim(), txtBCC.Text.Trim(), txtsubjext.Text.Trim(), txtbody.Text.Trim(), attachment, 0, txtscheduleddate.Text.Trim(), ddlscheduler.SelectedValue.ToString(), bulk, 0, 0, 0, 0))
            {

                Response.Redirect("scheduler_status.aspx");
            }



            else {

                lblmessage.Text = "There are some problem to add scheduler please read all instractions for add scheduler";
                return;
            }

        
        
        
        
        
        
        }
        catch (Exception ex) { }

    }
    private void setmail(string email)
    {
        try
        {
            string[] emails = email.Split(',');

            for (int i = 0; i <= emails.Length - 1; i++)
            {
                savemail(emails[i].ToString());

            }



        }
        catch (Exception ex) { }
    }
    private void savemail(string mail)
    {
        try
        {
            objmail.savedata(3, 0, Convert.ToInt32(ddlmailcategory.SelectedValue), mail.ToString(), "", "", "", "", "", 0);
            objtag.removemailstore(mail.ToString());
        }
        catch (Exception ex) { }

    }

}