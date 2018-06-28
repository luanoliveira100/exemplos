using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CondominioEstribo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Extensão Mensagem para ActionResult
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Mensagens";
            DefaultModelBinder.ResourceClassKey = "Mensagens";
        }
    }
}
