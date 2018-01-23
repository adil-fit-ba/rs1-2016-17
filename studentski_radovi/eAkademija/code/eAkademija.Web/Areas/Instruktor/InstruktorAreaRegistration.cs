using System.Web.Mvc;

namespace eAkademija.Web.Areas.Instruktor
{
    public class InstruktorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Instruktor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Instruktor_default",
                url: "Instruktor/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "eAkademija.Web.Areas.Instruktor.Controllers" }
            );
        }
    }
}