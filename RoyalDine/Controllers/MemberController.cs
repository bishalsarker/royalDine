using RoyalDine.Models;
using RoyalDine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalDine.Controllers
{
    public class MemberController : Controller
    {
        //View methods
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "u" || session.acctype == "m")
                {
                    string memberId = session.userid;
                    string todayVal = getTodayVal();
                    getAllMessages(todayVal, memberId);
                    getToday();

                    //Get meals
                    getSelfMeals(memberId);
                    getGuestMeals(memberId);

                    ViewBag.AccType = session.acctype;
                    ViewBag.MemberName = getMemberName();
                    return View();
                }
                else
                {
                    return RedirectToAction("UserLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGuestMeal(string b, string l, string d)
        {
            if (b == null && l == null && d == null)
            {
                return Json(new
                {
                    msgtype = "e",
                    msg = "Please select at least one item..."
                });
            }
            else
            {
                GuestMealsModel gmm = new GuestMealsModel();

                //Breakfst
                if (b != "1")
                {
                    gmm.breakfast = "0";
                }
                else
                {
                    gmm.breakfast = "1";
                }

                //Lunch
                if (l != "1")
                {
                    gmm.lunch = "0";
                }
                else
                {
                    gmm.lunch = "1";
                }

                //Dinner
                if (d != "1")
                {
                    gmm.dinner = "0";
                }
                else
                {
                    gmm.dinner = "1";
                }
                
                //Other info
                UserSessionModel usm = (UserSessionModel)Session["user"];
                gmm.memberId = usm.userid;
                gmm.date = getDate();

                //Add meal
                gmm.addGuestMeal();
                if (gmm.response == "500")
                {
                    return Json(new
                    {
                        msgtype = "e",
                        msg = "Something went wrong! Try again..."
                    });
                }
                else
                {
                    return Json(new
                    {
                        msgtype = "s",
                        msg = "Added successfully!"
                    });
                }
            }
        }
        public ActionResult GuestViewer()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "u" || session.acctype == "m")
                {
                    processGuestDetails();
                    ViewBag.AccType = session.acctype;
                    ViewBag.MemberName = getMemberName();
                    return View();
                }
                else
                {
                    return RedirectToAction("UserLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }
        public ActionResult ModifyGuestMeal(int id)
        {
            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "u" || session.acctype == "m")
                {
                    GuestMealsModel gmm = new GuestMealsModel();
                gmm.id = id.ToString();
                gmm.getMealData();
                if (gmm.response == "500")
                {
                    return Json(new
                    {
                        msgtype = "e",
                        msg = "Something went wrong! Try again..."
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string r = "";
                    if (gmm.breakfast == "1")
                    {
                        r = r + "<input type='checkbox' name='b' value='1' checked> Breakfast<br>";
                    }
                    else
                    {
                        r = r + "<input type='checkbox' name='b' value='1'> Breakfast<br>";
                    }

                    if (gmm.lunch == "1")
                    {
                        r = r + "<input type='checkbox' name='l' value='1' checked> Lunch<br>";
                    }
                    else
                    {
                        r = r + "<input type='checkbox' name='l' value='1'> Lunch<br>";
                    }

                    if (gmm.dinner == "1")
                    {
                        r = r + "<input type='checkbox' name='d' value='1' checked> Dinner<br>";
                    }
                    else
                    {
                        r = r + "<input type='checkbox' name='d' value='1'> Dinner<br>";
                    }

                    r = r + "<input type='hidden' name='mealId' value='" + id + "' />";

                    r = r + "<div class='text-center'>"
                          + "<input type='submit' id='submitInfo' value='Update' class='btn btn-success' /></div>";

                    return Json(new
                    {
                        msgtype = "s",
                        msg = r
                    }, JsonRequestBehavior.AllowGet);
                    }
                    }
                else
                {
                    return RedirectToAction("UserLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyGuestMeal(string mealId, string b, string l, string d)
        {
            if (b == null && l == null && d == null)
            {
                return Json(new
                {
                    msgtype = "e",
                    msg = "Please select at least one item..."
                });
            }
            else
            {
                GuestMealsModel gmm = new GuestMealsModel();

                //Breakfst
                if (b != "1")
                {
                    gmm.breakfast = "0";
                }
                else
                {
                    gmm.breakfast = "1";
                }

                //Lunch
                if (l != "1")
                {
                    gmm.lunch = "0";
                }
                else
                {
                    gmm.lunch = "1";
                }

                //Dinner
                if (d != "1")
                {
                    gmm.dinner = "0";
                }
                else
                {
                    gmm.dinner = "1";
                }

                //Other info
                gmm.id = mealId;

                //Update meal
                gmm.updateMeal();
                if (gmm.response == "500")
                {
                    return Json(new
                    {
                        msgtype = "e",
                        msg = "Something went wrong! Try again..."
                    });
                }
                else
                {
                    return Json(new
                    {
                        msgtype = "s",
                        msg = "Added successfully!"
                    });
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifySelfMeal(string mealId, string b, string l, string d)
        {
            SelfMealsModel smm = new SelfMealsModel();

            //Breakfst
            if (b != "1")
            {
                smm.breakfast = "0";
            }
            else
            {
                smm.breakfast = "1";
            }

            //Lunch
            if (l != "1")
            {
                smm.lunch = "0";
            }
            else
            {
                smm.lunch = "1";
            }

            //Dinner
            if (d != "1")
            {
                smm.dinner = "0";
            }
            else
            {
                smm.dinner = "1";
            }

            //Other info
            smm.id = mealId;

            //Update meal
            smm.updateMeal();
            if (smm.response == "500")
            {
                return Json(new
                {
                    msgtype = "e",
                    msg = "Something went wrong! Try again..."
                });
            }
            else
            {
                return Json(new
                {
                    msgtype = "s",
                    msg = "Added successfully!"
                });
            }
        }
        public ActionResult DeleteGuestMeal(int id)
        {
            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "u" || session.acctype == "m")
                {
                    GuestMealsModel gmm = new GuestMealsModel();
                    gmm.id = id.ToString();
                    gmm.deleteMeal();
                    if (gmm.response == "500")
                    {
                        return Json(new
                        {
                            msgtype = "e",
                            msg = "Something went wrong! Try again..."
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            msgtype = "s",
                            msg = "Deleted successfully!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return RedirectToAction("UserLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }
        public ActionResult ReportViewer(string m, string y)
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "u" || session.acctype == "m")
                {
                    processReport(m, y);
                    ViewBag.AccType = session.acctype;
                    ViewBag.MemberName = getMemberName();
                    return View();
                }
                else
                {
                    return RedirectToAction("UserLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }
        public ActionResult Settings()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "u" || session.acctype == "m")
                {
                    processSettings();
                    ViewBag.AccType = session.acctype;
                    ViewBag.MemberName = getMemberName();
                    return View();
                }
                else
                {
                    return RedirectToAction("UserLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updateMealSchedule(string schId, string b, string l, string d)
        {
            SettingsModel sm = new SettingsModel();
            //Breakfst
            if (b != "1")
            {
                sm.breakfast = "0";
            }
            else
            {
                sm.breakfast = "1";
            }

            //Lunch
            if (l != "1")
            {
                sm.lunch = "0";
            }
            else
            {
                sm.lunch = "1";
            }

            //Dinner
            if (d != "1")
            {
                sm.dinner = "0";
            }
            else
            {
                sm.dinner = "1";
            }

            //Other info
            sm.id = schId;

            //Update meal
            sm.updateSettings();
            if (sm.response == "500")
            {
                return Json(new
                {
                    msgtype = "e",
                    msg = "Something went wrong! Try again..."
                });
            }
            else
            {
                return Json(new
                {
                    msgtype = "s",
                    msg = "Added successfully!"
                });
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("UserLogin", "User");
        }

        //Internel methods
        private string getMemberName()
        {
            UsersModel um = new UsersModel();
            UserSessionModel usm = (UserSessionModel)Session["user"];
            um.id = usm.userid;
            um = um.getUserById();
            if (um.response == "500")
            {
                return "NA";
            }
            else
            {
                return um.fullname;
            }
        }
        private void getAllMessages(string todayVal, string memberId)
        {
            string msg = "";
            string response = "";

            //Check if bazar day
            BazarSchedulesModel bsm = new BazarSchedulesModel();
            bsm.day = todayVal;
            bsm.memberId = memberId;
            bool isBazarDay = bsm.checkIfScheduleExists();

            //Generate message data
            if (bsm.response == "500")
            {
                msg = "<b>Something went wrong!";
                response = "e";
            }
            else
            {
                //Get messages
                List<string> msgs = new List<string>();

                if (isBazarDay == true)
                {
                    msgs.Add("<p>Today is your bazar day</p>");
                }
                
                //Process List
                if (msgs.Count() < 1)
                {
                    msg = "<p>No messages for you today!</p>";
                }
                else
                {
                    foreach (string m in msgs)
                    {
                        msg = msg + m;
                    }
                }
                response = "s";
            }

            ViewBag.MsgData = msg;
            ViewBag.MsgResponse = response;
        }
        private string getTodayVal()
        {
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"));
            int d = (int)dt.DayOfWeek;
            return d.ToString();
        }
        private void getToday()
        {
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"));
            string date = dt.ToString("D");
            ViewBag.Date = date;
        }
        private string getDate()
        {
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"));
            return dt.ToString("dd/MM/yyyy");
        }
        private void getGuestMeals(string mid)
        {
            string response = "";
            GuestMealsModel gmm = new GuestMealsModel();
            List<GuestMealsModel> gmList = new List<GuestMealsModel>();

            //Get all guest meals
            gmm.memberId = mid;
            gmm.date = getDate();
            foreach (GuestMealsModel gm in gmm.getAllGuestMealByMemberIdAndDate())
            {
                gmList.Add(gm);
            }

            //Process result
            if (gmm.response == "500")
            {
                response = "e";
                ViewBag.GuestTotal = 0;
            }
            else
            {
                response = "s";
                //Count meals
                int countB = 0;
                int countL = 0;
                int countD = 0;
                foreach (GuestMealsModel g in gmList)
                {
                    if (g.breakfast == "1")
                    {
                        countB++;
                    }

                    if (g.lunch == "1")
                    {
                        countL++;
                    }

                    if (g.dinner == "1")
                    {
                        countD++;
                    }
                }
                ViewBag.GuestB = countB;
                ViewBag.GuestL = countL;
                ViewBag.GuestD = countD;
                ViewBag.GuestTotal = countB + countL + countD;
            }
            ViewBag.GuestResponse = response;
        }
        private void getSelfMeals(string mid)
        {
            string response = "";
            SelfMealsModel smm = new SelfMealsModel();

            //Get self meal
            smm.memberId = mid;
            smm.date = getDate();

            SelfMealsModel smm2 = new SelfMealsModel();
            smm.getMealByMemberIdAndDate();

            //Process result
            if (smm.response == "500")
            {
                response = "e";
                ViewBag.SelfTotal = 0;
            }
            else
            {
                response = "s";
                //Count meals
                int totalMeal = 0;
                string b = "No";
                string l = "No";
                string d = "No";

                if (smm.breakfast == "1")
                {
                    b = "Yes";
                    totalMeal++;
                }

                if (smm.lunch == "1")
                {
                    l = "Yes";
                    totalMeal++;
                }

                if (smm.dinner == "1")
                {
                    d = "Yes";
                    totalMeal++;
                }
                ViewBag.MealID = smm.id;
                ViewBag.SelfB = b;
                ViewBag.SelfL = l;
                ViewBag.SelfD = d;
                ViewBag.SelfTotal = totalMeal;
            }
            ViewBag.SelfResponse = response;
        }
        private void processGuestDetails()
        {
            string response = "";
            string msg = "";
            GuestMealsModel gmm = new GuestMealsModel();
            List<GuestMealsModel> gmList = new List<GuestMealsModel>();

            //Get all guest meals
            UserSessionModel usm = (UserSessionModel)Session["user"];
            gmm.memberId = usm.userid;
            gmm.date = getDate();
            foreach (GuestMealsModel gm in gmm.getAllGuestMealByMemberIdAndDate())
            {
                gmList.Add(gm);
            }

            //Process result
            if (gmm.response == "500")
            {
                response = "e";
                msg = "<h3>Something went wrong! Try again...</h3>";
            }
            else
            {
                response = "s";
                msg = msg + "<table class='table table-bordered'>"
                          +"<tr>"
                          +"<td><b>#</b></td>"
                          +"<td><b>Breakfast</b></td>"
                          +"<td><b>Lunch</b></td>"
                          +"<td><b>Dinner</b></td>"
                          +"<td><b>Options</b></td>"
                          +"</tr>";

                int count = 1;
                foreach (GuestMealsModel gm in gmList)
                {
                    msg = msg + "<tr><td>" + count + "</td>";
                    if (gm.breakfast == "1")
                    {
                        msg = msg + "<td><p class='text-success'>Y</p></td>";
                    }
                    else
                    {
                        msg = msg + "<td><p class='text-danger'>N</p></td>";
                    }

                    if (gm.lunch == "1")
                    {
                        msg = msg + "<td><p class='text-success'>Y</p></td>";
                    }
                    else
                    {
                        msg = msg + "<td><p class='text-danger'>N</p></td>";
                    }

                    if (gm.dinner == "1")
                    {
                        msg = msg + "<td><p class='text-success'>Y</p></td>";
                    }
                    else
                    {
                        msg = msg + "<td><p class='text-danger'>N</p></td>";
                    }

                    msg = msg + "<td>"
                              + "<a class='btn btn-success' data-ajax='true' data-ajax-success='getMealData' href='" + Url.Action("ModifyGuestMeal", new { id = gm.id}) + "'>Modify</a>"
                              + " <a class='btn btn-danger' data-ajax='true' data-ajax-success='serverResponse' href='" + Url.Action("DeleteGuestMeal", new { id = gm.id }) + "'>Remove</a>"
                              + "</td></tr>";
                    count++;
                }
                msg = msg + "</table>";
            }

            ViewBag.GuestDetailsData = msg;
            ViewBag.GuestDetailsResponse = response;
        }
        private void processSettings()
        {
            //Get meal scheduler data
            string msMsg = "";
            string msResponse = "";
            SettingsModel sm = new SettingsModel();
            UsersModel um = new UsersModel();
            UserSessionModel usm = (UserSessionModel)Session["user"];
            sm.memberId = usm.userid;
            sm = sm.getSettingByMemberId();
            um.id = usm.userid;
            um = um.getUserById();
            if (sm.response == "500" || um.response == "500")
            {
                msResponse = "e";
                msMsg = "Something went wrong! Try again...";
            }
            else
            {
                msResponse = "s";
                string b, l, d;
                string editData = "";
                if (sm.breakfast == "1")
                {
                    b = "Yes";
                    editData = editData + "<input type='checkbox' name='b' value='1' checked> Breakfast<br>";
                }
                else
                {
                    b = "No";
                    editData = editData + "<input type='checkbox' name='b' value='1'> Breakfast<br>";
                }
                if (sm.lunch == "1")
                {
                    l = "Yes";
                    editData = editData + "<input type='checkbox' name='l' value='1' checked> Lunch<br>";
                }
                else
                {
                    l = "No";
                    editData = editData + "<input type='checkbox' name='l' value='1'> Lunch<br>";
                }
                if (sm.dinner == "1")
                {
                    d = "Yes";
                    editData = editData + "<input type='checkbox' name='d' value='1' checked> Dinner<br>";
                }
                else
                {
                    d = "No";
                    editData = editData + "<input type='checkbox' name='d' value='1'> Dinner<br>";
                }
                editData = editData + "<input type='hidden' name='schId' value='" + sm.id + "' />";

                ViewBag.MemberInfo = "<p><b>Name: </b>" + um.fullname + "</p><p><b>Username: </b>" + um.username + "</p>";
                ViewBag.B = b;
                ViewBag.L = l;
                ViewBag.D = d;
                ViewBag.EditInfo = editData;
            }
            ViewBag.msMsg = msMsg;
            ViewBag.msResponse = msResponse;
        }
        private void processReport(string month, string year)
        {
            //Get all meals
            string response = "";
            List<SelfMealsModel> smList = new List<SelfMealsModel>();
            List<GuestMealsModel> gmList = new List<GuestMealsModel>();
            SelfMealsModel smm = new SelfMealsModel();
            GuestMealsModel gmm = new GuestMealsModel();
            UserSessionModel usm = (UserSessionModel)Session["user"];
            smm.memberId = usm.userid;
            gmm.memberId = usm.userid;
            foreach (SelfMealsModel sm in smm.getAllMealByMemberId())
            {
                smList.Add(sm);
            }
            foreach (GuestMealsModel gm in gmm.getAllGuestMealByMemberId())
            {
                gmList.Add(gm);
            }

            //Get preferences
            PreferencesModel pm = new PreferencesModel();
            pm.getPrefrences();
            if (smm.response == "500" || gmm.response == "500" || pm.response == "500")
            {
                response = "e";
            }
            else
            {
                response = "s";
                string showing = "";
                string mealInfo = "";
                string costInfo = "";
                string details = "";
                bool isValid = false;
                List<SelfMealsModel> smlistByMonth = new List<SelfMealsModel>();
                List<GuestMealsModel> gmlistByMonth = new List<GuestMealsModel>();

                //Check for valid date input
                string date = year + "-" + month + "-01";
                DateTime dt2;
                if (DateTime.TryParse(date, out dt2))
                {
                    isValid = true;
                    int inM = (int)dt2.Month;
                    int inY = (int)dt2.Year;
                    month = inM.ToString();
                    year = inY.ToString();
                }
                else
                {
                    isValid = false;
                }

                //List by month
                if (isValid)
                {
                    showing = getMonth(month) + ", " + year;
                    //Get self list by month
                    foreach (SelfMealsModel item in smList)
                    {
                        DateTime dt = DateTime.ParseExact(item.date, "dd/MM/yyyy", null);
                        int m = (int)dt.Month;
                        int y = (int)dt.Year;
                        string dbYear = y.ToString();
                        string dbMonth = m.ToString();
                        if (dbMonth == month && dbYear == year)
                        {
                            smlistByMonth.Add(item);
                        }
                    }

                    //Get guest list by month
                    foreach (GuestMealsModel item in gmList)
                    {
                        DateTime dt = DateTime.ParseExact(item.date, "dd/MM/yyyy", null);
                        int m = (int)dt.Month;
                        int y = (int)dt.Year;
                        string dbYear = y.ToString();
                        string dbMonth = m.ToString();
                        if (dbMonth == month && dbYear == year)
                        {
                            gmlistByMonth.Add(item);
                        }
                    }
                }
                else
                {
                    //Get list by current month
                    showing = "Current Month";
                    month = getCurrentMonth();
                    year = getCurrentYear();
                    foreach (SelfMealsModel item in smList)
                    {
                        DateTime dt = DateTime.ParseExact(item.date, "dd/MM/yyyy", null);
                        int m = (int)dt.Month;
                        int y = (int)dt.Year;
                        string dbYear = y.ToString();
                        string dbMonth = m.ToString();
                        if (dbMonth == month && dbYear == year)
                        {
                            smlistByMonth.Add(item);
                        }
                    }

                    //Get guest list by month
                    foreach (GuestMealsModel item in gmList)
                    {
                        DateTime dt = DateTime.ParseExact(item.date, "dd/MM/yyyy", null);
                        int m = (int)dt.Month;
                        int y = (int)dt.Year;
                        string dbYear = y.ToString();
                        string dbMonth = m.ToString();
                        if (dbMonth == month && dbYear == year)
                        {
                            gmlistByMonth.Add(item);
                        }
                    }
                }

                //Get totals
                int totalSelf = 0;
                int totalGuest = 0;
                foreach (SelfMealsModel s in smlistByMonth)
                {
                    int countB = 0;
                    int countL = 0;
                    int countD = 0;
                    int subTotal = 0;

                    int selfB = 0;
                    int selfL = 0;
                    int selfD = 0;

                    if (s.breakfast == "1")
                    {
                        selfB++;
                    }
                    if (s.lunch == "1")
                    {
                        selfL++;
                    }
                    if (s.dinner == "1")
                    {
                        selfD++;
                    }

                    totalSelf = totalSelf + selfB + selfL + selfD;
                    subTotal = subTotal + selfB + selfL + selfD;

                    countB = countB + selfB;
                    countL = countL + selfL;
                    countD = countD + selfD;

                    //Calculate guest meals
                    string d = s.date;
                    foreach (GuestMealsModel g in gmlistByMonth)
                    {
                        int guestB = 0;
                        int guestL = 0;
                        int guestD = 0;

                        if (g.date == d)
                        {
                            if (g.breakfast == "1")
                            {
                                guestB++;
                            }
                            if (g.lunch == "1")
                            {
                                guestL++;
                            }
                            if (g.dinner == "1")
                            {
                                guestD++;
                            }
                            totalGuest = totalGuest + guestB + guestL + guestD;
                            subTotal = subTotal + guestB + guestL + guestD;
                            countB = countB + guestB;
                            countL = countL + guestL;
                            countD = countD + guestD;
                        }
                    }

                    details = details + "<tr>"
                            + "<td>" + d + "</td>"
                            + "<td>" + countB + "</td>"
                            + "<td>" + countL + "</td>"
                            + "<td>" + countD + "</td>"
                            + "<td>" + subTotal + "</td>"
                            + "</tr>";
                }

                //Generate results
                int totalMeal = totalSelf + totalGuest;
                mealInfo = "<p><b>Total Meal: <span class='text-success'>" + totalMeal + "</span></b></p>"
                        + "<p><b>Self: <span class='text-success'>" + totalSelf + "</span></b></p>"
                        + "<p><b>Guest: <span class='text-success'>" + totalGuest + "</span></b></p>";
                int mealRate = int.Parse(pm.mealrate.Trim());
                int serviceCharge = int.Parse(pm.servicecharge.Trim());
                int totalMealCost = totalMeal * mealRate;
                int totalCost = totalMealCost + serviceCharge;
                costInfo = "<p><b>Meal rate: <span class='text-success'>" + pm.mealrate + " Tk/person</span></b></p>"
                        + "<p><b>Meal cost: <span class='text-success'>" + totalMealCost + " Tk</span></b></p>"
                        + "<p><b>Service charge: <span class='text-success'>" + pm.servicecharge + " Tk/person</span></b></p>"
                        + "<p><b>Total cost: <span class='text-success'>" + totalCost + " Tk</span></b></p>";

                ViewBag.Showing = showing;
                ViewBag.CostInfo = costInfo;
                ViewBag.Info = mealInfo;
                ViewBag.Details = details;
            }
            ViewBag.ReportResponse = response;
        }
        private string getCurrentMonth()
        {
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"));
            int m = (int)dt.Month;
            return m.ToString();
        }
        private string getCurrentYear()
        {
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"));
            int y = (int)dt.Year;
            return y.ToString();
        }
        private string getMonth(string monthVal)
        {
            Dictionary<string, string> dayIndex = new Dictionary<string, string>();
            dayIndex.Add("1", "January");
            dayIndex.Add("2", "February");
            dayIndex.Add("3", "March");
            dayIndex.Add("4", "April");
            dayIndex.Add("5", "May");
            dayIndex.Add("6", "June");
            dayIndex.Add("7", "July");
            dayIndex.Add("8", "August");
            dayIndex.Add("9", "September");
            dayIndex.Add("10", "October");
            dayIndex.Add("11", "November");
            dayIndex.Add("12", "December");

            string result;
            if (dayIndex.TryGetValue(monthVal, out result))
            {
                return result;
            }
            else
            {
                return "NA";
            }
        }
	}
}