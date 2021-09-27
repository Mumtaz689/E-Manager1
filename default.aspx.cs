using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
cls_account objac = new cls_account();
protected void Page_Load(object sender, EventArgs e)
{
   
}
protected void btnlogin_Click(object sender, EventArgs e)
{
try
{
if(txtemail.Text.Trim()==string.Empty || txtpwd.Text.Trim()==string.Empty){
lblmsg.Text = "Enter login detail";
return;
}


DataSet ds = objac.Getlogin("exec us_login 1,'"+ txtemail.Text.Trim() +"','"+ txtpwd.Text.Trim() +"'", "login");
if (ds.Tables["login"].Rows.Count > 0)
{

if (chkremember.Checked == true)
{
HttpCookie cookieslogin = new HttpCookie("email");
cookieslogin.Value = ds.Tables["login"].Rows[0]["email"].ToString();
cookieslogin.Expires = DateTime.Now.AddHours(24);
Response.Cookies.Add(cookieslogin);

Response.Cookies["email"].Value = ds.Tables["login"].Rows[0]["email"].ToString();
                
}

Session.Add("email", ds.Tables["login"].Rows[0]["email"].ToString());
Response.Redirect("dashboard.aspx");

}
else {
lblmsg.Text = "Invalid login detail";
return;
}


          
}
catch (Exception ex) { }
}
}