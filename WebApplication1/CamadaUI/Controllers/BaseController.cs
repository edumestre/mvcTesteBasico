using System.Web.Mvc;

namespace CamadaUI.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Tratamento de erro básico
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public ActionResult TratarErro(System.Exception ex)
        {
            //log erro, trata erro
            ViewBag.ErroPagina = ex.Message;
            return View();
        }

        public ActionResult TratarErro(System.Exception ex, RedirectToRouteResult redirectToRouteResult)
        {
            ViewBag.ErroPagina = ex.Message;
            Session["ErroPagina"] = ex.Message;
            return redirectToRouteResult;
        }

        public JsonResult TratarErroJson(System.Exception ex)
        {
            //log erro, trata erro
            return new JsonResult
            {
                Data = new { erro = ex.Message },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}