using RoyalDine.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RoyalDine.Models
{
    public class SettingsModel
    {
        public string id { get; set; }
        public string breakfast { get; set; }
        public string lunch { get; set; }
        public string dinner { get; set; }
        public string memberId { get; set; }
        public string response { get; set; }
        SqlConnection conn;

        public SettingsModel()
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
        public void addSettings()
        {
            string query = "INSERT INTO settings(breakfast, lunch, dinner, memberId) VALUES('" + this.breakfast + "', '" + this.lunch + "', '" + this.dinner + "', '" + this.memberId + "');";
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
        public SettingsModel getSettingByMemberId()
        {
            string query = "SELECT * FROM settings WHERE memberId='" + this.memberId + "';";
            SettingsModel data = new SettingsModel();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.id = reader["id"] + "";
                    data.breakfast = reader["breakfast"] + "";
                    data.lunch = reader["lunch"] + "";
                    data.dinner = reader["dinner"] + "";
                }

                reader.Close();
                connClose();
                data.response = "200";
            }
            else
            {
                data.response = "500";
            }
            return data;
        }
        public List<SettingsModel> getAllSettings()
        {
            string query = "SELECT * FROM settings";
            List<SettingsModel> tmpList = new List<SettingsModel>();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmpList.Add(new SettingsModel
                    {
                        id = reader["id"] + "",
                        breakfast = reader["breakfast"] + "",
                        lunch = reader["lunch"] + "",
                        dinner = reader["dinner"] + "",
                        memberId = reader["memberId"] + ""
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
        public void updateSettings()
        {
            string query = "UPDATE settings SET breakfast='" + this.breakfast + "',lunch='" + this.lunch + "',dinner='" + this.dinner + "' WHERE id='" + this.id + "';";
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