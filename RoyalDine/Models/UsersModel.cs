using RoyalDine.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RoyalDine.Models
{
    public class UsersModel
    {
        public string id { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string acctype { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        public UsersModel()
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
        public void addUser()
        {
            string query = "INSERT INTO users(fullname, username, password, acctype) VALUES('" + this.fullname + "', '" + this.username + "', '" + this.password + "', 'u');";
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
        public List<UsersModel> getAllUsers()
        {
            string query = "SELECT * FROM users";
            List<UsersModel> tmpList = new List<UsersModel>();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmpList.Add(new UsersModel
                    {
                        id = reader["id"] + "",
                        fullname = reader["fullname"] + "",
                        acctype = reader["acctype"] + ""
                    });
                }

                reader.Close();
                connClose();
                response = "200";
            }
            else
            {
                response = "500";
            }
            return tmpList;
        }
        public UsersModel getUserById()
        {
            string query = "SELECT * FROM users WHERE id='" + this.id + "'";
            UsersModel user = new UsersModel();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.fullname = reader["fullname"] + "";
                    user.username = reader["username"] + "";
                    user.password = reader["password"] + "";
                    user.acctype = reader["acctype"] + "";
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
        public UsersModel getUserByUsername()
        {
            string query = "SELECT * FROM users WHERE username='" + this.username + "'";
            UsersModel user = new UsersModel();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.id = reader["id"] + "";
                    user.fullname = reader["fullname"] + "";
                    user.acctype = reader["acctype"] + "";
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
        public void updateAccTypeByUserId()
        {
            string query = "UPDATE users SET acctype='" + this.acctype + "' WHERE id='" + this.id + "';";
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
        public UsersModel findAccTypeM()
        {
            string query = "SELECT * FROM users WHERE acctype='m'";
            UsersModel user = new UsersModel();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.id = reader["id"] + "";
                    user.fullname = reader["fullname"] + "";
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
        public bool checkIfUsernameExists()
        {
            string query = "SELECT COUNT(*) FROM users WHERE username='" + this.username + "';";
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
        public bool checkIfUserExists()
        {
            string query = "SELECT COUNT(*) FROM users WHERE username='" + this.username + "' AND password='" + this.password + "';";
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
    }
}