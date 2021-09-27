using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DB;
public partial class ajax : System.Web.UI.Page
{
    clsperson objperson = new clsperson();
    DBHelper db = new DBHelper();
    clsmailstore objtag = new clsmailstore();
    protected void Page_Load(object sender, EventArgs e)
    
     {
        if (Page.Request.QueryString["addperson"] != null)
        {

           bool write= addperson(Convert.ToInt32(Page.Request.QueryString["spmode"]) , Convert.ToInt32(Page.Request.QueryString["id"]), Convert.ToInt32(Page.Request.QueryString["categoryid"]), Convert.ToString(Page.Request.QueryString["personname"]), Convert.ToString(Page.Request.QueryString["email"]), Convert.ToString(Page.Request.QueryString["phone"]),Convert.ToString(Page.Request.QueryString["mobile"]), Convert.ToString(Page.Request.QueryString["zipcode"]), Convert.ToString(Page.Request.QueryString["companyname"]), Convert.ToString(Page.Request.QueryString["city"]), Convert.ToString(Page.Request.QueryString["country"]), Convert.ToString(Page.Request.QueryString["address1"]), Convert.ToString(Page.Request.QueryString["address2"]), Convert.ToString(Page.Request.QueryString["remark"]), Convert.ToInt32(Page.Request.QueryString["status"]));
           if (write == true)
           {
               Write("0");
           }
           else

           {
               Write("1");
           }
        }
        if (Page.Request.QueryString["searchperson"] != null)
        {

            searchperson(Convert.ToInt32(Page.Request.QueryString["spmode"]), Convert.ToInt32(Page.Request.QueryString["id"]), Convert.ToInt32(Page.Request.QueryString["categoryid"]), Convert.ToString(Page.Request.QueryString["personname"]), Convert.ToString(Page.Request.QueryString["email"]), Convert.ToString(Page.Request.QueryString["phone"]), Convert.ToString(Page.Request.QueryString["mobile"]), Convert.ToString(Page.Request.QueryString["zipcode"]), Convert.ToString(Page.Request.QueryString["companyname"]), Convert.ToString(Page.Request.QueryString["city"]), Convert.ToString(Page.Request.QueryString["country"]), Convert.ToString(Page.Request.QueryString["address1"]), Convert.ToString(Page.Request.QueryString["address2"]), Convert.ToString(Page.Request.QueryString["remark"]), Convert.ToInt32(Page.Request.QueryString["status"]), Convert.ToInt32(Page.Request.QueryString["status"]), Convert.ToInt32(Page.Request.QueryString["status"]));
        
        }

        if (Page.Request.QueryString["scheduled"] != null)
        {
            Getschedulerdetail(Convert.ToInt32(Page.Request.QueryString["scheduled"]));
        
        }

        if (Page.Request.QueryString["sendmail"] != null)
        {
            Getmaildetail(Convert.ToInt32(Page.Request.QueryString["sendmail"]));
        
        }


        





        if (Page.Request.QueryString["tag"] != null)
        {
            Write(objtag.getcount().ToString());

        }





    }

