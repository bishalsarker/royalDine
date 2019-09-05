using RoyalDine.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RoyalDine.Models
{
    public class SelfMealsModel
    {
        public string id { get; set; }
        public string breakfast { get; set; }
        public string lunch { get; set; }
        public string dinner { get; set; }
        public string memberId { get; set; }
        public string date { get; set; }
        public string response { get; set; }
        public string r { get; set; }
        SqlConnection conn;

        public SelfMealsModel()
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
        public void addMeal()
        {
            string query = "INSERT INTO selfmeals(breakfast, lunch, dinner, memberId, date) VALUES('" + this.breakfast + "', '" + this.lunch + "', '" + this.dinner + "', '" + this.memberId + "', '" + this.date + "');";
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

        public void addMultipleMeal(string q)
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
        public List<SelfMealsModel> getAllMealByMemberIdAndDate()
        {
            string query = "SELECT * FROM selfmeals WHERE memberId='" + this.memberId + "' AND date='" + this.date + "';";
            List<SelfMealsModel> tmpList = new List<SelfMealsModel>();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmpList.Add(new SelfMealsModel
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
        public List<SelfMealsModel> getAllMealByMemberId()
        {
            string query = "SELECT * FROM selfmeals WHERE memberId='" + this.memberId + "';";
            List<SelfMealsModel> tmpList = new List<SelfMealsModel>();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmpList.Add(new SelfMealsModel
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
        public SelfMealsModel getMealByMemberIdAndDate()
        {
            string query = "SELECT * FROM selfmeals WHERE memberId='" + this.memberId + "' AND date='" + this.date + "';";
            SelfMealsModel data = new SelfMealsModel();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader["id"] + "";
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
        public SelfMealsModel getMealData()
        {
            string query = "SELECT * FROM selfmeals WHERE id='" + this.id + "';";
            SelfMealsModel data = new SelfMealsModel();
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
            string query = "UPDATE selfmeals SET breakfast='" + this.breakfast + "',lunch='" + this.lunch + "',dinner='" + this.dinner + "' WHERE id='" + this.id + "';";
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
        public void cancelBreakfastByMemberIdAndDate()
        {
            string query = "UPDATE selfmeals SET breakfast='0' WHERE memberId='" + this.memberId + "' AND date='" + this.date + "';";
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
        public void cancelLunchByMemberIdAndDate()
        {
            string query = "UPDATE selfmeals SET lunch='0' WHERE memberId='" + this.memberId + "' AND date='" + this.date + "';";
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
        public void cancelDinnerByMemberIdAndDate()
        {
            string query = "UPDATE selfmeals SET dinner='0' WHERE memberId='" + this.memberId + "' AND date='" + this.date + "';";
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
        public void cancelAllBreakfastByDate()
        {
            string query = "UPDATE selfmeals SET breakfast='0' WHERE date='" + this.date + "';";
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                connClose();
                response = "200";
                r = query;
            }
            else
            {
                response = "500";
            }
        }
        public void cancelAllLunchByDate()
        {
            string query = "UPDATE selfmeals SET lunch='0' WHERE date='" + this.date + "';";
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
        public void cancelAllDinnerByDate()
        {
            string query = "UPDATE selfmeals SET dinner='0' WHERE date='" + this.date + "';";
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
            string query = "DELETE FROM selfmeals WHERE id='" + this.id + "';";
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
        public bool checkIfEntryExists()
        {
            string query = "SELECT COUNT(*) FROM selfmeals WHERE date='" + this.date + "' AND memberId='" + this.memberId + "';";
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