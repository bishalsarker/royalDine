using RoyalDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoyalDine.Tools
{
    public class Tasks
    {
        public void autoScheduler()
        {
            //Check if auto scheduler has already been started
            MealSchedulerLogModel ms = new MealSchedulerLogModel();
            ms.date = getDate();
            bool checkIfAlreadyCheckedToday = ms.checkIfDateExists();
            
            /*
            if (ms.response == "500")
            {
                checkIfAlreadyCheckedToday = true;
            }
            */

            if (checkIfAlreadyCheckedToday == false && ms.response == "200")
            {
                //Get all not checked dates
                MealSchedulerLogModel ms2 = new MealSchedulerLogModel();
                ms2 = ms2.getLastEntry();
                string lastDate = ms2.date;
                string presentDate = getDate();

                //Collect all dates
                List<string> dates = new List<string>();
                DateTime dt = DateTime.ParseExact(lastDate, "dd/MM/yyyy", null);
                dt = dt.AddDays(1);
                while (true)
                {
                    string d = dt.ToString("dd/MM/yyyy");
                    if (d != presentDate)
                    {
                        dates.Add(d);
                        dt = dt.AddDays(1);
                    }
                    else
                    {
                        dates.Add(d);
                        break;
                    }
                }

                //Get all members
                UsersModel um = new UsersModel();
                List<string> allMemberId = new List<string>();
                foreach (UsersModel mId in um.getAllUsers())
                {
                    allMemberId.Add(mId.id);
                }

                //Get all settings
                SettingsModel sm = new SettingsModel();
                List<SettingsModel> smList = new List<SettingsModel>();
                foreach (SettingsModel s in sm.getAllSettings())
                {
                    smList.Add(s);
                }

                //Start adding meals
                if (ms2.response != "500" || um.response != "500" || sm.response != "500")
                {
                    string selfmealQuery = "";
                    string mealLogQuery = "";

                    foreach (string date in dates)
                    {
                        foreach (string m in allMemberId)
                        {
                            string b = "";
                            string l = "";
                            string d = "";
                            foreach (SettingsModel setting in smList)
                            {
                                if (setting.memberId == m)
                                {
                                    b = setting.breakfast;
                                    l = setting.lunch;
                                    d = setting.dinner;
                                    break;
                                }
                            }
                            selfmealQuery = selfmealQuery + "INSERT INTO selfmeals(breakfast, lunch, dinner, memberId, date) VALUES('" + b + "', '" + l + "', '" + d + "', '" + m + "', '" + date + "');";
                        }
                        mealLogQuery = mealLogQuery + "INSERT INTO mealschedulerlog(date, checked) VALUES('" + date + "','1');";
                    }

                    SelfMealsModel smm = new SelfMealsModel();
                    smm.addMultipleMeal(selfmealQuery);
                    MealSchedulerLogModel mlm = new MealSchedulerLogModel();
                    mlm.addMultipleCheckMark(mealLogQuery);
                }
                
                /*
                //Add meals
                foreach (string date in dates)
                {
                    foreach (string m in allMemberId)
                    {
                        //Get preferences
                        SettingsModel sm = new SettingsModel();
                        sm.memberId = m;
                        sm = sm.getSettingByMemberId();
                        string b = sm.breakfast;
                        string l = sm.lunch;
                        string d = sm.dinner;

                        //Insert meals
                        SelfMealsModel smm = new SelfMealsModel();
                        string mid = m;
                        string dT = date;

                        smm.memberId = m;
                        smm.date = date;
                        bool checkIfAlreadyInserted = smm.checkIfEntryExists();

                        if (checkIfAlreadyInserted == false)
                        {
                            smm.memberId = mid;
                            smm.date = dT;
                            smm.breakfast = b;
                            smm.lunch = l;
                            smm.dinner = d;
                            smm.addMeal();
                        }
                    }

                    MealSchedulerLogModel ms3 = new MealSchedulerLogModel();
                    string logDate = date;
                           
                    ms3.date = date;
                    bool checkIfLogged = ms3.checkIfDateExists();

                    if (checkIfLogged == false)
                    {
                        ms3.date = logDate;
                        ms3.check = "1";
                        ms3.addCheckMark();
                    }
                }
                */
            }
        }
        private string getDate()
        {
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"));
            return dt.ToString("dd/MM/yyyy");
        }
    }
}