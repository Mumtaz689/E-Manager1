using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class changepassword : System.Web.UI.Page
{
    cls_account objac = new cls_account();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try {
            if (txtconfirm.Text.Trim() == string.Empty || txtnewpwd.Text.Trim() == string.Empty) {
                lblmessage.Text = "Please enter password";
                return;
            }
            if (txtconfirm.Text.Trim()!= txtnewpwd.Text.Trim() )
            {
                lblmessage.Text = "Password and confirm password detail mismatch";
                return;
            }


            if ( Convert.ToInt32(txtnewpwd.Text.Length) <5)
            {
                lblmessage.Text = "Password too short password should not be less than 5 characters";
                return;
            }





            if (Session["email"] != null)
            {

                if (objac.update_pwd(2, Session["email"].ToString(), txtnewpwd.Text.Trim()))
                {
                    lblmessage.Text = "Password has been updated successfully";
                    return;
                }
                else
                {

                    lblmessage.Text = "We are getting some problem, Please contact to administrator";
                    return;
                }
            }
            else {
                lblmessage.Text = "Your session has been expire please re-login for change password";
                return;
            
            }
        
        }
        catch (Exception ex) { }
    }
}