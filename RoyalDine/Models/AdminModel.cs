using RoyalDine.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RoyalDine.Models
{
    public class AdminModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        public AdminModel()
        {
            DBConfig db = new DBConfig();
            conn = new SqlConnection(db.getConnStr());
        }
        private bool connOpen()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch(SqlException ex){
                return false;
            }
        }
        private bool connClose()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        public AdminModel getAdmin()
        {
            string query = "SELECT * FROM admin WHERE id='1'";
            AdminModel user = new AdminModel();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.id = reader["id"] + "";
                    user.username = reader["username"] + "";
                }

                reader.Close();
                connClose();
                user.response = "200";
            }
            else
            {
                user.response = "500";
            }
            return user;
        }
        public bool checkIfUserExists()
        {
            string query = "SELECT COUNT(*) FROM admin WHERE username='" + this.username + "' AND password='" + this.password + "';";
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                connClose();
                this.response = "200";
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                this.response = "500";
                return false;
            }
        }
        public void updateUsername()
        {
            string query = "UPDATE admin SET username='" + this.username + "' WHERE id='1';";
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                connClose();
                response = "200";
            }
            else
            {
                response = "500";
            }
        }
        public void updatePassword()
        {
            string query = "UPDATE admin SET password='" + this.password + "' WHERE id='1';";
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                connClose();
                response = "200";
            }
            else
            {
                response = "500";
            }
        }
    }
}