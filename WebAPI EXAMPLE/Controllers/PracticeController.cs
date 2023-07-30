using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using WebAPI_EXAMPLE.Models;

namespace WebAPI_EXAMPLE.Controllers
{
    public class PracticeController : ApiController
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-IGCVHMJQ\\SQLEXPRESS;Initial Catalog = Virtusa; Integrated Security = true");
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        SqlDataAdapter adapter = null;

        public List<Students> slist = new List<Students>();
        // GET: api/Practice
       public List<Students> Get()
        {
            cmd = new SqlCommand();
            cmd.CommandText = "select * from Students";
            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();
            while(reader.Read() == true)
            {
                Students s = new Students();

                s.Sno = int.Parse(reader["Sno"].ToString());
                s.Sname = reader["Sname"].ToString();
                s.Saddr = reader["Saddr"].ToString() ;
                s.Gender = reader["Gender"].ToString();



                slist.Add(s);
            }
            conn.Close();   
            return slist;
        }

      // GET: api/Practice/5
        public Students Get(int id)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "Select * from Students where Sno = '"+id+"' ";
            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();
            Students s = new Students();
            if (reader.Read() == true)
            {

                s.Sno = int.Parse(reader["Sno"].ToString());
                s.Sname = reader["Sname"].ToString();
                s.Saddr = reader["Saddr"].ToString();
                s.Gender = reader["Gender"].ToString();

            }
            conn.Close();
            return s;
        } 

        // POST: api/Practice
        public void Post([FromBody]Students s)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "insert into Students(Sno,Sname, Saddr, Gender) values('" + s.Sno + "','" + s.Sname + "','" + s.Saddr + "','" + s.Gender + "')";
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // PUT: api/Practice/5
        public string Put(int id, [FromBody]Students s)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "update Students set Sno = '"+s.Sno+"', Sname = '"+s.Sname+"', Saddr = '"+s.Saddr+"', Gender = '"+s.Gender+"' where Sno = '"+id+"' ";
            cmd.Connection = conn;
            conn.Open();
           int rowaffect =  cmd.ExecuteNonQuery();
            conn.Close();
            if(rowaffect > 0)
            {
                return "updated successfully";

            }
            else
            {
                return "not updated";
            }
        }

        // DELETE: api/Practice/5
        public string Delete(int id)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "Delete from Students where Sno = '" + id + "'";
            cmd.Connection = conn;
            conn.Open();
            int e = cmd.ExecuteNonQuery();
            conn.Close();   
            if (e > 0)
            {
                return "deleted successfully";
            }
            else
            {
                return "Deletion failed";
            }
        }
     
    }
}
