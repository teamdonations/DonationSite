using Donations_Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Donations_Software.Controllers
{
    public class ConfirmationController : Controller
    {
        // GET: Confirmation
        public ActionResult Index(PersonalInfo TempData)
        {   
            return View();
        }
    }
}