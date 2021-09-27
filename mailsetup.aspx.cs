using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DB;
using System.Configuration;
using System.IO;
public partial class mailsetup : System.Web.UI.Page
{
cls_category objcategory = new cls_category();
cls_validation objvalidation = new cls_validation();
clsperson objper = new clsperson();
clsmailstore objtag = new clsmailstore();
DBHelper db = new DBHelper();
protected void Page_Load(object sender, EventArgs e)
{
if (!IsPostBack)
{
    bindcategory();       
// btnsearch.Visible = false;
this.GetCustomersPageWise(1);
ViewState["mode"] = 0;
ViewState["personid"] = 0;
if (Page.Request.QueryString["mid"] != null)
{
Getdetail(Convert.ToInt32(Page.Request.QueryString["mid"]));
return;
}
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

ddlmailcategory.Items.Insert(0, new ListItem("ALL", "0"));

}

}
catch (Exception ex) { }

}
protected void btnadd_Click(object sender, EventArgs e)
{
try { 
        
        
if(txtemail.Text.Trim()=="")
{
lblmsg.Text = "Email address should't be blank";
return;
}


if (objvalidation.Email(txtemail.Text.Trim()) == false) {
lblmsg.Text = "Email address Invalid";
return;
}


if (ViewState["mode"].ToString()=="0")
{


if (Page.Request.QueryString["mid"] != null)
{

if (objper.addperson(8, 0, Convert.ToInt32(ddlmailcategory.SelectedValue), txtpersonname.Text.Trim(), txtemail.Text.Trim(), txtphone.Text.Trim(), txtmobile.Text.Trim(), txtzip.Text.Trim(), txtorganization.Text.Trim(), txtcity.Text.Trim(), txtcountry.Text.Trim(), txtaddress1.Text.Trim(), txtaddress2.Text.Trim(), txtremark.Text.Trim(), 0))
{
lblmsg.Text = "Record added successfully";

}
}

else
{
if (objper.addperson(1, 0, Convert.ToInt32(ddlmailcategory.SelectedValue), txtpersonname.Text.Trim(), txtemail.Text.Trim(), txtphone.Text.Trim(), txtmobile.Text.Trim(), txtzip.Text.Trim(), txtorganization.Text.Trim(), txtcity.Text.Trim(), txtcountry.Text.Trim(), txtaddress1.Text.Trim(), txtaddress2.Text.Trim(), txtremark.Text.Trim(), 0))
{
lblmsg.Text = "Record added successfully";

}
                
}
        
        
        
}
if (ViewState["mode"].ToString() == "1") 
{

    if (objper.addperson(6, Convert.ToInt32(ViewState["personid"]), Convert.ToInt32(ddlmailcategory.SelectedValue), txtpersonname.Text.Trim(), txtemail.Text.Trim(), txtphone.Text.Trim(), txtmobile.Text.Trim(), txtzip.Text.Trim(), txtorganization.Text.Trim(), txtcity.Text.Trim(), txtcountry.Text.Trim(), txtaddress1.Text.Trim(), txtaddress2.Text.Trim(), txtremark.Text.Trim(), 0))
{
lblmsg.Text = "Record updated successfully";
btnadd.Text = "Save";
ViewState["mode"] = "0";
txtemail.Text = "";

}
}
clear();
this.GetCustomersPageWise(1);

}
catch (Exception ex) { }

}
private void clear() {

try {

txtemail.Text = string.Empty;
txtphone.Text = string.Empty;
txtpersonname.Text = string.Empty;
txtemail.Text = string.Empty;
txtmobile.Text = string.Empty;
txtorganization.Text = string.Empty;
txtzip.Text = string.Empty;
txtcity.Text = string.Empty;
txtcountry.Text = string.Empty;
txtaddress1.Text = string.Empty;
txtaddress2.Text = string.Empty;
txtremark.Text = string.Empty;
ddlmailcategory.ClearSelection();
ddlmailcategory.SelectedValue = "0";


}
catch (Exception ex) { }
    
}
protected void bntsearch_Click(object sender, EventArgs e)
{
try {
this.GetCustomersPageWise(1);
}
catch (Exception ex) { }
}
protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
{
this.GetCustomersPageWise(1);
}
protected void lnkexport_Click(object sender, EventArgs e)
{
this.GetCustomersPageWise(1);
try
{
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
if (ddlfile.SelectedValue.ToString().ToLower() == "pdf")
{
Response.AddHeader("content-disposition", "attachment;filename=.DataTable.pdf");

Response.Charset = "";
Response.ContentType = "application/vnd.pdf";

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
private void GetCustomersPageWise(int pageIndex)
{
try
{

string constring = ConfigurationManager.AppSettings["ConnectionString"].ToString();
using (SqlConnection con = new SqlConnection(constring))
{
using (SqlCommand cmd = new SqlCommand("usp_person", con))
{
cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.AddWithValue("@spmode", 5);
cmd.Parameters.AddWithValue("@category_id", int.Parse(ddlmailcategory.SelectedValue));
cmd.Parameters.AddWithValue("@person_name",txtpersonname.Text.Trim());
cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim().TrimEnd().TrimStart().Replace(" ", ""));
cmd.Parameters.AddWithValue("@phone", txtphone.Text.Trim());
cmd.Parameters.AddWithValue("@mobile", txtmobile.Text.Trim());
cmd.Parameters.AddWithValue("@zip", txtzip.Text.Trim());
cmd.Parameters.AddWithValue("@company", txtorganization.Text.Trim());
cmd.Parameters.AddWithValue("@city", txtcity.Text.Trim());
cmd.Parameters.AddWithValue("@country", txtcountry.Text.Trim());
cmd.Parameters.AddWithValue("@address1", txtaddress1.Text.Trim());
cmd.Parameters.AddWithValue("@address2", txtaddress2.Text.Trim());
cmd.Parameters.AddWithValue("@remark", txtremark.Text.Trim());
cmd.Parameters.AddWithValue("@status", 0);
cmd.Parameters.AddWithValue("@id", 0);
cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlpage.SelectedValue));
cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
con.Open();
IDataReader idr = cmd.ExecuteReader();

grdperson.DataSource = idr;
grdperson.DataBind();
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
protected void Page_Changed(object sender, EventArgs e)
{
int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
this.GetCustomersPageWise(pageIndex);
}
private void Getdetail(int id)
{
try
{
DataSet ds = db.GetDataSet("exec [usp_person] 7," + Convert.ToInt32(ddlmailcategory.SelectedValue) + ",'','','','','','','','','','','',0," + id + ",10,1,0", "tbl_persondetail");
if (ds.Tables["tbl_persondetail"].Rows.Count > 0)
{
ddlmailcategory.ClearSelection();
ddlmailcategory.Items.FindByValue(Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["categortyid"])).Selected = true;
txtpersonname.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["person"]);
txtemail.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["email"]);
txtphone.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["phone"]);
txtmobile.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["mobile"]);
txtzip.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["zipcode"]);
txtorganization.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["company"]);
txtcity.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["city"]);
txtcountry.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["country"]);
txtaddress1.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["address1"]);
txtaddress2.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["address2"]);
txtremark.Text = Convert.ToString(ds.Tables["tbl_persondetail"].Rows[0]["remark"]);

