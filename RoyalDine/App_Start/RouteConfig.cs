using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RoyalDine
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );
             * */

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "User Login",
                url: "login/user",
                defaults: new { controller = "User", action = "UserLogin", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin Login",
                url: "login/admin",
                defaults: new { controller = "User", action = "AdminLogin", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Registration",
                url: "resgistration",
                defaults: new { controller = "User", action = "Registration", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Response",
                url: "response",
                defaults: new { controller = "App", action = "ActionResponse", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin Home",
                url: "admin",
                defaults: new { controller = "Admin", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin Settings",
                url: "admin/settings",
                defaults: new { controller = "Admin", action = "Settings", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin Make Manager",
                url: "admin/assignManager/{id}",
                defaults: new { controller = "Admin", action = "MakeManager"}
            );

            routes.MapRoute(
                name: "Admin Update Username",
                url: "admin/update/username",
                defaults: new { controller = "Admin", action = "updateUsername" }
            );

            routes.MapRoute(
                name: "Admin Update Password",
                url: "admin/update/pwd",
                defaults: new { controller = "Admin", action = "updatePassword" }
            );

            routes.MapRoute(
                name: "Admin Logout",
                url: "admin/logout",
                defaults: new { controller = "Admin", action = "Logout" }
            );

            routes.MapRoute(
                name: "Member Home",
                url: "member",
                defaults: new { controller = "Member", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member Add Guest Meal",
                url: "member/add/meal/guest",
                defaults: new { controller = "Member", action = "AddGuestMeal", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member Modify Guest Meal",
                url: "member/modify/meal/guest/{id}",
                defaults: new { controller = "Member", action = "ModifyGuestMeal", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member Modify Meal Schedule",
                url: "member/modify/meal/schedule",
                defaults: new { controller = "Member", action = "updateMealSchedule", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member Delete Guest Meal",
                url: "member/remove/meal/guest/{id}",
                defaults: new { controller = "Member", action = "DeleteGuestMeal"}
            );

            routes.MapRoute(
                name: "Member Guest Details",
                url: "member/view/guests",
                defaults: new { controller = "Member", action = "GuestViewer", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member Modify Self Meal",
                url: "member/modify/meal/self",
                defaults: new { controller = "Member", action = "ModifySelfMeal", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member Report Viewer",
                url: "member/report/view/{m}/{y}",
                defaults: new { controller = "Member", action = "ReportViewer", m = UrlParameter.Optional, y = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member Settings",
                url: "member/settings",
                defaults: new { controller = "Member", action = "Settings", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member Logout",
                url: "member/logout",
                defaults: new { controller = "Member", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manager Home",
                url: "manager",
                defaults: new { controller = "Manager", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manager Bazar Schedules",
                url: "manager/bazarschedules",
                defaults: new { controller = "Manager", action = "BazarSchedular", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manager Modify Bazar Schedule",
                url: "manager/modify/bazarschedule",
                defaults: new { controller = "Manager", action = "ModifySchedule", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manager Preferences",
                url: "manager/preferences",
                defaults: new { controller = "Manager", action = "Preferences", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manager Modify Preferences",
                url: "manager/modify/preferences",
                defaults: new { controller = "Manager", action = "updatePreferences", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manager Report Viewer",
                url: "manager/report/view/{m}/{y}",
                defaults: new { controller = "Manager", action = "ReportViewer", m = UrlParameter.Optional, y = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manager Cancel B",
                url: "manager/cancel/breakfast/{m}",
                defaults: new { controller = "Manager", action = "cancelBreakfast" }
            );

            routes.MapRoute(
                name: "Manager Cancel L",
                url: "manager/cancel/lunch/{m}",
                defaults: new { controller = "Manager", action = "cancelLunch" }
            );

            routes.MapRoute(
                name: "Manager Cancel D",
                url: "manager/cancel/dinner/{m}",
                defaults: new { controller = "Manager", action = "cancelDinner" }
            );

            routes.MapRoute(
               name: "Manager Cancel All B",
               url: "manager/cancel/all/breakfast",
               defaults: new { controller = "Manager", action = "cancelAllBreakfast" }
           );

            routes.MapRoute(
                name: "Manager Cancel All L",
                url: "manager/cancel/all/lunch",
                defaults: new { controller = "Manager", action = "cancelAllLunch" }
            );

            routes.MapRoute(
                name: "Manager Cancel All D",
                url: "manager/cancel/all/dinner",
                defaults: new { controller = "Manager", action = "cancelAllDinner" }
            );

            routes.MapRoute(
                name: "Manager Logout",
                url: "manager/logout",
                defaults: new { controller = "Manager", action = "Logout", id = UrlParameter.Optional }
            );
        }
    }
}
