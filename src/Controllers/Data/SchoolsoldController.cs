using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
//using System.Web.Mvc;
using Company.WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using BES.Models;
using BES.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BES.Controllers.Data
{
    public class SchoolsoldController : Controller
    {
        private readonly ApplicationDbContext db;
       // SessionGet.loginUser loginUser = new SessionGet.loginUser();
        //public bool checkAuthentication()
        //{
        //    if (!Request.IsAuthenticated)
        //    { return false; }
        //    try
        //    { loginUser = SessionGet.sessionIntial(User.Identity.Name); }
        //    catch
        //    {
        //        string user = User.Identity.Name;
        //        if (loginUser != null)
        //        { }
        //        else
        //            return false;
        //    }
        //    return true;
        //}
        // GET: /Schools/
        public ActionResult Index()
        {
            //if (!checkAuthentication())
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            var schools = db.Schools.Where(a=>a.SelectedStatus!=null).Include(s => s.UC).OrderBy(s =>s.UC.Tehsil.District.Region.RegionID);
            
            return View(schools.ToList());
        }

        // GET: /Schools/Details/5
        public ActionResult Details(int? id)
        {
            if (!checkAuthentication())
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // GET: /Schools/Create
        public ActionResult Create()
        {
            //if (!checkAuthentication())
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            ViewBag.UCID = new SelectList(db.UCs, "UCID", "UCName");
            return View();
        }

        // POST: /Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("SchoolID,UCID,SCode,SName,BEMIS,Level,Latitude,Longitude,Level,Status,Abandon,Upgradation,NotificationDate,Zone")] School school)
        {
            if (ModelState.IsValid)
            {
               
                school.SelectedStatus = true;
               
                db.Schools.Add(school);
                db.SaveChanges();

               // EduKeyIndicator newEduKedyIndicator = new EduKeyIndicator();
               // newEduKedyIndicator.EDUID = (short)school.SchoolID;
                //newEduKedyIndicator.SchoolID = school.SchoolID;
                //db.EduKeyIndicators.Add(newEduKedyIndicator);
               // db.SaveChanges();

               // DevKeyIndicator newDevKedyIndicator = new DevKeyIndicator();
              //  newDevKedyIndicator.DevID = (short)school.SchoolID;
               // newDevKedyIndicator.SchoolID = school.SchoolID;
              //  db.DevKeyIndicators.Add(newDevKedyIndicator);
              //  db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UCID = new SelectList(db.UCs, "UCID", "UCName", school.UCID);
            return View(school);
        }

        public ActionResult selectSchool()
        {
           // ViewBag.url = Session["url"].ToString();

            return View();
        }

        public ActionResult selectView(int? UCID)
        {
            // ViewBag.url = Session["url"].ToString();
            var Schools = db.Schools.Include(a => a.UC).Where(a => a.SchoolID > 0) ;
            if (UCID != null)
            {
                Schools = db.Schools.Include(a => a.UC).Where(a => a.UCID == UCID);
            }
            return PartialView(Schools.ToList());
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        //Filter View

        public ActionResult FilterView(short? RID, short? DID, short? TID, short? UID, short? LID)
        {

            if (!checkAuthentication())
            {
                return RedirectToAction("Login", "Account");
            }

            //without filter
            var schools = db.Schools.Include(s => s.UC).OrderBy(s => s.UC.Tehsil.District.Region.RegionID)
                .Where(a => a.SelectedStatus != null && a.SchoolOf==1);

            if(LID>0)
            {
                schools = schools.Where(q => q.Level == LID);
            }

            //if (loginUser.RegionID < 8)
            //{
            //    RID = Convert.ToInt16(loginUser.RegionID);
            //    System.Web.HttpContext.Current.Session["RID"] = RID;
            //}

            if (UID > 0)
                schools = schools.Where(q => q.UCID == UID);
            else if (TID > 0)
                schools = schools.Where(q => q.UC.TehsilID == TID);
            else if (DID > 0)
                schools = schools.Where(q => q.UC.Tehsil.DistrictID == DID);
            else if (RID > 0)
                schools = schools.Where(q => q.UC.Tehsil.District.RegionID == RID);

            return PartialView(schools.ToList());
        }
        // Region View

        public static Models.ViewModel.RegionViewModel cvm = new Models.ViewModel.RegionViewModel();
        public ActionResult RegionView()
        {
            List<Models.Region> Regions = db.Regions.ToList();
            cvm.RegionList.Clear();
            foreach (Models.Region cd in Regions)
            {
                cvm.RegionList.Add(cd);
            }
            cvm.RegionList.RemoveAt(7);
            return View(cvm);
        }

        // District View
        public static Models.ViewModel.DistrictViewModel dvm = new Models.ViewModel.DistrictViewModel();
        public ActionResult DistrictView(int? RegionID)
        {
            //List<Models.District> Districts = db.Districts.ToList();
            dvm.DistrictList.Clear();
            if (RegionID != null)
            {
                List<Models.District> Districts = db.Districts.Where(t => t.RegionID == RegionID).ToList();
                //Models.District cd = Districts.Find(p => p.DistrictID == DistrictID);

                foreach (Models.District spd in Districts)
                {
                    dvm.DistrictList.Add(spd);
                }
            }
            return View(dvm);
        }

        //Tehsil view
        public static Models.ViewModel.TehsilViewModel spvm = new Models.ViewModel.TehsilViewModel();
        public ActionResult TehsilView(int? DistrictID)
        {
            var Districts = db.Districts.ToList();

            spvm.TehsilList.Clear();
            if (DistrictID != null)
            {
                List<Models.Data.Tehsil> Tehsils = db.Tehsils.Where(t => t.DistrictID == DistrictID).ToList();
                //Models.District cd = Districts.Find(p => p.DistrictID == DistrictID);

                foreach (Models.Data.Tehsil spd in Tehsils)
                {
                    spvm.TehsilList.Add(spd);
                }
            }
            return View(spvm);
        }

        public static Models.ViewModel.UCViewModel cityvm = new Models.ViewModel.UCViewModel();
        public ActionResult UCView(int? DistrictID, int? TehsilID)
        {
            var Districts = db.Districts.ToList();
            cityvm.CityList.Clear();
            if (DistrictID != null && TehsilID != null)
            {
                //SelectedTehsilID = TehsilID;

                List<Models.Data.UC> UCs = db.UCs.Where(t => t.TehsilID == TehsilID).ToList();
                Models.District cd = Districts.Find(p => p.DistrictID == DistrictID);
                //   Models.Tehsil spd = cd.TehsilList.Find(p => p.TehsilID == TehsilID);

                foreach (Models.Data.UC cpd in UCs)
                {
                    cityvm.CityList.Add(cpd);
                }
            }
            return View(cityvm);
        }
        public ActionResult LevelView()
        {
            return View();
        }

        // GET: /Schools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return NotFound();
            }
            ViewBag.UCID = new SelectList(db.UCs, "UCID", "UCName", school.UCID);
            return View(school);
        }

        // POST: /Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("SchoolID,UCID,SCode,SName,BEMIS,Level,Latitude,Longitude,Level,Status,Abandon,Upgradation,Zone,SelectedStatus,NotificationDate,Remarks,Password")] School school)
        {
            school.UC = db.UCs.Find(school.UCID);
            if (ModelState.IsValid)
            {
                if (school.SelectedStatus == false && school.Remarks == null)
                {
                    ModelState.AddModelError("", "Dropped: Remarks should be Entered");
                    return View(school);
                }
                if(school.Level>1)
                {school.SelectedStatus=true;}
                school.SchoolOf = 1;
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UCID = new SelectList(db.UCs, "UCID", "UCName", school.UCID);
           
            return View(school);
        }

        // GET: /Schools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }



        // POST: /Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = db.Schools.Find(id);
            db.Schools.Remove(school);
            //db.SaveChanges();
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
