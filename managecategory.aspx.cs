using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DB;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
public partial class managecategory : System.Web.UI.Page
{
    cls_category objcategory = new cls_category();
    cls_validation objvalidation = new cls_validation();
    DBHelper db = new DBHelper();
    cls_mail objmail = new cls_mail();
    clsmailstore objtag = new clsmailstore();
    DataSet dss;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dss = new DataSet();
            ViewState["category"] = "";
            ViewState["pageindex"] = 1;
            ViewState["sendmail"] = "0";
            bindcategory();
            GetCustomersPageWise(1);
        
          
            // btnsearch.Visible = false;
        }
    }
    private void bindcategory()
    {
        try
        {
            dss = objcategory.Getcategory("exec usp_category 3,'',1,0", "tbl_category");
            if (dss.Tables["tbl_category"].Rows.Count > 0)
            {
                ddlmailcategory.DataSource = dss.Tables["tbl_category"];
                ddlmailcategory.DataTextField = "mailcategory";
                ddlmailcategory.DataValueField = "ID";
                ddlmailcategory.DataBind();

                ddlmailcategory.Items.Insert(0, new ListItem("ALL", "0"));
                ViewState["category"] = dss.Tables["tbl_category"];

            }

        }
        catch (Exception ex) { }

    }
    private void GetCustomersPageWise(int pageIndex)
    {
        try
        {

            string constring = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("usp_mailidentification", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spmode", 1);
                    cmd.Parameters.AddWithValue("@mailcategory", int.Parse(ddlmailcategory.SelectedValue));
                    cmd.Parameters.AddWithValue("@identification", int.Parse(ddlidentification.SelectedValue));
                    cmd.Parameters.AddWithValue("@mail", txtmails.Text.Trim().TrimEnd().TrimStart().Replace(" ",""));
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlpage.SelectedValue));
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    con.Open();
                    IDataReader idr = cmd.ExecuteReader();

                    grdmail.DataSource = idr;
                    grdmail.DataBind();
                    idr.Close();
                    con.Close();
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    this.PopulatePager(recordCount, pageIndex);
                    lblcounter.Text = recordCount + " Records found";

                }
            }
        }
        catch (Exception ex) { }
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        ViewState["pageindex"] = currentPage;
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlpage.SelectedValue));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", currentPage > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void PageSize_Changed(object sender, EventArgs e)
    {
        this.GetCustomersPageWise(1);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetCustomersPageWise(pageIndex);
    }
    protected void grdmail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try {

     

            HiddenField hdemailid = (HiddenField)grdmail.Rows[e.RowIndex].FindControl("hdemailid");


            DropDownList ddlcategorye = (DropDownList)grdmail.Rows[e.RowIndex].FindControl("ddlcategory");
                   



            objcategory.update_mail_category(2, Convert.ToInt32(ddlmailcategory.SelectedValue), Convert.ToInt32(hdemailid.Value));
            lblmessage.Text = "Mail Category moved successfully";
        }
        catch (Exception ex) { }

    }
    protected void grdmail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlcategory = (DropDownList)e.Row.FindControl("ddlcategory");
                HiddenField hdmailcategoryid = (HiddenField)e.Row.FindControl("hdmailcategoryid");

                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["category"];

                if(dt.Rows.Count>0)
                {

                    ddlcategory.DataSource = dt;
                    ddlcategory.DataTextField = "mailcategory";
                    ddlcategory.DataValueField = "ID";
                    ddlcategory.DataBind();

                    ddlcategory.Items.Insert(0, new ListItem("None of these", "0"));

                
                }

             
                ddlcategory.ClearSelection();
                ddlcategory.Items.FindByValue(hdmailcategoryid.Value).Selected = true;


                //int CountryId = Convert.ToInt32(ddl.SelectedItem);
            }

        
        
        
        
        
        
        
        
        
        
        }
        catch (Exception ex) { }
    }
    protected void grdmail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdemailid = (HiddenField)grdmail.Rows[e.RowIndex].FindControl("hdemailid");
            objcategory.update_mail_category(3, Convert.ToInt32(ddlmailcategory.SelectedValue), Convert.ToInt32(hdemailid.Value));
            lblmessage.Text = "Record deleted successfully";
            GetCustomersPageWise(Convert.ToInt32(ViewState["pageindex"]));
        }
        catch (Exception ex) {
            lblmessage.Text = "Record not delete";
        }

    }
    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
        try {

            int count = 0;

            CheckBox chk = (CheckBox)grdmail.HeaderRow.FindControl("chkall");

            if (chk.Checked==true)
            {

          foreach ( GridViewRow grd in grdmail.Rows)
          {

              CheckBox chkr = (CheckBox)(grd.FindControl("chkselect"));
              chkr.Checked = true;
              count++;
              ViewState["sendmail"] = "1";
        }
          lblselection.Text = count + " Records selected";
        }

            if (chk.Checked == false)
            {

                foreach (GridViewRow grd in grdmail.Rows)
                {

                    CheckBox chkr = (CheckBox)(grd.FindControl("chkselect"));
                    chkr.Checked = false;
                    ViewState["sendmail"] = "0";
                    lblselection.Text = "";

                }
            }
        }
        catch (Exception ex) { }

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try {

            GetCustomersPageWise(1);
        }
        catch (Exception ex) { }


    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            int isupdate = 0;

            if (ddlmailcategory.SelectedValue.ToString() != "0")
            {

                foreach (GridViewRow grd in grdmail.Rows)
                {

                    CheckBox chkr = (CheckBox)(grd.FindControl("chkselect"));
                    if (chkr.Checked == true)
                    {
                        flag = 1;

                        foreach (GridViewRow grdrow in grdmail.Rows)
                        {
                           if (ddlmailcategory.SelectedValue != "0")
                            {
                                HiddenField hdemailid = (HiddenField)(grd.FindControl("hdemailid"));

                                objcategory.update_mail_category(2, Convert.ToInt32(ddlmailcategory.SelectedValue), Convert.ToInt32(hdemailid.Value));
                                isupdate = 1;

                            }

                        }







                    }

                }

            }
            else
            {




                foreach (GridViewRow grd in grdmail.Rows)
                {

                    DropDownList ddlcategorye = (DropDownList)(grd.FindControl("ddlcategory"));
                   

                    
                    if (ddlcategorye.SelectedValue!="0")
                    {
                        HiddenField hdemailid = (HiddenField)(grd.FindControl("hdemailid"));

                        objcategory.update_mail_category(2, Convert.ToInt32(ddlcategorye.SelectedValue), Convert.ToInt32(hdemailid.Value));
                        isupdate = 1;

                    }

                }
            
            
            
            }









            if (isupdate == 1) {

                lblmessage.Text = "Selected Mails are updated successfully";
                GetCustomersPageWise(Convert.ToInt32( ViewState["pageindex"]));
                return;
            }


            if (flag == 1) {

                lblmessage.Text = "Selected Mails are updated successfully";
                GetCustomersPageWise(Convert.ToInt32(ViewState["pageindex"]));
                return;
            
            }

        }
        catch (Exception ex) { }
    }
    protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetCustomersPageWise(1);
        }
        catch (Exception ex) { }
    }
    protected void lnkexport_Click(object sender, EventArgs e)
    {
        try {

            //Get the data from database into datatable
            string strQuery = "exec usp_mailidentification 4,0,0,'',0,0,0,''";
            DataSet dsd = db.GetDataSet(strQuery, "export");
            DataTable dt = dsd.Tables["export"];
//Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;


            if (ddlfile.SelectedValue.ToString().ToLower() == "excel")
            {
            Response.AddHeader("content-disposition",
             "attachment;filename=Emaildata.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            }


            if (ddlfile.SelectedValue.ToString().ToLower() == "doc")
            {
                Response.AddHeader("content-disposition", "attachment;filename=Emaildata.doc");
        
   Response.Charset = "";
   Response.ContentType = "application/vnd.doc";

            }













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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtmails.Text.Trim() != "")
            {
                setmail(txtmails.Text.Trim());
                txtmails.Text = string.Empty;
                lblmessage.Text = "Mails added successfully";
                GetCustomersPageWise(1);
            }
            else
            {
                lblmessage.Text = "You have't enter any mail,please enter mails";
            
            }
        }
        catch (Exception ex) { }
    }
    protected void Btnsend_Click(object sender, EventArgs e)
    {
        try
        {

               string selectedmails = "";
                foreach (GridViewRow grd in grdmail.Rows)
                {
                    CheckBox chkr = (CheckBox)(grd.FindControl("chkselect"));
                    Label lblemail = (Label)(grd.FindControl("lblemail"));

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


                if (selectedmails == "") 
                {
                    try
                    {


                        if (ddlmailcategory.SelectedValue.ToString() != "0")
                        {

                            Session.Add("selectedmails", ddlmailcategory.SelectedValue);
                            Response.Redirect("sendmail.aspx?type=1");


                        }
                    }
                    catch (Exception ex) 
                    { 
                    
                    
                    }
                
                }
        

        
        
        
        
        }
        catch(Exception ex)
            {}
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

        }
        catch (Exception ex) { }

    }


    protected void btnscheduler_Click(object sender, EventArgs e)
    {
        try
        {

            string selectedmails = "";
            foreach (GridViewRow grd in grdmail.Rows)
            {
                CheckBox chkr = (CheckBox)(grd.FindControl("chkselect"));
                Label lblemail = (Label)(grd.FindControl("lblemail"));

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


            if (selectedmails == "")
            {
                try
                {


                    if (ddlmailcategory.SelectedValue.ToString() != "0")
                    {

                        Session.Add("selectedmails", ddlmailcategory.SelectedValue);
                        Response.Redirect("scheduler.aspx?type=1");


                    }
                }
                catch (Exception ex)
                {


                }

            }






        }
        catch (Exception ex)
        { }
    }
    protected void btntag_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow grd in grdmail.Rows)
            {
                CheckBox chkr = (CheckBox)(grd.FindControl("chkselect"));
                Label lblemail = (Label)(grd.FindControl("lblemail"));

                if (chkr.Checked == true)
                {

                    objtag.savemailstore(lblemail.Text.Trim(),0,Convert.ToInt32(ddlmailcategory.SelectedValue));
                }

            }

           
        
        }
        catch (Exception ex) { }

    }
}