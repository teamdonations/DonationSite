using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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


        public ActionResult SendEmail(User user)
        {
            var email = TempData["Email"];
            var body = "<p>Email From: {0} ({1})</p><p>Message:Thank you for donating the amount. We Appriate it.</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(email.ToString()));  // replace with valid value 
            message.From = new MailAddress("teamdonation3@gmail.com");  // replace with valid value
            message.Subject = "Donation Confirmation";
            message.Body = string.Format(body, "Admin", "teamdonation3@gmail.com", message);
            message.IsBodyHtml = true;


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //  LocalClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            //  smtp.Credentials = new System.Net.NetworkCredential("Admin@outlook.com", "1234"); // Enter seders User name and password   
            var credential = new NetworkCredential
            {
                UserName = "teamdonation3@gmail.com",  // replace with valid value
                Password = "Teamdonation@3"  // replace with valid value
            };
            smtp.Credentials = credential;

            smtp.EnableSsl = true;
            smtp.Send(message);
            return RedirectToAction("Index", "DonationDetails");
       
            //  return View();
        }


    }

}