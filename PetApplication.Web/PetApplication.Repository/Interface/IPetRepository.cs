using PetApplication.Entity;
using System.Collections.Generic;

namespace PetApplication.Repository
{
    public interface IPetRepository
    {
       IEnumerable<PetResultViewModel> GetPetNamesAccordingToGender();
    }
}
