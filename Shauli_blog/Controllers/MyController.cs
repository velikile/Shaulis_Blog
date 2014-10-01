using Shauli_blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Shauli_blog.Controllers
{
    public class MyController : Controller
    {
        UsersContext db = new UsersContext();

        // GET: /My/
        [HttpGet]
        public ActionResult Index()
        {
            
            ViewBag.UserName = User.Identity.Name;
            var CArticle =db.Articles.ToList().OrderByDescending(x=>x.pubtime).First();
            ViewBag.ArticleId = CArticle.id;
            var comments = (from c in db.Comments where(c.ArticleId==CArticle.id) select c).ToArray();
            ViewBag.Comments = comments;

            return View();
        }

       
        [Authorize]
        [HttpPost]
        public ActionResult Index(Models.Comment comment)
        {
            
            if (ModelState.IsValid)
            {
            
                db.Comments.Add(comment);
                var dbusers = new UsersContext();
                var CurrentUser = dbusers.UserProfiles.Find(WebSecurity.GetUserId(User.Identity.Name));
                if (CurrentUser.Points == null)
                    CurrentUser.Points = 1;
                else
                    CurrentUser.Points++;
                dbusers.Entry(CurrentUser).State = EntityState.Modified;
                dbusers.SaveChanges();
                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult CreateComment() {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult CreateComment(Models.Comment comment)
        {
            
            if (ModelState.IsValid)
            {
            
                db.Comments.Add(comment);

                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return CreateComment();

        }

        


        
        public ActionResult SearchComment(string name,string description,string website,string email,DateTime ?Sdate,DateTime ?Fdate, short ?point){

            var comments =
                (from c in db.Comments where c.name == name select c);

            if (description != ""&&comments.ToArray().Length!=0)
                comments = (from c in db.Comments where c.description.Contains(description) select c).Intersect(comments);
            else if(description != "")
                comments=(from c in db.Comments where c.description.Contains(description) select c);
            if (email != "" && comments.ToArray().Length != 0)
            comments=(from c in db.Comments where c.email == email select c).Intersect(comments);
            else if(email!="")
                comments = (from c in db.Comments where c.email == email select c);
            if (website != "" && comments.ToArray().Length != 0)
                comments = (from c in db.Comments where c.website == website select c).Intersect(comments);
            else if (website != "")
                comments = (from c in db.Comments where c.website == website select c);
            if (Sdate != null && Fdate != null && comments.ToArray().Length != 0)
                comments = (from c in db.Comments where c.pubTime >= Sdate && c.pubTime <= Fdate select c).Intersect(comments);
            else if (Sdate != null && Fdate != null)
                comments = (from c in db.Comments where c.pubTime >= Sdate && c.pubTime <= Fdate select c);

             var finalcomments=comments.ToArray();
            
            return View(finalcomments);
            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewAll() {

            return View(db.Comments.ToArray());
        
        
        }

        [Authorize(Roles="Admin")]
        public ActionResult ViewComment(long id) {
            
            var comment = db.Comments.Find(id);
            return View(comment);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(long id)
        {
            
            var comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {   
            
 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Comment comment)
        {  
            
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Viewall");
            }
            return View(comment);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowUsers() {
            var db = new UsersContext();
            var Users = db.UserProfiles.ToArray();

            return View(Users);
        }
        [Authorize(Roles="Admin")]
        public ActionResult DeleteUser(long id) {
            var db = new UsersContext();
            var User = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(User);
            db.SaveChanges();
            return RedirectToAction("ShowUsers");
        }

        


    }
}
