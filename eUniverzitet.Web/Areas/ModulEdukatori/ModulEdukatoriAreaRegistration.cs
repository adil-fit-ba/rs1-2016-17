using System.Web.Mvc;

namespace eUniverzitet.Web.Areas.ModulEdukatori
{
    public class ModulEdukatoriAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulEdukatori";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulEdukatori_default",
                "ModulEdukatori/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}