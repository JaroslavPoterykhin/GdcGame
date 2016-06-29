using System.Web.Mvc;
using EntityFX.EconomicsArcade.Utils.ClientProxy.Manager;

namespace EntityFX.EconomicsArcade.Presentation.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var simpleUserManagerClient = new SimpleUserManagerClient("net.tcp://localhost:8555/EntityFX.EconomicsArcade.Manager/EntityFX.EconomicsArcade.Contract.Manager.UserManager.ISimpleUserManager");
            if (!simpleUserManagerClient.Exists(User.Identity.Name))
            {
                simpleUserManagerClient.Create(User.Identity.Name);
            }

            var sessionManagerClient = new SessionManagerClient("net.tcp://localhost:8555/EntityFX.EconomicsArcade.Manager/EntityFX.EconomicsArcade.Contract.Manager.SessionManager.ISessionManager");
            var sessionGuid = sessionManagerClient.AddSession(User.Identity.Name);
            System.Web.HttpContext.Current.Session[User.Identity.Name] = sessionGuid;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}