using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Assignment_6
{
    public class Dboperation
    {
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();

        public Dboperation()
        {
            con.ConnectionString = "Data Source=NTP-LAP-683\\SQL_SERVER;Initial Catalog=Employee;Integrated Security=True;Pooling=False";
            cmd.Connection = con;
        }

        public SqlConnection Getcon()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        public void dbclose()
        {
            con.Close();
        }

        //Two_Tier Architecture

        public int exenonquery(String Sql)
        {
            Getcon();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = Sql;
            int i = cmd.ExecuteNonQuery();
            return i;
        }

        //DISCONNECTED ARCHITECTURE


        public DataSet exedataset(String sql)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void fillgrid(String sql, GridView dv)
        {
            dv.DataSource = exedataset(sql);
            dv.DataBind();
        }
        public DataTable exedatatable(String sql)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public object exescalar(String sql)
        {
            Getcon();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            object ob = cmd.ExecuteScalar();
            return ob;
        }
    }
}