btnadd.Text = "Update";
            
//    btnadd.OnClientClick = "return confirm('Are you sure\n all filup information is right');";

}

}
catch (Exception ex) { }
}
protected void grdperson_RowUpdating(object sender, GridViewUpdateEventArgs e)
{
try {

HiddenField hdcategorymail = (HiddenField)grdperson.Rows[e.RowIndex].FindControl("hdcategorymail");
HiddenField hdpersonid = (HiddenField)grdperson.Rows[e.RowIndex].FindControl("hdpersonid");
Label lblname = (Label)grdperson.Rows[e.RowIndex].FindControl("lblperson");
Label lblemail = (Label)grdperson.Rows[e.RowIndex].FindControl("lblemail");
Label lblphone = (Label)grdperson.Rows[e.RowIndex].FindControl("lblphone");
Label lblmobile = (Label)grdperson.Rows[e.RowIndex].FindControl("lblmobile");
Label lblzipcode = (Label)grdperson.Rows[e.RowIndex].FindControl("lblzipcode");
Label lblcomany = (Label)grdperson.Rows[e.RowIndex].FindControl("lblcomany");
Label lblcity = (Label)grdperson.Rows[e.RowIndex].FindControl("lblcity");

Label lblcountry = (Label)grdperson.Rows[e.RowIndex].FindControl("lblcountry");

Label lbladdress1 = (Label)grdperson.Rows[e.RowIndex].FindControl("lbladdress1");
Label lbladdress2 = (Label)grdperson.Rows[e.RowIndex].FindControl("lbladdress2");


Label lblremark = (Label)grdperson.Rows[e.RowIndex].FindControl("lblremark");

ViewState["mode"] = 1;
ViewState["personid"] = hdpersonid.Value.Trim();
txtpersonname.Text = lblname.Text.Trim();
txtemail.Text = lblemail.Text.Trim();
txtphone.Text = lblphone.Text.Trim();
txtmobile.Text = lblmobile.Text.Trim();
txtzip.Text = lblzipcode.Text.Trim();
txtorganization.Text = lblcomany.Text.Trim();
txtcity.Text = lblcity.Text.Trim();
txtcountry.Text = lblcountry.Text.Trim();
txtaddress1.Text = lbladdress1.Text.Trim();
txtaddress2.Text = lbladdress2.Text.Trim();
txtremark.Text = lblremark.Text.Trim();

ddlmailcategory.ClearSelection();
ddlmailcategory.Items.FindByValue(hdcategorymail.Value).Selected = true;

btnadd.Text = "Update";




}


catch (Exception ex) { }

}
protected void btntag_Click(object sender, EventArgs e)
{
try
{
foreach (GridViewRow grd in grdperson.Rows)
{
    RadioButton chkr = (RadioButton)(grd.FindControl("ckh"));
Label lblemail = (Label)(grd.FindControl("lblemail"));

if (chkr.Checked == true)
{

objtag.savemailstore(lblemail.Text.Trim(), 0, Convert.ToInt32(ddlmailcategory.SelectedValue));
}

}



}
catch (Exception ex) { }
}

protected void btnsend_Click(object sender, EventArgs e)
{
try
{
string selectedmails = "";
foreach (GridViewRow grd in grdperson.Rows)
{
CheckBox chkr = (CheckBox)(grd.FindControl("ckh"));
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
catch (Exception ex)
{ }
}
protected void btnscheduler_Click(object sender, EventArgs e)
{
try
{

string selectedmails = "";
foreach (GridViewRow grd in grdperson.Rows)
{
    RadioButton chkr = (RadioButton)(grd.FindControl("ckh"));
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

protected void ckhall_CheckedChanged1(object sender, EventArgs e)
{
    RadioButton chk = (RadioButton)grdperson.HeaderRow.FindControl("ckhall");

    if (chk.Checked == true)
    {

        foreach (GridViewRow grd in grdperson.Rows)
        {

            RadioButton chkr = (RadioButton)(grd.FindControl("ckh"));
            chkr.Checked = true;

        }

    }
    else
    {



        foreach (GridViewRow grd in grdperson.Rows)
        {

            RadioButton chkr = (RadioButton)(grd.FindControl("ckh"));
            chkr.Checked = false;

        }



    }

}
}