using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using DB;
public partial class sendmail : System.Web.UI.Page
{

    cls_category objcategory = new cls_category();
    cls_account objac = new cls_account();
    cls_mail objmail = new cls_mail();
    clsmailstore objtag = new clsmailstore();
    DBHelper db = new DBHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindcategory();
            bindmailfrom();
            addsignature();

            if (Page.Request.QueryString["send"]!=null)
            {

                viewresend(Convert.ToInt32(Page.Request.QueryString["send"]));
                btnsend.Text = "Resend";

            }


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
    protected void btnsend_Click(object sender, EventArgs e)
    {


        try
        {
            string attachment="";


            if (txtto.Text == "") {

                lblmessage.Text = "To mail should not be blank";
                return;
            
            }
             DataSet ds = objmail.Getmailconfiguration(Convert.ToInt32(ddlfrommail.SelectedValue));
            if (ds.Tables["tbl_email"].Rows.Count > 0) 
            {

                ArrayList ar = new ArrayList();
                if (fffileupload.HasFile)
                {
                    
                    HttpFileCollection fileCollection = Request.Files;
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        HttpPostedFile uploadfile = fileCollection[i];
                        string fileName = Path.GetFileName(uploadfile.FileName);


                        if (uploadfile.ContentLength > 3307788)
                        {



                            lblmessage.Text = "File size should not be greater than 3MB";

                            return;

                        }
                    }


                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        HttpPostedFile uploadfile = fileCollection[i];
                        string fileName = Path.GetFileName(uploadfile.FileName);
                    

                        if (uploadfile.ContentLength > 0)
                        {

                            uploadfile.SaveAs(Server.MapPath("~/mail_attchment/") + fileName);
                            ar.Add(Server.MapPath("~/mail_attchment/") + fileName);
                            attachment = attachment + fileName + ",";

                        }
                    }

                }

                if (objmail.sendmail(txtbody.Text.Replace("textarea", "div").ToString().Trim(), txtto.Text.Trim(), txtsubjext.Text.Trim(), ds.Tables["tbl_email"].Rows[0]["smtp"].ToString(), txtcc.Text.Trim(), txtBCC.Text.Trim(), ddlfrommail.SelectedItem.Text.ToString().Trim(), ds.Tables["tbl_email"].Rows[0]["pwd"].ToString(), Convert.ToInt32(ds.Tables["tbl_email"].Rows[0]["port"].ToString()), Convert.ToBoolean(ds.Tables["tbl_email"].Rows[0]["EnableSsl"].ToString()), ar) == true)
                {
                    setmail(txtto.Text.Trim() + "," + txtcc.Text.Trim() + "," + txtBCC.Text.Trim());

                    objmail.savedata(1, Convert.ToInt32(ddlfrommail.SelectedValue), Convert.ToInt32(ddlmailcategory.SelectedValue), txtto.Text.Trim(), txtcc.Text.Trim(), txtBCC.Text.Trim(), txtsubjext.Text.Trim(), txtbody.Text.Trim(), attachment,0);
     
                    lblmessage.Text = "Mail has been sent successfully";
                    lblmessage.ForeColor = System.Drawing.Color.Green;
                    txtBCC.Text = "";
                    txtcc.Text = "";
                    txtto.Text = "";
                    txtsubjext.Text = "";
                }
                else
                {

                    lblmessage.Text = "Mail sending failure";
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        catch (Exception ex) { 
        
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

                ddlmailcategory.Items.Insert(0, new ListItem("Any", "0"));

            }

        }
        catch (Exception ex) { }

    }
 private void bindmailfrom()
    {
        try
        {
            DataSet ds = objac.Getconfigemail("exec usp_mail_config 3,'','','','','',0,0", "tblmail");
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
private void setmail(string email)
 {
     try
     {
         string[] emails = email.Split(',');

         for (int i = 0; i <= emails.Length - 1;i++ )
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
         objmail.savedata(3,0,Convert.ToInt32(ddlmailcategory.SelectedValue),mail.ToString(),"","","","","",0);
         objtag.removemailstore(mail.ToString());
     }
     catch (Exception ex) { }
 
 }
 private void viewresend(int id)
 {
     try { 
     
           DataSet ds = db.ExecuteDataSet("usp_mailhistory", new SqlParameter[] { new SqlParameter("@spmode", 2), new SqlParameter("@mailfrom", id) }, "sendmail");
           if (ds.Tables["sendmail"].Rows.Count > 0) 
           {
               txtto.Text = ds.Tables["sendmail"].Rows[0]["to"].ToString();
               txtcc.Text = ds.Tables["sendmail"].Rows[0]["cc"].ToString();
               txtBCC.Text = ds.Tables["sendmail"].Rows[0]["bcc"].ToString();
               txtsubjext.Text = ds.Tables["sendmail"].Rows[0]["subject"].ToString();
               txtbody.Text = ds.Tables["sendmail"].Rows[0]["body"].ToString();


               ddlmailcategory.ClearSelection();
               ddlmailcategory.Items.FindByValue(ds.Tables["sendmail"].Rows[0]["mailcategory_id"].ToString()).Selected = true;
               ddlfrommail.Items.FindByValue(ds.Tables["sendmail"].Rows[0]["frommailid"].ToString()).Selected = true;
               


           
           }


     
     }
     catch (Exception ex) { }
 
 }
 private void addsignature()
 {
     try
     {
         DataSet ds = objtag.getcategory(3, 1);
         if (ds.Tables["tbl_signature"].Rows.Count > 0) {

             ddlsignature.DataSource = ds.Tables["tbl_signature"];
             ddlsignature.DataTextField = "categoryname";
             ddlsignature.DataValueField = "id";
             ddlsignature.DataBind();
             ddlsignature.Items.Insert(0, new ListItem("Select", "0"));
         }
     
     }
     catch (Exception ex) { }
 
 }


 protected void ddlsignature_SelectedIndexChanged(object sender, EventArgs e)
 {
     try { 
     
 DataSet ds = objtag.getcategory(2, Convert.ToInt32(ddlsignature.SelectedValue));
 if (ds.Tables["tbl_signature"].Rows.Count > 0)
 {
     txtbody.Text = "<div><br/><br/></div>" + ds.Tables["tbl_signature"].Rows[0]["signature"].ToString();
 
 }
     
     }
     catch (Exception ex) { }
 }
}