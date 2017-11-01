using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ActionResult ViewResponse(HttpStatusCode status, string value = "")
        {
            Response.StatusCode = (int)status;
            Response.TrySkipIisCustomErrors = true;
            return Content(value);
        }

        protected ActionResult ViewResponse(HttpStatusCode status, IEnumerable<string> value, string separador = "<br/>")
        {
            Response.StatusCode = (int)status;
            Response.TrySkipIisCustomErrors = true;
            return Content(string.Join(separador, value));
        }

        protected ActionResult JsonViewResponse(HttpStatusCode status, object value)
        {
            Response.StatusCode = (int)status;
            Response.TrySkipIisCustomErrors = true;
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ErrorMessage(string message)
        {
            return ViewResponse(HttpStatusCode.BadRequest, message);
        }

        public ActionResult ErrorMessage(IEnumerable<string> value)
        {
            return ViewResponse(HttpStatusCode.BadRequest, value);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result =  ErrorMessage($"Erro durante a operação: {filterContext.Exception.Message}");
        }
    }
}