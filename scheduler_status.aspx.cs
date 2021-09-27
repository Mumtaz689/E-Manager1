using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DB;
using System.Data.SqlClient;
public partial class scheduler_status : System.Web.UI.Page
{

    DBHelper db = new DBHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
if (
    !IsPostBack
    )
{
    Bindscheduler();
}



    }

    private void Bindscheduler()
    
    {
    
    try{
        DataSet ds = db.ExecuteDataSet("usp_scheduler", new SqlParameter[] { new SqlParameter("@spmode", 2) }, "tblschuder");
        if (ds.Tables["tblschuder"].Rows.Count > 0)
        {

            grdscheduler.DataSource = ds.Tables["tblschuder"];
            grdscheduler.DataBind();
            lblcounter.Text = ds.Tables["tblschuder"].Rows.Count + " Records Found";


        }
        else
        {
            grdscheduler.DataSource = string.Empty;
            grdscheduler.DataBind();
            lblcounter.Text = "Records not found";
        
        }

    

    
    
    }
        catch(Exception ex){
        
        
        }
    
    
    
    
    
    }



    protected void grdscheduler_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hid = (HiddenField)grdscheduler.Rows[e.RowIndex].FindControl("hid");




            db.NonQuery("delete from tbl_mail_send_scheduler where id=" + hid.Value);

            Bindscheduler();
        
        
        
        }
        catch (Exception ex) { }

    }
    protected void grdscheduler_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try {
            HiddenField hid = (HiddenField)grdscheduler.Rows[e.RowIndex].FindControl("hid");
            HiddenField hdstatus = (HiddenField)grdscheduler.Rows[e.RowIndex].FindControl("hdstatus");
            if (hdstatus.Value.ToString()=="0")
            {
                db.NonQuery("update tbl_mail_send_scheduler set status=1 where id=" + hid.Value);
            }
            if (hdstatus.Value.ToString() == "1")
            {
                db.NonQuery("update tbl_mail_send_scheduler set status=0 where id=" + hid.Value);
            }



         
            Bindscheduler();
        
        }
        catch (Exception ex) { }

    }
    protected void grdscheduler_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdstatus = (HiddenField)e.Row.FindControl("hdstatus");
                LinkButton lnkdeactive = (LinkButton)e.Row.FindControl("lnkdeactive");
                if (hdstatus.Value.ToString() == "0")
                {
                    lnkdeactive.Text = "Deactive";

                }
                else
                {
                    lnkdeactive.Text = "Active";
                
                }
         

            }
        
        
        
        
        
        
        
        }
        catch (Exception ex) { }
    }
}