using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_6
{
    public partial class test : System.Web.UI.Page
    {
        Dboperation db = new Dboperation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string _id = Request.QueryString["DesignationId"].ToString();
                DataTable dt = db.exedatatable("Select * from tbl_Designation where DesignationId='" + _id + "'");
                Label1.Text = dt.Rows[0]["DesignationName"].ToString();
                Label2.Text = dt.Rows[0]["DepartmentId"].ToString();
                
            }

        }
    }
}