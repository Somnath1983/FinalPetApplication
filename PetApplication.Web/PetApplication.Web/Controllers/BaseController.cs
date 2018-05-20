using PetApplication.Repository;
using PetApplication.Utility;
using System.Web.Mvc;

namespace PetApplication.Web.Controllers
{
    [Audit]
    public class BaseController : Controller
    {
        protected readonly IPetRepository _PetRepository;
        public BaseController(IPetRepository PetRepository)
        {
            this._PetRepository = PetRepository;
        }

        public ViewResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}