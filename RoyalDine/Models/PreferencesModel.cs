using RoyalDine.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RoyalDine.Models
{
    public class PreferencesModel
    {
        public string id { get; set; }
        public string mealrate { get; set; }
        public string servicecharge { get; set; }
        public string response { get; set; }
        SqlConnection conn;

        public PreferencesModel()
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
        public PreferencesModel getPrefrences()
        {
            string query = "SELECT * FROM preferences WHERE id='1';";
            PreferencesModel data = new PreferencesModel();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mealrate = reader["mealrate"] + "";
                    servicecharge = reader["servicecharge"] + "";
                }

                reader.Close();
                connClose();
                response = "200";
            }
            else
            {
                response = "500";
            }
            return data;
        }
        public void updatePrefrences()
        {
            string query = "UPDATE preferences SET mealrate='" + this.mealrate + "',servicecharge='" + this.servicecharge + "' WHERE id='1';";
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