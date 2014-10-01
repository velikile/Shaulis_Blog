using Shauli_blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shauli_blog.Controllers
{
    public class FansController : Controller
    {
        UsersContext db = new UsersContext();
        public ActionResult Index()
        {
            
            var Fans = db.Fans.ToArray();
            return View(Fans);
        }

        public ActionResult Details(long? id)
        {
            
            var fan = db.Fans.Find(id);

            if (fan.ImagePath!=null && fan.ImagePath.Contains("Content"))
                ViewBag.ImagePath = fan.ImagePath.Substring(fan.ImagePath.IndexOf("Content"));
            else
                ViewBag.ImagePath = "Image not available";
            return View(fan);

        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Fan fan, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null && file.ContentLength > 0)
                {
                    var FileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), FileName);
                    file.SaveAs(path);
                    fan.ImagePath = path.ToString();

                }
                else
                {
                    fan.ImagePath = ""; 

                }

                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }

            ViewBag.FanDate = fan.BirthDate;
            string date;
            if (fan.BirthDate != null) {
                var year=fan.BirthDate.Year.ToString();
                var month=fan.BirthDate.Month.ToString();
                
                var  days=fan.BirthDate.Day.ToString();
                if (fan.BirthDate.Day < 10)
                {
                    days = "0" + days;  
                }
                if (fan.BirthDate.Month < 10)
                {
                    month = "0" + month;
                }
                date = year + "-" + month + "-" + days;
                ViewBag.FanDate = date;
            }
            ViewBag.ImagePath = fan.ImagePath;
            return View(fan);
        }



        [HttpPost]
        public ActionResult Edit(Fan fan)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(long id)
        {
            
            var fan = db.Fans.Find(id);


            if (fan != null)
            {
                if (fan.ImagePath != null && fan.ImagePath!="")
                {
                    var file = new FileInfo(fan.ImagePath);
                    if (file != null)
                        file.Delete();
                }
                db.Fans.Remove(fan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var FileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), FileName);
                file.SaveAs(path);

            }
            return RedirectToAction("Index");
        }

    }
}
