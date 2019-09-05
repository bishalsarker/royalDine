using RoyalDine.Models;
using RoyalDine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalDine.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
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
                if (session.acctype == "a")
                {
                    //Get all members
                    string response = "";
                    string list = "";
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
                        foreach (UsersModel u in usList)
                        {
                            if (u.acctype == "m")
                            {
                                list = list + "<tr><td>" + u.fullname + "</td><td>"
                                        + "<p class='text-danger'>Manager</p>";
                            }
                            else
                            {
                                list = list + "<tr><td>" + u.fullname + "</td><td>"
                                        + "<a class='btn btn-success' data-ajax='true' data-ajax-success='serverResponse' href='" + Url.Action("MakeManager", new { id = u.id}) + "'>Make Manager</a>";
                            }
                        }
                        ViewBag.Data = list;
                        ViewBag.DataCount = usList.Count();
                    }
                    ViewBag.Response = response;
                    return View();
                }
                else
                {
                    return RedirectToAction("AdminLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("AdminLogin", "User");
            }
            
        }
        public ActionResult MakeManager(string id)
        {
            if (Session["user"] != null)
            {
                UserSessionModel session = (UserSessionModel)Session["user"];
                if (session.acctype == "a")
                {
                    //Get present manager
                    UsersModel usm = new UsersModel();
                    usm = usm.findAccTypeM();
                    if (usm.id == null)
                    {
                        usm.id = id;
                        usm.acctype = "m";
                        usm.updateAccTypeByUserId();
                    }
                    else
                    {
                        //Remove previous admin
                        usm.acctype = "u";
                        usm.updateAccTypeByUserId();

                        //Set new admin
                        usm = new UsersModel();
                        usm.id = id;
                        usm.acctype = "m";
                        usm.updateAccTypeByUserId();
                    }

                    if (usm.response == "500")
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
                            msg = "Successfully assigned manager!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return RedirectToAction("AdminLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("AdminLogin", "User");
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
                if (session.acctype == "a")
                {
                    //Get user details
                    string response = "";
                    string data = "";
                    AdminModel am = new AdminModel();
                    am = am.getAdmin();
                    if (am.response == "500")
                    {
                        response = "e";
                    }
                    else
                    {
                        data = am.username;
                    }
                    ViewBag.uN = data;
                    ViewBag.Response = response;
                    return View();
                }
                else
                {
                    return RedirectToAction("AdminLogin", "User");
                }
            }
            else
            {
                return RedirectToAction("AdminLogin", "User");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updateUsername(string u)
        {
            string r = "";
            string m = "";
            if (u == "")
            {
                r = "e";
                m = "Please fill the box!";
            }
            else
            {
                AdminModel am = new AdminModel();
                am.username = u;
                am.updateUsername();
                if (am.response == "500")
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
                msgtype = r,
                msg = m
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updatePassword(string p, string rp)
        {
            string r = "";
            string m = "";
            if (p == "" || rp == "")
            {
                r = "e";
                m = "Please fill all the boxes!";
            }
            else if (p != rp)
            {
                r = "e";
                m = "Password didn't match!";
            }
            else
            {
                AdminModel am = new AdminModel();
                am.password = p;
                am.updatePassword();
                if (am.response == "500")
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
                msgtype = r,
                msg = m
            });
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("AdminLogin", "User");
        }
	}
}