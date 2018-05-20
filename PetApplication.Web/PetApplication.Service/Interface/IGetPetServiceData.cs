using PetApplication.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetApplication.Service
{
    public interface IGetPetServiceData
    {
        IEnumerable<PetOwnerModel> GetPetDataFromService();
    }
}
