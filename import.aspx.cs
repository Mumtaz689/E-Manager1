using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class import : System.Web.UI.Page
{
    cls_category objcategory = new cls_category();
    clsimport objimp = new clsimport();
    cls_validation objva = new cls_validation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bindcategory();
            bindmailcategory();
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
    private void bindmailcategory()
    {

        DataSet ds = objcategory.Getcategory("exec usp_category 3,'',1,0", "tbl_category");
        if (ds.Tables["tbl_category"].Rows.Count > 0)
        {

            grdcategory.DataSource = ds.Tables["tbl_category"];
            grdcategory.DataBind();



        }
        else
        { }
    }
    protected void lnkdownload_Click(object sender, EventArgs e)
    {
        try {


            Response.ContentType = "xlsx";
            Response.AddHeader("Content-Disposition", "attachment;filename=import.xlsx");
            Response.TransmitFile(Server.MapPath("template/import.xlsx"));
            Response.End();
        }
        catch (Exception ex) { }
    }
    private void Import()
    {

        try
        {

            if (ffimport.HasFile == false) {

                lblmesg.Text = "Please select file for import data";

                return;
            }

            
            
            





            //Coneection String by default empty  
            string ConStr = "";
            //Extantion of the file upload control saving into ext because   
            //there are two types of extation .xls and .xlsx of Excel   
            string ext = Path.GetExtension(ffimport.FileName).ToLower();
           


            //getting the path of the file   
            string path = Server.MapPath("import/" + ffimport.FileName);
            //saving the file inside the MyFolder of the server  


          
                ffimport.SaveAs(path);
          
           // Label1.Text = ffimport.FileName + "\'s Data showing into the GridView";
            //checking that extantion is .xls or .xlsx  
            if (ext.Trim() == ".xls")
            {
                //connection string for that file which extantion is .xls  
                ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (ext.Trim() == ".xlsx")
            {
                //connection string for that file which extantion is .xlsx  
                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            //making query  
            string query = "SELECT * FROM [Sheet1$]";
            //Providing connection  
            OleDbConnection conn = new OleDbConnection(ConStr);
            //checking that connection state is closed or not if closed the   
            //open the connection  
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //create command object  
            OleDbCommand cmd = new OleDbCommand(query, conn);
            // create a data adapter and get the data into dataadapter  
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            //fill the Excel data to data set  
            da.Fill(ds);
            //set data source of the grid view  
         //   gvExcelFile.DataSource = ds.Tables[0];
            //binding the gridview  
           // gvExcelFile.DataBind();
            //close the connection  
            conn.Close();

            importdata(ds);

        }
        catch (Exception ex) {

            lblmesg.Text = ex.Message;
        }
    
    
    
    
    
    
    
    }


    private void importdata( DataSet ds)
    {

        try { 
        
        if(ds.Tables[0].Rows.Count>0)
        {
            btnupload.Visible = true;
            grdimport.DataSource = ds.Tables[0];
            grdimport.DataBind();

            lblcounter.Text = ds.Tables[0].Rows.Count + " Records for uploading process";

            //foreach (DataRow dr in ds.Tables[0].Rows  )
            //{

            // int code =0;
            //if(ddlmailcategory.SelectedValue.ToString()!="0")
            //{
            //    code=Convert.ToInt32(dr["code"]);
            // }
            //   objimp.import(1, Convert.ToInt32(dr["code"]), Convert.ToString(dr["email"]), Convert.ToString(dr["name"]), Convert.ToString(dr["phone"]), Convert.ToString(dr["mobile"]), Convert.ToString(dr["phone"]), Convert.ToString(dr["hotelname"]), Convert.ToString(dr["city"]), Convert.ToString(dr["country"]), Convert.ToString(dr["address1"]), Convert.ToString(dr["address2"]));
            //}


         
        
        }
        
        
        
        
        
        }
        catch (Exception ex) {


            lblmesg.Text = ex.Message;
        }
    }

    protected void btnimport_Click(object sender, EventArgs e)
    {
        try {


            Import();
        
        }
        catch (Exception ex) { }
    }
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        try {
            CheckBox chk = (CheckBox)grdimport.HeaderRow.FindControl("chkall");
            if (chk.Checked == true)
            {

                foreach (GridViewRow grd in grdimport.Rows)
                {

                    CheckBox chkr = (CheckBox)(grd.FindControl("chk"));
                    chkr.Checked = true;

                }

            }



            else
            {

                foreach (GridViewRow grd in grdimport.Rows)
                {

                    CheckBox chkr = (CheckBox)(grd.FindControl("chk"));
                    chkr.Checked = false;

                }
            
            
            
            
            }
        
        
        
        }
        catch (Exception ex) { }



    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        try {


            int invalidmail = 0;
            int validmail = 0;
            int count=0;

            foreach (GridViewRow grd in grdimport.Rows)
            {
                int code = 0;
                CheckBox chkr = (CheckBox)(grd.FindControl("chk"));
                Label lblcode = (Label)(grd.FindControl("lblcode"));
                Label lblemail = (Label)(grd.FindControl("lblemail"));
                Label lblname = (Label)(grd.FindControl("lblname"));
                Label lblmobile = (Label)(grd.FindControl("lblmobile"));
                Label lblphone = (Label)(grd.FindControl("lblphone"));
                Label lblzip = (Label)(grd.FindControl("lblzip"));
                Label lblhotelname = (Label)(grd.FindControl("lblhotelname"));

                Label lblcity = (Label)(grd.FindControl("lblcity"));

                Label lblcountry = (Label)(grd.FindControl("lblcountry"));
                Label lbladdress1 = (Label)(grd.FindControl("lbladdress1"));
                Label lbladdress2 = (Label)(grd.FindControl("lbladdress2"));

                if(ddlmailcategory.SelectedValue.ToString()!="0")
                {

                    code = Convert.ToInt32(ddlmailcategory.SelectedValue);

                }
                
                



                if (chkr.Checked == true)
                {
                    count++;

                    if (objva.Email(lblemail.Text.Trim()) == true)
                    {
                        validmail++;
                        objimp.import(1, code, lblemail.Text.Trim(), lblname.Text.Trim(), lblphone.Text.Trim(), lblmobile.Text.Trim(), lblphone.Text.Trim(), lblhotelname.Text.Trim(), lblcity.Text.Trim(), lblcountry.Text.Trim(), lbladdress1.Text.Trim(), lbladdress2.Text.Trim());
                    }
                    else
                    {
                        invalidmail++;
                    
                    }

                }

            }

            lblmesg.Text = "Total Upload=" + count +" Invalid Mails=" + invalidmail +" Valid Mails="+ validmail;
        
        
        
        }
        catch(Exception ex){}
    }
}