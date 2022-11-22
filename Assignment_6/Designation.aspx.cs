using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_6
{
    public partial class Department : System.Web.UI.Page
    {
        Dboperation db = new Dboperation();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con1 = new SqlConnection();
                con1.ConnectionString = "Data Source=NTP-LAP-683\\SQL_SERVER;Initial Catalog=Employee;Integrated Security=True;Pooling=False";
                con1.Open();
                DropDownList1.DataSource = db.exedataset("Select * from tbl_Department");
                DropDownList1.DataTextField = "DepartmentName";
                DropDownList1.DataValueField = "DepartmentId";
                DropDownList1.DataBind();
                GridView1.DataSource = db.exedataset("Select * from tbl_Designation");
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(DropDownList1.SelectedItem.Value);
            String sql = "insert into tbl_Designation values('" + TextBox1.Text + "','" + id+ "')";
            int i = db.exenonquery(sql);
            if (i == 1)
            {
                Response.Write("Successfully Inserted");
            }
            else
            {
                Response.Write("Insertion Failed");
            }
            GridView1.DataSource = db.exedataset("Select * from tbl_Designation");

            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;//changing grid view to edit mode and set edit index as from event
            GridView1.DataSource = db.exedataset("select * from tbl_Designation");//take value from table
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string DesignationId = GridView1.DataKeys[e.RowIndex].Value.ToString();

            TextBox txtDesignation = new TextBox();

            txtDesignation = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];

            TextBox txtDepartment = new TextBox();
            txtDepartment = (TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0];

           

            db.exenonquery("update tbl_Designation set DesignationName='" + txtDesignation.Text + "',DepartmentId='" + txtDepartment.Text + "'where DesignationId="+DesignationId+"");


            GridView1.EditIndex = -1;
            GridView1.DataSource = db.exedataset("Select * from tbl_Designation");
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataSource = db.exedataset("Select * from tbl_Designation");
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string _id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            db.exenonquery("delete from tbl_Designation where DesignationId='" + _id + "'");
            GridView1.DataSource = db.exedataset("Select * from tbl_Designation");
            GridView1.DataBind();
        }
    }
}