using RoyalDine.Models;
using RoyalDine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalDine.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel user = (UserSessionModel)Session["user"];
                if (user.acctype == "a")
                {
                    return RedirectToAction("Home", "Admin");
                }
                else if (user.acctype == "m")
                {
                    return RedirectToAction("Home", "Manager");
                }
                else if (user.acctype == "u")
                {
                    return RedirectToAction("Home", "Member");
                }
                else
                {
                    return RedirectToAction("UserLogin");
                }
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }
        public ActionResult UserLogin()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel user = (UserSessionModel)Session["user"];
                if (user.acctype == "a")
                {
                    return RedirectToAction("Home", "Admin");
                }
                else if (user.acctype == "m")
                {
                    return RedirectToAction("Home", "Manager");
                }
                else if (user.acctype == "u")
                {
                    return RedirectToAction("Home", "Member");
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(string Uu, string Up)
        {
            UsersModel um = new UsersModel();
            um.username = Uu;
            um.password = Up;
            bool exists = um.checkIfUserExists();
            um = um.getUserByUsername();

            if (um.response == "500")
            {
                return RedirectToAction("Index", "App", new { type = "se" });
            }
            else
            {
                if (exists)
                {
                    UserSessionModel u = new UserSessionModel();
                    u.userid = um.id;
                    u.acctype = um.acctype;
                    Session["user"] = u;
                    return RedirectToAction("Home", "Member");
                }
                else
                {
                    //t = error type; a = area(admin, user, server)
                    return RedirectToAction("ActionResponse", "App", new { t = "le", a = "u" });
                }
            }
        }
        public ActionResult AdminLogin()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            if (Session["user"] != null)
            {
                UserSessionModel user = (UserSessionModel)Session["user"];
                if (user.acctype == "a")
                {
                    return RedirectToAction("Home", "Admin");
                }
                else if (user.acctype == "m")
                {
                    return RedirectToAction("Home", "Manager");
                }
                else if (user.acctype == "u")
                {
                    return RedirectToAction("Home", "Member");
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
            return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(string Uu, string Up)
        {
            AdminModel am = new AdminModel();
            am.username = Uu;
            am.password = Up;
            bool exists = am.checkIfUserExists();
            am = am.getAdmin();

            if (am.response == "500")
            {
                return RedirectToAction("Index", "App", new { type = "se" });
            }
            else
            {
                if (exists)
                {
                    UserSessionModel u = new UserSessionModel();
                    u.userid = am.id;
                    u.acctype = "a";
                    Session["user"] = u;
                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    return RedirectToAction("ActionResponse", "App", new { t = "le", a = "a" });
                }
            }
        }
        public ActionResult Registration()
        {
            //Run auto meal scheduler
            Tasks task = new Tasks();
            task.autoScheduler();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(string Un, string Uu, string Up, string Pr)
        {
            string r = "";
            string m = "";
            List<string> errList = new List<string>();
            UsersModel usm = new UsersModel();
            usm.username = Uu;
            bool isExist = usm.checkIfUsernameExists();

            //Check for errors
            if (Un == "" || Uu == "" || Up == "" || Pr == "")
            {
                errList.Add("<li>Please fill all the boxes</li>");
            }
            if (isExist)
            {
                errList.Add("<li>User already exists</li>");
            }
            if (Up != Pr)
            {
                errList.Add("<li>Passwords didn't match</li>");
            }

            //Action
            if (errList.Count() < 1)
            {
                //Add user
                UsersModel user = new UsersModel();
                user.fullname = Un;
                user.username = Uu;
                user.password = Up;
                user.addUser();

                //Get user
                user = new UsersModel();
                user.username = Uu;
                string uId = user.getUserByUsername().id;

                //Set settings
                SettingsModel st = new SettingsModel();
                st.breakfast = "0";
                st.lunch = "0";
                st.dinner = "0";
                st.memberId = uId;
                st.addSettings();

                //Set meals
                SelfMealsModel smm = new SelfMealsModel();
                smm.breakfast = "0";
                smm.lunch = "0";
                smm.dinner = "0";
                smm.memberId = uId;
                smm.date = getDate();
                smm.addMeal();

                if (user.response == "500" || usm.response == "500" || st.response == "500" || smm.response == "500")
                {
                    r = "e";
                    m = "Something went wrong! Try again";
                }
                else
                {
                    r = "s";
                    m = "Registration completed successfully! Please login to continue.";
                }
            }
            else
            {
                r = "e";
                m = "<p>Error!</p><ul>";
                foreach (string i in errList)
                {
                    m = m + i;
                }
                m = m + "</ul>";
            }

            return Json(new
            {
                msgtype = r,
                msg = m
            });
        }
        private string getDate()
        {
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"));
            return dt.ToString("dd/MM/yyyy");
        }
	}
}