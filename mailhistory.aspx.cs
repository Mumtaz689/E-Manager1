using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mailhistory : System.Web.UI.Page
{
    cls_category objcategory = new cls_category();
    cls_account objac = new cls_account();
    cls_mail objmail = new cls_mail();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

            bindcategory();
            bindmailfrom();
       //   Bindgrid();
          GetCustomersPageWise(1);
            
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

                ddlmailcategory.Items.Insert(0, new ListItem("All", "0"));

            }

        }
        catch (Exception ex) { }

    }

    private void bindmailfrom()
    {
        try
        {
            DataSet ds = objac.Getconfigemail("exec usp_mail_config 3,'','','','','',0", "tblmail");
            if (ds.Tables["tblmail"].Rows.Count > 0)
            {

                ddlfrommail.DataSource = ds.Tables["tblmail"];
                ddlfrommail.DataTextField = "email";
                ddlfrommail.DataValueField = "ID";
                ddlfrommail.DataBind();
                ddlfrommail.Items.Insert(0, new ListItem("All", "0"));


            }
            else
            {


            }



        }
        catch (Exception ex) { }

    }
    private void Bindgrid()
    {
        try
        {
            DataSet ds = objac.Getconfigemail("exec usp_mail 5", "mailhistory");
            if (ds.Tables["mailhistory"].Rows.Count > 0)
            {
                grdmailhistory.DataSource = ds.Tables["mailhistory"];
                grdmailhistory.DataBind();
                lblcounter.Text = ds.Tables["mailhistory"].Rows.Count + " Records found";

            }
            else
            {
                grdmailhistory.DataSource = string.Empty;
                grdmailhistory.DataBind();
                lblcounter.Text = "No records found";

            }



        }
        catch (Exception ex) { }
    
    }



    private void GetCustomersPageWise(int pageIndex)
    {
        string constring = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        using (SqlConnection con = new SqlConnection(constring))
        {
            using (SqlCommand cmd = new SqlCommand("usp_mailhistory", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@spmode", 1);
                cmd.Parameters.AddWithValue("@mailfrom", int.Parse(ddlfrommail.SelectedValue));
                cmd.Parameters.AddWithValue("@mailcategory", int.Parse(ddlmailcategory.SelectedValue));
                cmd.Parameters.AddWithValue("@name", txtname.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtphone.Text.Trim());
                cmd.Parameters.AddWithValue("@mailbody", txtmailbody.Text.Trim());
                cmd.Parameters.AddWithValue("@fromdate", txtdatefrom.Text.Trim());
                cmd.Parameters.AddWithValue("@todate", txtdateto.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtmail.Text.Trim());
                cmd.Parameters.AddWithValue("@mailstatus", int.Parse(ddlstatus.SelectedValue));
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@subject", txtsubject.Text.Trim());
                cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlpage.SelectedValue));
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                con.Open();
                IDataReader idr = cmd.ExecuteReader();
             
                grdmailhistory.DataSource = idr;
                grdmailhistory.DataBind();
                idr.Close();
                con.Close();
                int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                this.PopulatePager(recordCount, pageIndex);
                lblcounter.Text = recordCount + " Records found";
                
            }
        }
    }

    private void PopulatePager(int recordCount, int currentPage)
    {
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

    protected void grdmailhistory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {


            HiddenField hdid = (HiddenField)grdmailhistory.Rows[e.RowIndex].FindControl("hdid");

            Response.Redirect("sendmail.aspx?send="+hdid.Value.ToString());
        }
        catch (Exception ex) { }


    }
    protected void grdmailhistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {

            HiddenField hdid = (HiddenField)grdmailhistory.Rows[e.RowIndex].FindControl("hdid");
            objmail.delete(Convert.ToInt32(hdid.Value));
            this.GetCustomersPageWise(1);


        }
        catch (Exception ex) { }


    }
    protected void grdmailhistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DropDownList ddl = (DropDownList)e.Row.FindControl("ddlCity");
            //int CountryId = Convert.ToInt32(ddl.SelectedItem);
        }


    }
    protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetCustomersPageWise(1);
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try {

            this.GetCustomersPageWise(1);
        }
        catch (Exception ex) { }
    }
}