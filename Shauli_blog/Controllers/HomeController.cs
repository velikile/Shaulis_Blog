using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Shauli_blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "This site has changed";

            return View();
        }
        public ActionResult EbayDeals(){    
            string deals=new WebClient().DownloadString("http://deals.ebay.com/feeds/xml");
            XDocument xml = XDocument.Parse(deals);
            
            IEnumerable<XNode> items = xml.Root.Nodes().Where(s => s.ToString().Substring(0, 6) == "<Item>"); // x is an array of the items listed
            IEnumerable<XNode> d = xml.Root.Nodes().Where(s => s.ToString().Contains("<MoreDeals>"));
            XDocument sections = XDocument.Parse(d.ElementAt(0).ToString());
            List<List<string>> tags = new List<List<string>>();
            IEnumerable<XNode> temp = sections.Root.Nodes();
            foreach (var a in temp){
                string sec = a.ToString();
                XDocument sect = XDocument.Parse(sec);
                IEnumerable<XNode> MoreDealsItems = sect.Root.Nodes().Where(s => s.ToString().Contains("<Item>"));
                tags.Add(HtmlImageTagGenerator(MoreDealsItems));
            
            
            }

            ViewBag.MoreDeals = temp; ;
            ViewBag.MORE = tags;
            ViewBag.RenderThis = HtmlImageTagGenerator(items);
            return View();

        }

        public List<string> HtmlImageTagGenerator(IEnumerable<XNode> items)
        {
            List<string> tags = new List<string>();
            for (int i = 0; i < items.ToArray().Length; i++)
            {
                XNode y = items.ElementAt(i);//y is the item itself
                XDocument z = XDocument.Parse(y.ToString());// z is the conversion from y back to xml doc
                IEnumerable<XNode> nodes = z.Root.Nodes().Where(item => item.ToString().Contains("<PictureURL>") || item.ToString().Contains("<DealURL>"));
                XDocument imgsrc = XDocument.Parse(nodes.ElementAt(0).ToString());
                string imgtag = imgsrc.Root.Value;
                XDocument deal = XDocument.Parse(nodes.ElementAt(1).ToString());
                string dealsrc = deal.Root.Value;
                XDocument z1 = XDocument.Parse(y.ToString());


                tags.Add("<a href=\"" + dealsrc + "\" >" + "<img src=" + "\"" + imgtag + "\"" + "style=\"width:100px;height:100px\" " + "  /  ><a/>");

            }
            return tags;

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Content()
        {
            return View();
        
        }

        
    }


}
