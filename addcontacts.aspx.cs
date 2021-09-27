using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addcontacts : System.Web.UI.Page
{
    cls_category objcategory = new cls_category();
    cls_validation objvalidation = new cls_validation();
    protected void Page_Load(object sender, EventArgs e)
    {
       if(!IsPostBack)   
       {

           bindcategory();
          // btnsearch.Visible = false;
       }
    }
    private void bindcategory()
    {
        try { 
         DataSet ds = objcategory.Getcategory("exec usp_category 3,'',1,0", "tbl_category");
         if (ds.Tables["tbl_category"].Rows.Count > 0)
         {
             ddlmailcategory.DataSource = ds.Tables["tbl_category"];
             ddlmailcategory.DataTextField = "mailcategory";
             ddlmailcategory.DataValueField = "ID";
             ddlmailcategory.DataBind();

             ddlmailcategory.Items.Insert(0, new ListItem("Select","0"));
         
         }
        
        }
        catch (Exception ex) { }
    
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        { 
            if(objvalidation.Email(txtemail.Text.Trim())==false)
            {
                return;
            }

        
        
        }
        catch (Exception ex) { }
    }
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}