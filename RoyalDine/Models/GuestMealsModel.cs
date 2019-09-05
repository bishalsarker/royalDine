using RoyalDine.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RoyalDine.Models
{
    public class GuestMealsModel
    {
        public string id { get; set; }
        public string breakfast { get; set; }
        public string lunch { get; set; }
        public string dinner { get; set; }
        public string memberId { get; set; }
        public string date { get; set; }
        public string response { get; set; }
        SqlConnection conn;

        public GuestMealsModel()
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
        public void addGuestMeal()
        {
            string query = "INSERT INTO guestmeals(breakfast, lunch, dinner, memberId, date) VALUES('" + this.breakfast + "', '" + this.lunch + "', '" + this.dinner + "', '" + this.memberId + "', '" + this.date + "');";
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
        public List<GuestMealsModel> getAllGuestMealByMemberIdAndDate()
        {
            string query = "SELECT * FROM guestmeals WHERE memberId='" + this.memberId + "' AND date='" + this.date + "';";
            List<GuestMealsModel> tmpList = new List<GuestMealsModel>();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmpList.Add(new GuestMealsModel
                    {
                        id = reader["id"] + "",
                        breakfast = reader["breakfast"] + "",
                        lunch = reader["lunch"] + "",
                        dinner = reader["dinner"] + "",
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
        public List<GuestMealsModel> getAllGuestMealByMemberId()
        {
            string query = "SELECT * FROM guestmeals WHERE memberId='" + this.memberId + "';";
            List<GuestMealsModel> tmpList = new List<GuestMealsModel>();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmpList.Add(new GuestMealsModel
                    {
                        id = reader["id"] + "",
                        breakfast = reader["breakfast"] + "",
                        lunch = reader["lunch"] + "",
                        dinner = reader["dinner"] + "",
                        date = reader["date"] + ""
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
        public GuestMealsModel getMealData()
        {
            string query = "SELECT * FROM guestmeals WHERE id='" + this.id + "';";
            GuestMealsModel data = new GuestMealsModel();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {                 
                    breakfast = reader["breakfast"] + "";
                    lunch = reader["lunch"] + "";
                    dinner = reader["dinner"] + "";
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
        public void updateMeal()
        {
            string query = "UPDATE guestmeals SET breakfast='" + this.breakfast + "',lunch='" + this.lunch + "',dinner='" + this.dinner + "' WHERE id='" + this.id + "';";
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
        public void deleteMeal()
        {
            string query = "DELETE FROM guestmeals WHERE id='" + this.id + "';";
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