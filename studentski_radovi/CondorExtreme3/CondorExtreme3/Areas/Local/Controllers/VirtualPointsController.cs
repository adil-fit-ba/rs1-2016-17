using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CondorExtreme3.DAL;
using CondorExtreme3.ModelsLocalDB;

namespace CondorExtreme3.Areas.Local.Controllers
{
    public class VirtualPointsController : Controller
    {
        private CondorDBContextChild _myVar;

        public CondorDBContextChild principal
        {
            get
            {
                if (HttpContext.Session["ConnectionString"] == "")
                { return _myVar; }
                if (_myVar == null)
                    _myVar = new CondorDBContextChild(HttpContext.Session["ConnectionString"].ToString());
                return _myVar;
            }
            set { _myVar = value; }
        }

        // GET: Local/VirtualPoints
        [HttpPost]
        public PartialViewResult GetVirtualPointPacks()
        {
            List<VirtualPointsPack> model = principal.VirtualPointsPacks.ToList();
            return PartialView("GetVirtualPointPacks", model);
        }

        [HttpPost]
        public PartialViewResult SetVirtualPointPacks()
        {
            return PartialView("SetVirtualPointPacks");
        }

        [HttpPost]
        public ActionResult SubmitVirtualPointPacks(int amount)
        {
            if (principal == null)
                return Redirect(Url.Content("~/"));

            principal.VirtualPointsPacks.Add(new VirtualPointsPack { Amount = amount });
                principal.SaveChanges();
               
            return RedirectToAction("Index", "RegisteredVisitor");
        }

        [HttpPost]
        public ActionResult BuyVirtualPointPacks(int id)
        {
            if (principal == null)
                return Redirect(Url.Content("~/"));

            int amountPurchased = principal.VirtualPointsPacks.Find(id).Amount;
                RegisteredVisitor rv = principal.RegisteredVisitors.Find(Session["UserID"]);

                rv.VirtualPointsTotal += amountPurchased;
                principal.SaveChanges();
            

            return RedirectToAction("Index", "RegisteredVisitor");
        }
    }
}