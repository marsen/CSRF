using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vulnerable.Models.Edmx;

namespace vulnerable.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]        
        public JsonResult CreditCard()
        {
            var CreditCard = new { number = "4938170100001213", GOODTHRU = "1219 ", SecurityCode = "713", Action="Get" };

            return Json(CreditCard, JsonRequestBehavior.AllowGet);
        }
        [Authorize] 
        public JsonResult ExchangeMoney(string fromWho, string toWho, decimal money) {
            try
            {
                using (var csrf = new CSRFDBEntities())
                {
                    //csrf.Database.Connection.Open();
                    var donor = (from a in csrf.Money
                                 where a.Name == fromWho
                                 select a).FirstOrDefault();
                    var donatarius = (from a in csrf.Money
                                      where a.Name == toWho
                                      select a).FirstOrDefault();
                    if (donor != null && donatarius != null && donor.Balance > money)
                    {
                        donor.Balance = donor.Balance - money;
                        donatarius.Balance = donatarius.Balance + money;
                    }
                    csrf.SaveChanges();
                    return Json(new { Msg = string.Format("${0} 交易成功", money) });
                }
            }
            catch(Exception ex) 
            {
                return Json(new { errMsg = ex.Message });
            }
        }
    }
}