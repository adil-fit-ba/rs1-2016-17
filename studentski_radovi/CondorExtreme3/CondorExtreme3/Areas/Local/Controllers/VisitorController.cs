using System;
using System.Web.Mvc;
using CondorExtreme3.Areas.Local.Models;
using CondorExtreme3.ModelsLocalDB;
using CondorExtreme3.DAL;
using CondorExtreme3.Tools;
using System.Linq;
using System.Collections.Generic;

namespace CondorExtreme3.Areas.Local.Controllers
{
    public class VisitorController : Controller
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

        Random rng = new Random();

        [HttpPost]
        public ActionResult Registration(string[] Seats, VisitorVM model = null)
        {

            ModelState.Clear();
            List<string> GetSeats = Seats.ToList();
            string s = "";
            foreach (string seat in GetSeats)
                s += seat + ",";

            model.ProjectionId = GetSeats[0];

            GetSeats.RemoveAt(0);

            if (Session["UserID"] != null)
                return RedirectToAction("Index", "RegisteredVisitor", new { seats = s, ProjectionId = model.ProjectionId });

            if (principal == null)
                return Redirect(Url.Content("~/"));

            if (model == null)
                model = new VisitorVM();

            model.Seats = GetSeats;


            return View("Registration", model);
        }

        [HttpPost]
        public ActionResult SubmitVisitors(VisitorVM model, string Seats)
        {
            List<string> AllSeats = Seats.Trim(',').Split(',').ToList();

            if (principal == null)
                return Redirect(Url.Content("~/"));

            if (!ModelState.IsValid)
                return View("Registration", model);

            model.ActivationCode = rng.Next(1000, 10000).ToString();
            model.Seats = AllSeats;

            Session["ActivationCode"] = model.ActivationCode;
            Session["PhoneNumber"] = model.PhoneNumber;

           TwilioHelper.SendSMS("+" + model.PhoneNumber, model.ActivationCode);
            return View("RegistrationActivation", model);
            

        }

        [HttpPost]
        public ActionResult ConfirmVisitors(VisitorVM model, string Seats)
        {
            if (principal == null)
                return Redirect(Url.Content("~/"));
            Visitor v;
            model.PhoneNumber = Session["PhoneNumber"].ToString();
            List<string> AllSeats = Seats.Trim(',').Split(',').ToList();
            if (model.ActivationConfirmationCode == Session["ActivationCode"].ToString())
            {
                if (!principal.Visitors.Any(x => x.PhoneNumber == model.PhoneNumber))
                {
                    v = new Visitor();

                    v.ActivationCode = Session["ActivationCode"].ToString();
                    v.PhoneNumber = Session["PhoneNumber"].ToString();

                    principal.Visitors.Add(v);
                    principal.SaveChanges();
                }
                else
                {
                   v = principal.Visitors.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefault();

                    v.ActivationCode = Session["ActivationCode"].ToString();
                    v.PhoneNumber = Session["PhoneNumber"].ToString();

                    principal.SaveChanges();
                }
                int i = 0;bool temp = false;
                foreach (ProjectionSeat s in principal.ProjectionSeats.Where(x=> x.ProjectionID.ToString() == model.ProjectionId).ToList())
                {
                    if (s.SeatID.ToString() == AllSeats[i] && s.IsReserved)
                    {
                        temp = true;
                    }
                }

                if (!temp)
                {
                    foreach (ProjectionSeat s in principal.ProjectionSeats.Where(x => x.ProjectionID.ToString() == model.ProjectionId).ToList())
                    {
                        if (i < AllSeats.Count)
                            foreach (string seat in AllSeats)
                                if (s.SeatID.ToString() == seat)
                                    s.IsReserved = true;
                            
                    }

                    Reservation r = new Reservation();
                    r.VisitorID = v.VisitorID;
                    r.Visitor = v;
                    r.ProjectionID = int.Parse(model.ProjectionId);
                    r.Projection = principal.Projections.Where(x => x.ProjectionID == r.ProjectionID).FirstOrDefault();
                    r.ReservationCompleted = true;
                    r.ReservationDate = DateTime.Now;
                    r.ExpiryDate = r.Projection.DateTimeStart.Subtract(new TimeSpan(0, 30, 0));
                    r.IsDeleted = false;

                    principal.Reservations.Add(r);
                    principal.SaveChanges();
                }
                else
                {
                    return Content("Welcome to destination fucked!");
                }
                

                Session["ActivationCode"] = null;
                Session["PhoneNumber"] = null;

                //ovdje se postavlja uspješna rezervacija 
            }
            else
            {
                Session["ActivationCode"] = null;
                Session["PhoneNumber"] = null;

                model.ErrorMessage = "Invalid Activation Code!";
                return RedirectToAction("Registration", model);
            }
            return RedirectToAction("Cinema", "../Local", new { Reservation  = "Your reservation has been made!"});
        }
    }
}
