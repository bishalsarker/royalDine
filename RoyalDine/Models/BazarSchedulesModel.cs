using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RoyalDine.Sql;

namespace RoyalDine.Models
{
    public class BazarSchedulesModel
    {
        public string id { get; set; }
        public string day { get; set; }
        public string memberId { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        public BazarSchedulesModel()
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
        public bool checkIfScheduleExists()
        {
            string query = "SELECT COUNT(*) FROM bazarschedules WHERE day='" + this.day + "' AND memberId='" + this.memberId + "';";
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
        public List<BazarSchedulesModel> getAllSchedules()
        {
            string query = "SELECT * FROM bazarschedules";
            List<BazarSchedulesModel> tmpList = new List<BazarSchedulesModel>();
            if (connOpen() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmpList.Add(new BazarSchedulesModel
                    {
                        day = reader["day"] + "",
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
        public void updateSchedule()
        {
            string query = "UPDATE bazarschedules SET memberId = '" + this.memberId + "' WHERE day = '" + this.day + "';";
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