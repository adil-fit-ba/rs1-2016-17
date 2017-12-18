using System.Web.Mvc;

namespace eUniverzitet.Web.Areas.ModulZaposlenik
{
    public class ModulZaposlenikAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulZaposlenik";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulZaposlenik_default",
                "ModulZaposlenik/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}