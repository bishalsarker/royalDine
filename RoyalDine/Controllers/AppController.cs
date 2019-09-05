using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalDine.Controllers
{
    public class AppController : Controller
    {
        //
        // GET: /App/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ActionResponse(string t, string a)
        {
            string rType = "";
            string msg = "";
            string actionTxt = "";
            string actionLink = "";

            if (t == "se")
            {
                rType = "Error!";
                msg = "Something went wrong!";
                actionTxt = "Try again";
                actionLink = Url.Action("AdminLogin", "User");
            }

            if (t == "le")
            {
                msg = "Wrong username and password";
                actionTxt = "Try again";
                if (a == "a")
                {
                    rType = "Admin Login";
                    actionLink = Url.Action("AdminLogin", "User");
                }

                if (a == "u")
                {
                    rType = "User Login";
                    actionLink = Url.Action("UserLogin", "User");
                }
            }

            ViewBag.ResponseType = rType;
            ViewBag.Msg = msg;
            ViewBag.ActionTxt = actionTxt;
            ViewBag.ActionLink = actionLink;
            return View();
        }
	}
}