using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Donations_Software.Models;
using System.IO;

namespace Donations_Software.Controllers
{
    public class DonationDetailsController : Controller
    {
        private teamdonationsEntities db = new teamdonationsEntities();

        // GET: DonationDetails
        public ActionResult Index()
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(db.DonationDetails.ToList());
        }

        // GET: DonationDetails/Details/5
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
            DonationDetail donationDetail = db.DonationDetails.Find(id);
            if (donationDetail == null)
            {
                return HttpNotFound();
            }
            return View(donationDetail);
        }

        // GET: DonationDetails/Create
        public ActionResult Create()
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        // POST: DonationDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonationID,DonationName,Description,ImageURL,Price,isMonthly")] DonationDetail donationDetail, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                donationDetail.ImageURL = "~/Images/" + file.FileName;

                db.DonationDetails.Add(donationDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donationDetail);
        }

        // GET: DonationDetails/Edit/5
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
            DonationDetail donationDetail = db.DonationDetails.Find(id);
            if (donationDetail == null)
            {
                return HttpNotFound();
            }
            return View(donationDetail);
        }

        // POST: DonationDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationID,DonationName,Description,ImageURL,Price,isMonthly")] DonationDetail donationDetail, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                donationDetail.ImageURL = "~/Images/" + file.FileName;

                db.Entry(donationDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donationDetail);
        }

        // GET: DonationDetails/Delete/5
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
            DonationDetail donationDetail = db.DonationDetails.Find(id);
            if (donationDetail == null)
            {
                return HttpNotFound();
            }
            return View(donationDetail);
        }

        // POST: DonationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonationDetail donationDetail = db.DonationDetails.Find(id);
            db.DonationDetails.Remove(donationDetail);
            db.SaveChanges();
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
