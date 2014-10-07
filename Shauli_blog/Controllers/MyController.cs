using Shauli_blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using System.Linq;
using System.Net;
using WebMatrix.WebData;

namespace Shauli_blog.Controllers
{
    
    public class MyController : Controller
    {
        UsersContext db = new UsersContext();
        

       
        // GET: /My/
        [HttpGet]
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {

            string[] roles = Roles.GetRolesForUser();
           //  var a=Roles.GetAllRoles();
            if (User.IsInRole("Admin"))
                ViewBag.HelloAdmin = "Hello "+User.Identity.Name;

            ViewBag.UserName = User.Identity.Name;
            
            return View();
        }


        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult CreateComment() {//create a comment page 
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult CreateComment(Models.Comment comment)//post a comment to the db
        {
            
            if (ModelState.IsValid)
            {
            
                db.Comments.Add(comment);

                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return CreateComment();
 }



        public ActionResult SearchCommentAdmin(long ?id,long? AId,string name, string description, string website, string email, DateTime? Sdate, DateTime? Fdate, short? point)
        {
            //search by 

            var comments =
            (from c in db.Comments where c.ArticleId == AId select c);
          
            if (id != null && comments.ToArray().Length != 0)
                comments = (from c in db.Comments where c.id==id select c).Intersect(comments);
            else if (id != null)
                comments = (from c in db.Comments where c.id==id select c);

           if (name != "" && comments.ToArray().Length != 0)
                comments = (from c in db.Comments where c.description.Contains(name) select c).Intersect(comments);
            else if (name != "")
                comments = (from c in db.Comments where c.name.Contains(name) select c);


            if (description != "" && comments.ToArray().Length != 0)
                comments = (from c in db.Comments where c.description.Contains(description) select c).Intersect(comments);
            else if (description != "")
                comments = (from c in db.Comments where c.description.Contains(description) select c);
            if (email != "" && comments.ToArray().Length != 0)
                comments = (from c in db.Comments where c.email == email select c).Intersect(comments);
            else if (email != "")
                comments = (from c in db.Comments where c.email == email select c);
            if (website != "" && comments.ToArray().Length != 0)
                comments = (from c in db.Comments where c.website == website select c).Intersect(comments);
            else if (website != "")
                comments = (from c in db.Comments where c.website == website select c);
            if (Sdate != null && Fdate != null && comments.ToArray().Length != 0)
                comments = (from c in db.Comments where c.pubTime >= Sdate && c.pubTime <= Fdate select c).Intersect(comments);
            else if (Sdate != null && Fdate != null)
                comments = (from c in db.Comments where c.pubTime >= Sdate && c.pubTime <= Fdate select c);

            var finalcomments = comments.ToArray();

            return View(finalcomments);

        }
        

        public ActionResult SearchComment(string name,string description,string website,string email,DateTime ?Sdate,DateTime ?Fdate, short ?point){
            //search by 

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
        public ActionResult ViewAll() {//list all of the comments

            return View(db.Comments.ToArray());
        
        
        }

        [Authorize(Roles="Admin")]
        public ActionResult ViewComment(long id) {// view the comment by id
            
            var comment = db.Comments.Find(id);
            return View(comment);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(long id)//delete a Comment by id 
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
        public ActionResult PartialList(string role)
        {
            var query = db.UserProfiles;
            if (role == null ||role=="All")
            {


                return PartialView(query);
            }
            else
            {
                var Requested = Roles.GetUsersInRole(role);


                var query2 = query.Where(s => Requested.Contains(s.UserName));
                return PartialView(query2);
            }



        }






        [Authorize(Roles = "Admin")]
        public ActionResult EditUserRoles(long id=-1)
        {
            if (id != -1)
            {
                var User = db.UserProfiles.Find(id);
                ViewBag.Role = Roles.GetRolesForUser(User.UserName).ToArray()[0];
                return View(User);
            }
            else return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditUserRoles(UserProfile User,string newRole)
        {
            if (ModelState.IsValid)
            {
                string usercurrentrole = Roles.GetRolesForUser(User.UserName).ElementAt(0);
                Roles.RemoveUserFromRole(User.UserName,usercurrentrole);
                if (newRole=="Admin") {
                    Roles.AddUserToRole(User.UserName, "Admin");
                }
                else if (newRole=="Contractor") {
                    Roles.AddUserToRole(User.UserName, "Contractor");
                }
                else if (newRole=="User") {
                    Roles.AddUserToRole(User.UserName, "User");
                
                }
                return RedirectToAction("ShowUsers");
            }
            else return RedirectToAction("Index");
        }

        [Authorize(Roles="Admin")]
        public ActionResult DeleteUser(long id) {
            
            var User = db.UserProfiles.Find(id);
            if (User != null)
            {
                var UserRole=Roles.GetRolesForUser(User.UserName).ElementAt(0);
                Roles.RemoveUserFromRole(User.UserName, UserRole);
            
                db.UserProfiles.Remove(User);
                db.SaveChanges();
                
            }
            return RedirectToAction("ShowUsers");
        }

        public ActionResult UserDetails(long id) {

            var User = db.UserProfiles.Find(id);
            if (User != null){
                ViewBag.Role = Roles.GetRolesForUser(User.UserName).ElementAt(0);
                return View(User);  
            
            
            }
            else
                return RedirectToAction("Index");
        }

        


    }
}
