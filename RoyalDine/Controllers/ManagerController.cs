using RoyalDine.Models;
using RoyalDine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalDine.Controllers
{
    public class ManagerController : Controller
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
                if (session.acctype == "m")
                {
                    processMealData();
                    getToday();
                    ViewBag.AccType = session.acctype;
                    ViewBag.ManagerName = getManagerName();
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
        public ActionResult BazarSchedular()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    processSchedules();
                    getMemberList();
                    ViewBag.ManagerName = getManagerName();
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
        public ActionResult ModifySchedule(string dayVal, string memberId)
        {
           
            if (dayVal == null || memberId == null)
            {
                return Json(new
                {
                    msgtype = "e",
                    msg = "Please fill all fields!"
                });
            }
            else
            {
                BazarSchedulesModel bsm = new BazarSchedulesModel();
                bsm.day = dayVal;
                bsm.memberId = memberId;
                bsm.updateSchedule();
                if (bsm.response == "500")
                {
                    return Json(new
                    {
                        msgtype = "e",
                        msg = "Something went wrong! Try again.."
                    });
                }
                else
                {
                    return Json(new
                    {
                        msgtype = "s",
                        msg = "Scheduled successfully!"
                    });
                }
            }
        }
        public ActionResult Preferences()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    string response = "";
                    PreferencesModel pm = new PreferencesModel();
                    pm.getPrefrences();
                    if (pm.response == "500")
                    {
                        response = "e";
                    }
                    else
                    {
                        response = "s";
                        ViewBag.MealRate = pm.mealrate;
                        ViewBag.ServiceCharge = pm.servicecharge;
                    }
                    ViewBag.PrefResponse = response;
                    ViewBag.ManagerName = getManagerName();
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
        public ActionResult updatePreferences(string mr, string sc)
        {
            
            string m = "";
            string r = "";

            if (mr == null || sc == null)
            {
                mr = "0";
                sc = "0";
            }
            
            int Mr = 0;
            int Sc = 0;
            bool isValidMr = int.TryParse(mr, out Mr);
            bool isValidSc = int.TryParse(sc, out Sc);
            if (isValidMr == false || isValidSc == false)
            {
                m = "Invalid input";
                r = "e";
            }
            else
            {
                r = "s";
                PreferencesModel pm = new PreferencesModel();
                pm.mealrate = Mr.ToString();
                pm.servicecharge = Sc.ToString();
                pm.updatePrefrences();
                if (pm.response == "500")
                {
                    r = "e";
                    m = "Something went wrong! Try again...";
                }
                else
                {
                    r = "s";
                }
            }

            return Json(new
            {
                msg = m,
                msgtype = r
            });
        }
        public ActionResult ReportViewer(string m, string y)
        {

            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    processReport(m, y);
                    ViewBag.ManagerName = getManagerName();
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
        public ActionResult cancelBreakfast(string m)
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    SelfMealsModel smm = new SelfMealsModel();
                    smm.memberId = m;
                    smm.date = getDate();
                    smm.cancelBreakfastByMemberIdAndDate();
                    if (smm.response == "500")
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
        public ActionResult cancelLunch(string m)
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    SelfMealsModel smm = new SelfMealsModel();
                    smm.memberId = m;
                    smm.date = getDate();
                    smm.cancelLunchByMemberIdAndDate();
                    if (smm.response == "500")
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
        public ActionResult cancelDinner(string m)
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    SelfMealsModel smm = new SelfMealsModel();
                    smm.memberId = m;
                    smm.date = getDate();
                    smm.cancelDinnerByMemberIdAndDate();
                    if (smm.response == "500")
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
        public ActionResult cancelAllBreakfast()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    SelfMealsModel smm = new SelfMealsModel();
                    smm.date = getDate();
                    smm.cancelAllBreakfastByDate();
                    if (smm.response == "500")
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
                            msg = "Success!"
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
        public ActionResult cancelAllLunch()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    SelfMealsModel smm = new SelfMealsModel();
                    smm.date = getDate();
                    smm.cancelAllLunchByDate();
                    if (smm.response == "500")
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
        public ActionResult cancelAllDinner()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "m")
                {
                    SelfMealsModel smm = new SelfMealsModel();
                    smm.date = getDate();
                    smm.cancelAllDinnerByDate();
                    if (smm.response == "500")
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
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("UserLogin", "User");
        }

        //Internel methods
        private void processSchedules()
        {
            string scheduleList = "";
            string response = "";
            BazarSchedulesModel bsm = new BazarSchedulesModel();
            List<BazarSchedulesModel> schedules = new List<BazarSchedulesModel>();
            foreach (BazarSchedulesModel item in bsm.getAllSchedules())
            {
                schedules.Add(item);
            }
            int count = schedules.Count();
            if (count < 0 || bsm.response == "500")
            {
                scheduleList = "<p class='text-danger'><b>Something went wrong!</b></p>";
                response = "e";
            }
            else
            {
                //Prepare list
                scheduleList = scheduleList + "<table class='table table-bordered'><tr><td><b>Day</b></td><td><b>Member</b></td></tr>";
                foreach (BazarSchedulesModel item in schedules)
                {
                    if (item.memberId == "0")
                    {
                        scheduleList = scheduleList + "<tr><td>" + getDay(item.day) + "</td><td>" + "--" + "</td></tr>";
                    }
                    else
                    {
                        scheduleList = scheduleList + "<tr><td>" + getDay(item.day) + "</td><td>" + getMemberName(item.memberId) + "</td></tr>";
                    }
                }
                scheduleList = scheduleList + "</table>";
                response = "s";
            }
            ViewBag.ScheduleData = scheduleList;
            ViewBag.ScheduleResponse = response;
        }
        private void processMealData()
        {
            //Get all member
            string response = "";
            UsersModel usm = new UsersModel();
            List<UsersModel> usList = new List<UsersModel>();
            foreach (UsersModel user in usm.getAllUsers())
            {
                usList.Add(user);
            }
            if (usm.response == "500")
            {
                response = "e";
            }
            else
            {
                response = "s";
                int breakfastT = 0;
                int lunchT = 0;
                int dinnerT = 0;
                int breakfastM = 0;
                int lunchM = 0;
                int dinnerM = 0;
                int breakfastG = 0;
                int lunchG = 0;
                int dinnerG = 0;
                string membersB = "";
                string membersL = "";
                string membersD = "";
                foreach (UsersModel u in usList)
                {
                    //Collect daily meals
                    SelfMealsModel smm = new SelfMealsModel();
                    GuestMealsModel gmm = new GuestMealsModel();
                    smm.memberId = u.id;
                    gmm.memberId = u.id;
                    smm.date = getDate();
                    gmm.date = getDate();

                    //List all meals by member
                    List<SelfMealsModel> smList = new List<SelfMealsModel>();
                    List<GuestMealsModel> gmList = new List<GuestMealsModel>();
                    foreach (SelfMealsModel sm in smm.getAllMealByMemberIdAndDate())
                    {
                        smList.Add(sm);
                    }
                    foreach (GuestMealsModel gm in gmm.getAllGuestMealByMemberIdAndDate())
                    {
                        gmList.Add(gm);
                    }

                    //Get all self meals
                    int sb = 0;
                    int sl = 0;
                    int sd = 0;

                    foreach (SelfMealsModel s in smList)
                    {
                        if (s.breakfast == "1")
                        {
                            sb++;
                        }
                        if (s.lunch == "1")
                        {
                            sl++;
                        }
                        if (s.dinner == "1")
                        {
                            sd++;
                        }
                    }

                    if (sb > 0)
                    {
                        membersB = membersB + "<btn id='b_" + u.id + "' class='btn btn-danger' value='" + Url.Action("cancelBreakfast", new { m = u.id }) + "' name='" + u.fullname + "' onclick='confirmRemovingB(" + u.id + ")'>" + u.fullname + "</btn> ";
                    }
                    if (sl > 0)
                    {
                        membersL = membersL + "<btn id='l_" + u.id + "' class='btn btn-danger' value='" + Url.Action("cancelLunch", new { m = u.id }) + "' name='" + u.fullname + "' onclick='confirmRemovingL(" + u.id + ")'>" + u.fullname + "</btn> ";
                    }
                    if (sd > 0)
                    {
                        membersD = membersD + "<btn id='d_" + u.id + "' class='btn btn-danger' value='" + Url.Action("cancelDinner", new { m = u.id }) + "' name='" + u.fullname + "' onclick='confirmRemovingD(" + u.id + ")'>" + u.fullname + "</btn> ";
                    }

                    breakfastM = breakfastM + sb;
                    lunchM = lunchM + sl;
                    dinnerM = dinnerM + sd;

                    //Get all guest meals
                    int gb = 0;
                    int gl = 0;
                    int gd = 0;

                    foreach (GuestMealsModel g in gmList)
                    {
                        if (g.breakfast == "1")
                        {
                            gb++;
                        }
                        if (g.lunch == "1")
                        {
                            gl++;
                        }
                        if (g.dinner == "1")
                        {
                            gd++;
                        }
                    }

                    breakfastG = breakfastG + gb;
                    lunchG = lunchG + gl;
                    dinnerG = dinnerG + gd;
                }

                breakfastT = breakfastM + breakfastG;
                lunchT = lunchM + lunchG;
                dinnerT = dinnerM + dinnerG;

                //Process all data
                ViewBag.Bt = breakfastT;
                ViewBag.Lt = lunchT;
                ViewBag.Dt = dinnerT;

                ViewBag.Bm = breakfastM;
                ViewBag.Lm = lunchM;
                ViewBag.Dm = dinnerM;

                ViewBag.Bg = breakfastG;
                ViewBag.Lg = lunchG;
                ViewBag.Dg = dinnerG;

                ViewBag.mB = membersB;
                ViewBag.mL = membersL;
                ViewBag.mD = membersD;
            }
            ViewBag.HomeResponse = response;
        }
        private void processReport(string month, string year)
        {
            //Get all users
            string response = "";
            UsersModel usm = new UsersModel();
            List<UsersModel> usList = new List<UsersModel>();

            //Get preferences
            PreferencesModel pm = new PreferencesModel();
            pm.getPrefrences();

            foreach (UsersModel user in usm.getAllUsers())
            {
                usList.Add(user);
            }

            if (usm.response == "500")
            {
                response = "e";
            }
            else
            {
                response = "s";
                int totalMeals = 0;
                int totalMealCost = 0;
                int serviceCharge = 0;
                int totalCost = 0;
                string showing = "";
                string costInfo = "";
                string details = "";

                //Check for valid date input
                bool isValid = false;
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

                //Get member's meal info
                foreach (UsersModel u in usList)
                {
                    List<SelfMealsModel> smList = new List<SelfMealsModel>();
                    List<GuestMealsModel> gmList = new List<GuestMealsModel>();
                    SelfMealsModel smm = new SelfMealsModel();
                    GuestMealsModel gmm = new GuestMealsModel();
                    smm.memberId = u.id;
                    gmm.memberId = u.id;
                    foreach (SelfMealsModel sm in smm.getAllMealByMemberId())
                    {
                        smList.Add(sm);
                    }
                    foreach (GuestMealsModel gm in gmm.getAllGuestMealByMemberId())
                    {
                        gmList.Add(gm);
                    }

                    //List by month and year
                    List<SelfMealsModel> smlistByMonth = new List<SelfMealsModel>();
                    List<GuestMealsModel> gmlistByMonth = new List<GuestMealsModel>();

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
                        if (s.breakfast == "1")
                        {
                            totalSelf++;
                        }
                        if (s.lunch == "1")
                        {
                            totalSelf++;
                        }
                        if (s.dinner == "1")
                        {
                            totalSelf++;
                        }
                    }

                    //Calculate guest meals
                    foreach (GuestMealsModel g in gmlistByMonth)
                    {
                        if (g.breakfast == "1")
                        {
                            totalGuest++;
                        }
                        if (g.lunch == "1")
                        {
                            totalGuest++;
                        }
                        if (g.dinner == "1")
                        {
                            totalGuest++;
                        }
                    }

                    //Process subtotal details
                    int subTotalMeals = totalSelf + totalGuest;
                    int subTotalCost = int.Parse(pm.mealrate) * subTotalMeals;
                    details = details + "<tr>"
                                + "<td>" + u.fullname + "</td>"
                                + "<td>" + subTotalMeals + "</td>"
                                + "<td>" + subTotalCost + " Tk</td>"
                                + "</tr>";
                    ViewBag.Details = details;

                    //Process total cost info
                    totalMeals = totalMeals + subTotalMeals;
                    totalMealCost = totalMealCost + subTotalCost;
                    serviceCharge = serviceCharge + int.Parse(pm.servicecharge);
                }

                //Process info
                totalCost = totalMealCost + serviceCharge;
                costInfo = costInfo + "<h3 class='text-danger'>Total Meal: <span class='text-success'>" + totalMeals + "</span></h3>"
                                    + "<h3 class='text-danger'>Meal Rate: <span class='text-success'>" + pm.mealrate + " Tk</span></h3>"
                                    + "<h3 class='text-danger'>Total Meal Cost: <span class='text-success'>" + totalMealCost + " Tk</span></h3>"
                                    + "<h3 class='text-danger'>Service Charge: <span class='text-success'>" + serviceCharge + " Tk</span></h3><hr />"
                                    + "<h3 class='text-danger'>Total Cost: <span class='text-success'>" + totalCost + " Tk</span></h3>";
                ViewBag.CostInfo = costInfo;
                ViewBag.Showing = showing;
            }
            ViewBag.ReportResponse = response;
        }
        private string getDay(string dayVal)
        {
            Dictionary<string, string> dayIndex = new Dictionary<string, string>();
            dayIndex.Add("0", "Sunday");
            dayIndex.Add("1", "Monday");
            dayIndex.Add("2", "Tuesday");
            dayIndex.Add("3", "Wednesday");
            dayIndex.Add("4", "Thursday");
            dayIndex.Add("5", "Friday");
            dayIndex.Add("6", "Saturday");

            string result;
            if (dayIndex.TryGetValue(dayVal, out result))
            {
                return result;
            }
            else
            {
                return "NA";
            }
        }
        private string getMemberName(string memberId)
        {
            UsersModel user = new UsersModel();
            user.id = memberId;
            user = user.getUserById();
            string memberName = "";
            if (user.response == "500")
            {
                memberName = "NA";
            }
            else
            {
                memberName = user.fullname;
            }
            return memberName;
        }
        private string getManagerName()
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
        private void getMemberList()
        {
            UsersModel usm = new UsersModel();
            List<UsersModel> tmpList = new List<UsersModel>();
            foreach (UsersModel user in usm.getAllUsers())
            {
                tmpList.Add(user);
            }
            int count = tmpList.Count();
            string memberList = "";
            string response = "";
            if (count < 1 && usm.response == "500")
            {
                response = "e";
                memberList = "<option>--</option>";
            }
            else if (count < 1 && usm.response == "200")
            {
                response = "s";
                memberList = "<option>--</option>";
            }
            else
            {
                response = "s";
                foreach (UsersModel item in tmpList)
                {
                    memberList = memberList + "<option value='" + item.id + "'>" + item.fullname + "</option>";
                }
            }

            ViewBag.MemberListData = memberList;
            ViewBag.MemberListResponse = response;
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
        
	}
}