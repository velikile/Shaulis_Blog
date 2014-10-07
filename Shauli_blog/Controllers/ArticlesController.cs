using Shauli_blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shauli_blog.Controllers
{
    public class ArticlesController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: /Articles/
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private List<string>ExtractParaGraphs(string text)
        {
            var paragraphs = new List<string>();
            string temptext = new string(text.ToCharArray());
            int counter=0;
            var StartEndKeypair=new Dictionary<int,int>();
            while (temptext.Length != 0)
            {   
                var a = temptext.IndexOf('\n');
                if (a != 0 && a!=-1) 
                paragraphs.Add(temptext.Substring(0, a + 1));
                temptext = temptext.Substring(a+1);
                if (!temptext.Contains('\n') && temptext.Length != 0)
                {
                    paragraphs.Add(temptext);
                    break;
                }
                counter = a+counter  ;
            }
            
            return paragraphs;
        }


        public ActionResult Index() {

            var articles = db.Articles.ToArray();
            return  View(articles);
        
        }

        [Authorize(Roles="Admin")]
        public ActionResult Details(long id=1)
        {
            /*Article article= db.Articles.Find(id);
            ViewBag.paragraphs = ExtractParaGraphs(article.content);
            var b = from c in db.Comments where id==c.ArticleId select c;
            ViewBag.comments = b.ToArray();
            ViewBag.UserName = User.Identity.Name;
            ViewBag.ArticleId = id;
           
            ViewBag.articles = db.Articles.ToArray();*/
            return PartialView();
        }

        public ActionResult PartialDetail(long id=-1)
        {
            Article article = db.Articles.Find(1);
            if (id == -1)
            {
                var articles = from c in db.Articles orderby (c.pubtime) descending select c;
                article = articles.First();
                ViewBag.paragraphs = ExtractParaGraphs(article.content);
                var b = from c in db.Comments where article.id == c.ArticleId select c;
                ViewBag.comments = b.ToArray();
                ViewBag.UserName = User.Identity.Name;
                ViewBag.ArticleId = article.id;
                ViewBag.articles = db.Articles.ToArray();
            }
            else
            {
                var articles = from c in db.Articles where(id==c.id)select c;
                article = articles.First();
                ViewBag.paragraphs = ExtractParaGraphs(article.content);
                var b = from c in db.Comments where article.id == c.ArticleId select c;
                ViewBag.comments = b.ToArray();
                ViewBag.UserName = User.Identity.Name;
                ViewBag.ArticleId = article.id;
                ViewBag.articles = db.Articles.ToArray();

            
            }

            return View(article);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult Details(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }

        [Authorize(Roles="Admin")]
        public ActionResult Edit(long id=1) {

            Article article = db.Articles.Find(id);
            return View(article);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult Edit(Article article) 
        {
            
                if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        
        }


        [Authorize(Roles="Admin")]
        public ActionResult Delete(long id=-1) {

            if (id != -1) {
                Article article = db.Articles.Find(id);
                if (article != null)
                {
                      db.Articles.Remove(article);
                      var comments2del = from c in db.Comments where(c.ArticleId == id) select c;
                        if(comments2del.ToArray().Length!=0)
                            foreach (var c in comments2del) {
                                db.Comments.Remove(c);
                                
                      }
                        db.SaveChanges();

                }
            }

            return RedirectToAction("Index");
        }

    }
}
