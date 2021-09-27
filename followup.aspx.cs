using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DB;
using System.IO;

public partial class followup : System.Web.UI.Page
{
    cls_validation objvalidation = new cls_validation();
    add_followup obgff = new add_followup();
    DBHelper db = new DBHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                ViewState["personeid"] = "0";
                ViewState["id"] = "0";
                ViewState["flag"] = "0";
                if (Page.Request.QueryString["id"] != null)
                {
                    ViewState["personeid"] = Page.Request.QueryString["id"].ToString();

                    getproperty();
                    gridbind();
                }
                else
                {
                    Response.Redirect("mailsetup.aspx");
                }

                txtdate.Text = System.DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");

            }


        }
        catch (Exception ex) { }

    }
    private void getproperty()
    {
        try
        {
            string query = "  select person_name,email,mobile from dbo.tbl_m_person where id= " + ViewState["personeid"].ToString() + "";
            DataSet ds = new DataSet();
            ds = db.GetDataSet(query, "person");
            if (ds.Tables["person"].Rows.Count > 0)
            {
                txtpersonname.Text = ds.Tables["person"].Rows[0]["person_name"].ToString();
                txtemail.Text = ds.Tables["person"].Rows[0]["email"].ToString();
                txtmobile.Text = ds.Tables["person"].Rows[0]["mobile"].ToString();
            }



        }
        catch (Exception ex) { }
    }



    protected void btn_sendmail_Click(object sender, EventArgs e)
    {
        try
        {


            Session.Add("selectedmails", txtemail.Text.Trim()+",");
            Response.Redirect("sendmail.aspx?type=0");




        }
        catch (Exception ex) { }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtemail.Text == string.Empty)
            {
                lblmsg.Text = "Email can't be blank";
                return;
            }

            if (txtdate.Text == string.Empty)
            {
                lblmsg.Text = "Follow Up date can't be blank";
                return;
            }
            if (txtremark.Text == string.Empty)
            {
                lblmsg.Text = "Remark can't be blank";
                return;
            }




            if(ViewState["flag"].ToString()=="0")
            {
             if (ViewState["personeid"].ToString() != "0")
            {
                if (Page.Request.QueryString["id"] != null)
                {
                    if (obgff.addfollowup(1, Convert.ToInt32(Page.Request.QueryString["id"]), txtpersonname.Text.Trim(), txtemail.Text.Trim(), txtremark.Text.Trim(), ddlContact.SelectedItem.Value.ToString(), txtdate.Text.Trim(), Convert.ToInt32(ViewState["id"]), txtmobile.Text.Trim()))
                    {
                        lblmsg.Text = "Record Save Successfully";



                    }
                    else
                    {

                        lblmsg.Text = "Record Not Saved";



                    }
                }
               
            }
             clear();
        }
            if (ViewState["flag"].ToString() == "1")
            {

                if (obgff.addfollowup(2, Convert.ToInt32(Page.Request.QueryString["id"]), txtpersonname.Text.Trim(), txtemail.Text.Trim(), txtremark.Text.Trim(), ddlContact.SelectedItem.Value.ToString(), txtdate.Text.Trim(), Convert.ToInt32(ViewState["id"]), txtmobile.Text.Trim()))
                {

                    lblmsg.Text = "Update Successfully";
                    ViewState["flag"] = "0";
                    txtremark.Text = "";
                    btn_add.Text = "Add";
                }
             }
       
            gridbind();
        }






        catch (Exception ex) { }

    }
    
    
    

    private void clear()
    {
        try
        {
    
            txtremark.Text = string.Empty;
   
            txtdate.Text = string.Empty;


        }
        catch (Exception ex) { }
    }
    private void gridbind()
    {
        try
        {
            string query = @"select id, personname,email,remark,
(contactby) as contactid,
case when contactby=0 
then 'Mobile' else 
case when contactby=1
 then 'Email' else 
 case when contactby=2 
 then 'Visit office' else 
 case when contactby=3 then 'Other' else 'N/A' end end end end as contactby
,nextfollowupdate from TBL_Followup 
where PersonID=" + Convert.ToInt32(Page.Request.QueryString["id"]) + "";
            DataSet ds = new DataSet();
            ds = db.GetDataSet(query, "person");
            if (ds.Tables["person"].Rows.Count > 0)
            {
                gdvfollowup.DataSource = ds.Tables["person"];
                lblmsg.Text = ds.Tables["person"].Rows.Count + " Record Found";
                gdvfollowup.DataBind();


            }
            else {
                gdvfollowup.DataSource = string.Empty;
                lblmsg.Text = "Record Not Found";
                gdvfollowup.DataBind();
            
            
            }

        }
        catch (Exception ex) { }
    
    }
    protected void gdvfollowup_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lbltext = (Label)gdvfollowup.Rows[e.RowIndex].FindControl("lblpersoname");
            HiddenField hdid = (HiddenField)gdvfollowup.Rows[e.RowIndex].FindControl("hdid");
            HiddenField hdcontactid = (HiddenField)gdvfollowup.Rows[e.RowIndex].FindControl("hdcontactid");
            
            Label lbltext1 = (Label)gdvfollowup.Rows[e.RowIndex].FindControl("lblremark");
            Label lbltext2 = (Label)gdvfollowup.Rows[e.RowIndex].FindControl("lblcontactby");
            Label lbltext3 = (Label)gdvfollowup.Rows[e.RowIndex].FindControl("lblfollowupdate");

            ViewState["flag"] = "1";

            ViewState["id"] = hdid.Value;
            txtpersonname.Text = lbltext.Text;
      
            txtremark.Text = lbltext1.Text.Trim();
            btn_add.Text = "Update";
            txtdate.Text = lbltext3.Text;
        }
        catch (Exception ex) { }
    }
    protected void lnkexport_Click(object sender, EventArgs e)
    {
        try
        {

            string query = @"select  personname,email,remark,

case when contactby=0 
then 'Mobile' else 
case when contactby=1
 then 'Email' else 
 case when contactby=2 
 then 'Visit office' else 
 case when contactby=3 then 'Other' else 'N/A' end end end end as contactby
,nextfollowupdate from TBL_Followup 
where PersonID=" + Convert.ToInt32(Page.Request.QueryString["id"]) + "";

            DataSet dsd = db.GetDataSet(query, "export");
            DataTable dt = dsd.Tables["export"];
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;



            Response.AddHeader("content-disposition",
             "attachment;filename=Emaildata.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";




            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
       
       
        

        }
        catch (Exception ex) { }
    }
}
