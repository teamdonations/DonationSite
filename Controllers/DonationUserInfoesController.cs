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
    public class DonationUserInfoesController : Controller
    {
        private teamdonationsEntities db = new teamdonationsEntities();

        // GET: DonationUserInfoes
        public ActionResult Index()
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (Session["isAdmin"].ToString() != "True")
            {
                return RedirectToAction("Login", "Home");
            }

            var donationUserInfoes = db.DonationUserInfoes.Include(d => d.DonationDetail).Include(d => d.PersonalInfo);
            return View(donationUserInfoes.ToList());
        }

        // GET: DonationUserInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (Session["isAdmin"].ToString() != "True")
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationUserInfo donationUserInfo = db.DonationUserInfoes.Find(id);
            if (donationUserInfo == null)
            {
                return HttpNotFound();
            }
            return View(donationUserInfo);
        }

        // GET: DonationUserInfoes/Create
        public ActionResult Create()
        {
            var personalInfo = TempData["PersonalInfo"];
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (Session["isAdmin"].ToString() != "True")
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.DonationID = new SelectList(db.DonationDetails, "DonationID", "DonationName");
            ViewBag.personalInfoID = new SelectList(db.PersonalInfoes, "personalInfoID", "FirstName");
            return View();
        }

        // POST: DonationUserInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DUID,DonationID,personalInfoID,Date,Amount")] DonationUserInfo donationUserInfo)
        {
            if (ModelState.IsValid)
            {
                db.DonationUserInfoes.Add(donationUserInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DonationID = new SelectList(db.DonationDetails, "DonationID", "DonationName", donationUserInfo.DonationID);
            ViewBag.personalInfoID = new SelectList(db.PersonalInfoes, "personalInfoID", "FirstName", donationUserInfo.personalInfoID);
            return View(donationUserInfo);
        }

        // GET: DonationUserInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (Session["isAdmin"].ToString() != "True")
            {
                return RedirectToAction("Login", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationUserInfo donationUserInfo = db.DonationUserInfoes.Find(id);
            if (donationUserInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonationID = new SelectList(db.DonationDetails, "DonationID", "DonationName", donationUserInfo.DonationID);
            ViewBag.personalInfoID = new SelectList(db.PersonalInfoes, "personalInfoID", "FirstName", donationUserInfo.personalInfoID);
            return View(donationUserInfo);
        }

        // POST: DonationUserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DUID,DonationID,personalInfoID,Date,Amount")] DonationUserInfo donationUserInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donationUserInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonationID = new SelectList(db.DonationDetails, "DonationID", "DonationName", donationUserInfo.DonationID);
            ViewBag.personalInfoID = new SelectList(db.PersonalInfoes, "personalInfoID", "FirstName", donationUserInfo.personalInfoID);
            return View(donationUserInfo);
        }

        // GET: DonationUserInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (Session["isAdmin"].ToString() != "True")
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationUserInfo donationUserInfo = db.DonationUserInfoes.Find(id);
            if (donationUserInfo == null)
            {
                return HttpNotFound();
            }
            return View(donationUserInfo);
        }

        // POST: DonationUserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonationUserInfo donationUserInfo = db.DonationUserInfoes.Find(id);
            db.DonationUserInfoes.Remove(donationUserInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Gifts()
        {
           // var personalInfo = TempData["PersonalInfo"];
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
          
          //  ViewBag.Donations = db.DonationDetails.ToList();
            ViewData["Donations"] = db.DonationDetails.ToList();
         //   return View(db.DonationDetails.ToList());
            return View();
        }

       


        public ActionResult GiftSave(DonationUserInfo donationUserInfo)
        {
            var personalInfoes = TempData["PersonalInfo"];

            PersonalInfo personalInfo = (Models.PersonalInfo)TempData["PersonalInfo"];
            db.PersonalInfoes.Add(personalInfo);
            db.SaveChanges();

           // if (ModelState.IsValid)
            {
                for(int i=0; i<donationUserInfo.DonationIDList.Count; i++)
                {
                    if (donationUserInfo.AmountList[i] != 0)
                    {
                        donationUserInfo.personalInfoID = personalInfo.personalInfoID;
                        donationUserInfo.Amount = donationUserInfo.AmountList[i];
                        donationUserInfo.DonationID = donationUserInfo.DonationIDList[i];
                        donationUserInfo.Date = DateTime.Now;
                        // donationUserInfo.DonationID = Convert.ToInt32(Session["DonationID"]);
                        db.DonationUserInfoes.Add(donationUserInfo);
                        db.SaveChanges();
                    }

                }
               

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
