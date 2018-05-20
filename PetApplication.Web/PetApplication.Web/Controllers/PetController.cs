using PetApplication.Repository;
using System.Web.Mvc;

namespace PetApplication.Web.Controllers
{

    //Unity IoC - controller injection
    //Moq in Unit Test
    //Elmah in Logging framework
    //Global Exception filter for additional work in Exception
    //Output cache can be enable if whole page cache is needed
    //Im Memory cache is used to avoid HTTP call overhead for 60 sec
    //Audit log using filter


    public class PetController : BaseController
    {
        public PetController(IPetRepository PetRespository) : 
         base(PetRespository)
        {

    }

    //Output cache can be enable if whole page cache is needed
    //[OutputCache(Duration = 60)]
    public ActionResult Index()
        {
            var result = this._PetRepository.GetPetNamesAccordingToGender();
            return View(result);
        }
    }
}