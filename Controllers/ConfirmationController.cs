using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Donations_Software.Models;

namespace Donations_Software.Controllers
{
    public class ConfirmationController : Controller
    {
        private teamdonationsEntities db = new teamdonationsEntities();
        // GET: Confirmation
        public ActionResult Index()
        {
            var personalId = TempData["personalInfoID"];
            var donationUserInfoes = db.DonationUserInfoes.Include(d => d.DonationDetail).Include(d => d.PersonalInfo);
            var infolist = donationUserInfoes.ToList().Where(p => p.personalInfoID.ToString() == personalId.ToString());
            var total = infolist.Sum(x => x.Amount);
            ViewData["total"] = total;
            return View(infolist);

        }

        public ActionResult DonationsUser()
        {
            //var userId = Session["UserID"];
            var personalUserInfo = db.PersonalInfoes.ToList().Where(t => t.UserID.ToString() == Session["UserID"].ToString());
            var donationUserInfoes = (db.DonationUserInfoes.Include(d => d.DonationDetail).Include(d => d.PersonalInfo));
            //    var infolist = donationUserInfoes.ToList().Where(p => p.personalInfoID.Contains(personalUserInfo));
            // var total = infolist.Sum(x => x.Amount);
            //ViewData["total"] = total;
            return View();
        }

        public ActionResult ModalPopUp()
        {
            var personalId = TempData["personalInfoID"];
            var donationUserInfoes = db.DonationUserInfoes.Include(d => d.DonationDetail).Include(d => d.PersonalInfo);
            var infolist = donationUserInfoes.ToList().Where(p => p.personalInfoID.ToString() == personalId.ToString());
            var total = infolist.Sum(x => x.Amount);
            ViewData["total"] = total;
            return View(infolist);
        }

    }

}