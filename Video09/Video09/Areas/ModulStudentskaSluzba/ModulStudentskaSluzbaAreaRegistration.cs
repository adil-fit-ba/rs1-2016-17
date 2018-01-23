using System.Web.Mvc;

namespace MVC.Areas.ModulStudentskaSluzba
{
    public class ModulStudentskaSluzbaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulStudentskaSluzba";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(name: "ModulStudentskaSluzba_default", 
                url: "ModulStudentskaSluzba/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}