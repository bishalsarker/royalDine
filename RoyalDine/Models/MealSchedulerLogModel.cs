using RoyalDine.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RoyalDine.Models
{
    public class MealSchedulerLogModel
    {
        public string id { get; set; }
        public string date { get; set; }
        public string check { get; set; }
        public string response{ get; set; }

        SqlConnection conn;

        public MealSchedulerLogModel()
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

        public bool checkIfDateExists()
        {
            string query = "SELECT COUNT(*) FROM mealschedulerlog WHERE date='" + this.date + "';";
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
        public MealSchedulerLogModel getLastEntry()
        {
            MealSchedulerLogModel data = new MealSchedulerLogModel();
            string query = "SELECT TOP 1 * FROM mealschedulerlog ORDER BY id DESC";
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.date = reader["date"] + "";
                    data.check = reader["checked"] + "";
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
        public void addCheckMark()
        {
            string query = "INSERT INTO mealschedulerlog(date, checked) VALUES('" + this.date + "','" + this.check + "');";
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
        public void addMultipleCheckMark(string q)
        {
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(q, conn);
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