    public bool addperson(int spmode, int id, int categoryid, string personname, string email, string phone, string mobile, string zopcode, string companyname, string city, string country, string address1, string address2, string remark, int status)
    {

        try
        {
          
            if (objperson.addperson(spmode, id, categoryid, personname.Trim(), email.Trim(), phone, mobile, zopcode, companyname.Trim(), city, country, address1.Trim(), address2.Trim(), remark.Trim(), status) == true)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        catch (Exception ex)
        {

            return false;
        }

    }

    private void searchperson(int spmode, int id, int categoryid, string personname, string email, string phone, string mobile, string zopcode, string companyname, string city, string country, string address1, string address2, string remark, int status, int pageindex, int pagesize)
    {
        try
        {


            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = objperson.searchperson(spmode, id, categoryid, personname, email, phone, mobile, zopcode, companyname, city, country, address1, address2, remark, status, 10,1);
            dt = ds.Tables["tblperson"];
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            Write(serializer.Serialize(rows));


        }
        catch (Exception ex) { }

    }

    private void Getschedulerdetail( int id)

    {
        try
        {
            string html = "";
            DataSet ds = db.ExecuteDataSet("usp_scheduler", new SqlParameter[] { new SqlParameter("@spmode", 3), new SqlParameter("@id", id) }, "tblschuder");
            if (ds.Tables["tblschuder"].Rows.Count > 0)
            {


                html = html + "<div class='row'><div class='col-lg-12'><label>Mail From: </label>" + ds.Tables["tblschuder"].Rows[0]["email"].ToString() + "</div>  </div>";

                html = html + "<div class='row'><div class='col-lg-12'><label>Mail Category: </label>" + ds.Tables["tblschuder"].Rows[0]["mail_category"].ToString() + "</div>  </div>";
                html = html + "<div class='row'><div class='col-lg-12'><label>Mail subject: </label>" + ds.Tables["tblschuder"].Rows[0]["subject"].ToString() + "</div>  </div>";
                html = html + "<div class='row'><div class='col-lg-12'><label>To: </label>" + ds.Tables["tblschuder"].Rows[0]["to"].ToString() + "</div>  </div>";
                html = html + "<div class='row'><div class='col-lg-12'><label>Mail subject: </label>" + ds.Tables["tblschuder"].Rows[0]["subject"].ToString() + "</div>  </div>";
                html = html + "<div class='row'><div class='col-lg-12'><label>Cc: </label>" + ds.Tables["tblschuder"].Rows[0]["cc"].ToString() + "</div>  </div>";
                html = html + "<div class='row'><div class='col-lg-12'><label>Bcc: </label>" + ds.Tables["tblschuder"].Rows[0]["bcc"].ToString() + "</div>  </div>";

                html = html + "<div class='row'><div class='col-lg-12'><lable>Message: </lable>" + ds.Tables["tblschuder"].Rows[0]["body"].ToString() + "</div>  </div>";
                if (ds.Tables["tblschuder"].Rows[0]["attachment"].ToString()!="")
                {
                    string att = "";
                    string[] data = ds.Tables["tblschuder"].Rows[0]["attachment"].ToString().Split(',');
                    for (int i = 0; i < data.Length - 1;i++ )
                    {
                        att = att + "<a href='/Emanager/mail_attchment/schedule" + data[i] + "'>File-" + i+1 + "</a><br/>";
                    }
                    html = html + "<div class='row'><div class='col-lg-12'><lable>Attachment: </lable>" + att + "</div>  </div>";
                }



            }
            else {
                html = "Record not found";
            
            }
            Write(html);
        
        }
        catch (Exception ex) { }

    
    }
    private void Getmaildetail(int id)

    {
        try
        {
            string html = "";
            DataSet ds = db.ExecuteDataSet("usp_mailhistory", new SqlParameter[] { new SqlParameter("@spmode", 2), new SqlParameter("@mailfrom", id) }, "sendmail");
            if (ds.Tables["sendmail"].Rows.Count > 0)
            {


                html = html + "<div class='row'><div class='col-lg-12'><label>Mail From: </label>" + ds.Tables["sendmail"].Rows[0]["frommail"].ToString() + "</div>  </div>";

                html = html + "<div class='row'><div class='col-lg-12'><label>Mail Category: </label>" + ds.Tables["sendmail"].Rows[0]["mailcategory"].ToString() + "</div>  </div>";
                html = html + "<div class='row'><div class='col-lg-12'><label>Mail subject: </label>" + ds.Tables["sendmail"].Rows[0]["subject"].ToString() + "</div>  </div>";
                html = html + "<div class='row'><div class='col-lg-12'><label>To: </label>" + ds.Tables["sendmail"].Rows[0]["to"].ToString() + "</div>  </div>";
                
                html = html + "<div class='row'><div class='col-lg-12'><label>Cc: </label>" + ds.Tables["sendmail"].Rows[0]["cc"].ToString() + "</div>  </div>";
                html = html + "<div class='row'><div class='col-lg-12'><label>Bcc: </label>" + ds.Tables["sendmail"].Rows[0]["bcc"].ToString() + "</div>  </div>";

                html = html + "<div class='row'><div class='col-lg-12'><lable>Message: </lable>" + ds.Tables["sendmail"].Rows[0]["body"].ToString() + "</div>  </div>";
                if (ds.Tables["sendmail"].Rows[0]["attachment"].ToString() != "")
                {
                    string att = "";
                    string[] data = ds.Tables["sendmail"].Rows[0]["attachment"].ToString().Split(',');
                    for (int i = 0; i < data.Length - 1;i++ )
                    {
                        att = att + "<a  target='_blank' href='mail_attchment/" + data[i] + "'>File-" + i+1 + "</a><br/>";
                    }
                    html = html + "<div class='row'><div class='col-lg-12'><lable>Attachment: </lable>" + att + "</div>  </div>";
                }

                html = html + "<div class='row'><div class='col-lg-12'><a href='sendmail.aspx?send="+ id +"'>Re-send mail  </a></div>  </div>";

            }
            else {
                html = "Record not found";
            
            }
            Write(html);
        
        }
        catch (Exception ex) { }

    
    }

   







    private void Write(string Text)
    {


        Response.Flush();
        Response.Write(Text);
        Response.End();


    }
}