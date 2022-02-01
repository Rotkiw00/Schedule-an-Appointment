using System.Web;
using System.Web.Mvc;

namespace Projekt_Przychodnia_TsWM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
