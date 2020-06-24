using Donations_Software.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Donations_Software.Controllers
{
    public class HomeController : Controller
    {
        private teamdonationsEntities db = new teamdonationsEntities();

        public ActionResult Index()
        {
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Login ()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")] User uSER)
        {

            if (!String.IsNullOrEmpty(uSER.Email) && !String.IsNullOrEmpty(uSER.Password)) 
            {
                var existsUser = db.Users.Where(c => c.Email.Contains(uSER.Email) && c.Password.Contains(uSER.Password)).ToList();

                if (existsUser.Count() > 0)
                {
                    Session["isAdmin"] = existsUser[0].isAdmin;
                    Session["UserID"] = existsUser[0].UserID;
                    Session["FullName"] = existsUser[0].FirstName + " " + existsUser[0].LastName;

                    if (existsUser[0].isAdmin == true)
                    {
                        return RedirectToAction("Index", "DonationDetails");
                    }
                    else
                    {
                        return RedirectToAction("Index", "DonationDetails");
                    }
                }
                else 
                {
                    ModelState.AddModelError("", "Wrong username and password");
                }
            }

            return View();
        }

        public User IsValidUser(User user)
        {
            using (var dataContext = new teamdonationsEntities())
            {
                //Retireving the user details from DB based on username and password enetered by user.
                User user1 = dataContext.Users.Where(query => query.Email.Equals(user.Email) && query.Password.Equals(user.Password)).SingleOrDefault();
                //If user is present, then true is returned.
                if (user1 == null)
                    return null;
                //If user is not present false is returned.
                else
                    return user1;
            }
        }
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}