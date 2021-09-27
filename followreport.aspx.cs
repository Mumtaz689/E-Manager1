using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DB;
using System.IO;

public partial class followreport : System.Web.UI.Page
{
    DBHelper db = new DBHelper();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            txtdateform.Text = System.DateTime.Now.AddDays(0).ToString("MM/dd/yyyy");
            txtdateto.Text = System.DateTime.Now.AddDays(0).ToString("MM/dd/yyyy");
            gridbind();
        
        }

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {

            gridbind();
        }

        catch (Exception ex) { }
    }
    private void gridbind()
    {
        try
        {
            string query = @"

select *,
(select top 1 

case when contactby=0 
then 'Mobile' else 
case when contactby=1
 then 'Email' else 
 case when contactby=2 
 then 'Visit office' else 
 case when contactby=3 then 'Other' else 'N/A' end end end end as contactby

 from  TBL_Followup where TBL_Followup.PersonID=m.PersonID and 


m.followupdate=  

convert(varchar(50),
(cast(convert(varchar(50),nextfollowupdate,101) as datetime)),101)

)
as t
 from  (
select convert(varchar(50),
max(cast(convert(varchar(50),nextfollowupdate,101) as datetime)),101) as followupdate,

PersonID,tbl_m_person.person_name,tbl_m_person.email,tbl_m_person.mobile


from  TBL_Followup
left join tbl_m_person on tbl_m_person.ID=TBL_Followup.PersonID where nextfollowupdate!=''
group by PersonID,PersonID,tbl_m_person.person_name,
tbl_m_person.email,tbl_m_person.mobile

) as m

where  
cast(convert(varchar(50),followupdate,101) as datetime)
 between cast(convert(varchar(50),'"+ txtdateform.Text.Trim() +@"',101) as datetime)
  and cast(convert(varchar(50),'" + txtdateto.Text.Trim() + @"',101) as datetime)";




            DataSet ds = new DataSet();
            ds = db.GetDataSet(query, "person");
            if (ds.Tables["person"].Rows.Count > 0)
            {
                gdvfollowreport.DataSource = ds.Tables["person"];
                lblmsg.Text = ds.Tables["person"].Rows.Count + " Record Found";
                gdvfollowreport.DataBind();

              
            }

            else {

                gdvfollowreport.DataSource = string.Empty;
                lblmsg.Text = "Record Not Found";
                gdvfollowreport.DataBind();
            }
        }
        catch (Exception ex) { }
    
    }


    protected void lnkexport_Click(object sender, EventArgs e)
    {
        try
        {

            string query = " select  personname,email,remark contactby,nextfollowupdate from  dbo.TBL_Followup";

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
                "attachment;filename=DataTable.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Charset = "";
            Response.ContentType = "application/vnd.pdf";




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