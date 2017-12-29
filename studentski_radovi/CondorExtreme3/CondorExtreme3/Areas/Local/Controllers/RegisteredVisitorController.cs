using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using CondorExtreme3.Areas.Local.Models;
using CondorExtreme3.ModelsUser;
using CondorExtreme3.DAL;
using CondorExtreme3.Tools;
using System.Data.SqlClient;
using System.Configuration;

namespace CondorExtreme3.Areas.Local.Controllers
{
    public class RegisteredVisitorController : Controller
    {

        //public CondorDBUsers principal = new CondorDBUsers();

        //Random rng = new Random();

        //// GET: Local/RegisteredVisitor
        //public ActionResult Index(string seats = "", string ProjectionId = "")
        //{

        //    if (seats != "" && ProjectionId != "")
        //    {
        //        List<string> AllSeats = seats.Trim(',').Split(',').ToList();
        //        //    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[HttpContext.Session["ConnectionString"].ToString()].ConnectionString))
        //        //    {

        //        //        SqlCommand command;
        //        //        connection.Open();
        //        //        string query = "";
        //        //        foreach (string s in AllSeats) { 
        //        //            query = $"UPDATE ProjectionSeats SET IsReserved = 1 WHERE ProjectionID = {int.Parse(ProjectionId)} AND SeatID = {int.Parse(s)}";
        //        //            command = new SqlCommand(query, connection);
        //        //            command.ExecuteNonQuery();
        //        //        }
        //        //         query = $"INSERT INTO Reservations VALUES({Session["UserID"]},{ProjectionId}, '{DateTime.Now}', " +
        //        //            $"'(SELECT DATEADD(MINUTE, -30, DateTimeStart) FROM Projections WHERE ProjectionID = " +
        //        //            $"{ProjectionId}'), 0, 0,'{HttpContext.Session["ConnectionString"]}')";

        //        //        command = new SqlCommand(query, connection);
        //        //        command.ExecuteNonQuery();
        //        //    }

        //        //}
        //        RegisteredVisitor visitor = principal.RegisteredVisitors.Find(int.Parse(Session["UserID"].ToString()));

        //        //Jedinstveno indetificirati Usera da se ne ponavlja pri dodavanju u unutar Cinema baze
        //        using (CondorDBContextChild context = new CondorDBContextChild(HttpContext.Session["ConnectionString"].ToString()))
        //        {

        //            if (!context.Cities.Any(x => x.Name == visitor.City.Name))
        //            {
        //                ModelsLocalDB.Country co = new ModelsLocalDB.Country();

        //                if (!context.Country.Any(x => x.Name == visitor.City.Country.Name))
        //                {
        //                   co = new ModelsLocalDB.Country() {
        //                        Name = visitor.City.Country.Name,
        //                        IsDeleted = false
        //                    };
        //                    context.Country.Add(co);
        //                    context.SaveChanges();
        //                }

        //                ModelsLocalDB.City ci = new ModelsLocalDB.City() {
        //                    CityID = visitor.CityID,
        //                    Name = visitor.City.Name,
        //                    PostalCode = visitor.City.PostalCode,
        //                    IsDeleted = false,
        //                    CountryID = co.CountryID,
        //                    Country = co
        //                };

        //                context.Cities.Add(ci);
        //                context.SaveChanges();

        //            }

        //            ModelsLocalDB.RegisteredVisitor rv = new ModelsLocalDB.RegisteredVisitor();
                    
        //                rv.RegisteredVisitorId = int.Parse(Session["UserID"].ToString());
                        
        //                rv.FirstName = visitor.FirstName;
        //                rv.LastName = visitor.LastName;
        //                rv.CityID = context.Cities.Any(x => x.Name == visitor.City.Name) ?  context.Cities.Where(x=> x.Name == visitor.City.Name).SingleOrDefault().CityID : visitor.CityID;
        //                rv.PhoneNumber = visitor.PhoneNumber;
        //                rv.Email = visitor.Email;

        //                context.RegisteredVisitors.Add(rv);
        //                context.SaveChanges();
                    
        //            foreach (string s in AllSeats)
        //            {
        //                context.ProjectionSeats.Find(int.Parse(ProjectionId), int.Parse(s)).IsReserved = true;
        //            }

        //            ModelsLocalDB.Reservation r = new ModelsLocalDB.Reservation();
        //            r.VisitorID = rv.RegisteredVisitorId;
        //            r.ProjectionID = int.Parse(ProjectionId);
        //            r.ConnectionString = HttpContext.Session["ConnectionString"].ToString();
        //            r.IsDeleted = false;
        //            r.ReservationDate = DateTime.Now;
        //            r.ExpiryDate = context.Projections.Find(r.ProjectionID).DateTimeStart.Subtract(new TimeSpan(0, 30, 0));
        //            r.ReservationCompleted = false;

        //            context.Reservations.Add(r);

        //            context.SaveChanges();

        //        }

        //        if (Session["UserID"] == null)
        //            return RedirectToAction("Login");
        //    }

        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Registration()
        //{           

        //    RegisteredVisitorVM model = new RegisteredVisitorVM();
        //    return View("Registration", model);
        //}

        //[HttpPost]
        //public ActionResult SubmitRegisteredVisitors(RegisteredVisitorVM model)
        //{
        //    if (!ModelState.IsValid)
        //        return View("Registration", model);

        //    RegisteredVisitor rv = new RegisteredVisitor();
        //    string salt = rng.Next().ToString();

        //    rv.FirstName = model.FirstName;
        //    rv.LastName = model.LastName;
        //    rv.UserName = model.UserName;
        //    rv.Password = Algorithm.GetStringSha256Hash(model.Password + salt) + '\0' + salt;
        //    rv.Email = model.Email;
        //    rv.City = principal.Cities.Find(model.CityID);
        //    rv.City.Country = principal.Country.Find(rv.City.Country.CountryID);
        //    rv.CityID = rv.City.CityID;
        //    rv.VirtualPointsTotal = 0;

        //    principal.RegisteredVisitors.Add(rv);
        //    principal.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public ActionResult Login(LoginVM model = null)
        //{
        //    if (Session["UserID"] != null)
        //        return RedirectToAction("Index");

        //    if (model == null)
        //        model = new LoginVM();

        //    ModelState.Clear();
        //    return View("Login", model);
        //}

        //[HttpPost]
        //public ActionResult Logout()
        //{
        //    Session["UserID"] = null;
        //    return RedirectToAction("Login");
        //}

        //[HttpPost]
        //public ActionResult LoginRequest(LoginVM model)
        //{
        //    if (!principal.RegisteredVisitors.Any(x => x.UserName == model.UserName))
        //    {
        //        model.ErrorMessage = "Invalid Username!";
        //        model.Password = "";
        //        return RedirectToAction("Login", model);
        //    }
        //    else
        //    {
        //        string pass = principal.RegisteredVisitors
        //            .Where(x => x.UserName == model.UserName)
        //            .Select(x => x.Password).SingleOrDefault();

        //        string hash = pass.Split('\0')[0];
        //        string salt = pass.Split('\0')[1];

        //        string modelPass = Algorithm.GetStringSha256Hash(model.Password + salt);
        //        if (modelPass == hash)
        //        {
        //            Session["UserID"] = principal.RegisteredVisitors
        //                .Where(x => x.UserName == model.UserName)
        //                .Select(x => x.Id).SingleOrDefault();

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            model.ErrorMessage = "Invalid Password!";
        //            return RedirectToAction("Login", model);
        //        }
        //    }
        //}

    }
}