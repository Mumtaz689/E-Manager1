using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class main : System.Web.UI.MasterPage
{
    HttpCookie cookie = new HttpCookie("email");
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {


            if (Request.Cookies["email"].Value == null)
            {

                if (Session["email"] == null)
                {
                    Response.Redirect("default.aspx");
                }
            }
            else
            {
                Session.Add("email", Request.Cookies["email"].Value);
            }
        }
        catch (Exception ex) {

            if (Session["email"] == null)
            {
                Response.Redirect("default.aspx");
            }
        }

    }
    protected void lnksignout_Click(object sender, EventArgs e)
    {
        try {


            HttpCookie cookie = new HttpCookie("email");
            cookie.Value = "test@gmail.com";
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.SetCookie(cookie);


            Session.Clear();
            Session.Abandon();
            Response.Redirect("default.aspx");
        
        }
        catch (Exception ex) { }
    }
}
