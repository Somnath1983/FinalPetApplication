using System.Collections.Generic;
using System.Linq;
using PetApplication.Entity;
using PetApplication.Utility;
using PetApplication.Service;
using System;

namespace PetApplication.Repository
{
    public class PetRepository : IPetRepository
    {
        readonly IGetPetServiceData _PetDataService;

        public PetRepository(IGetPetServiceData PetDataService)
        {
            this._PetDataService = PetDataService;
        }

        public IEnumerable<PetResultViewModel> GetPetNamesAccordingToGender()
        {
            try
            {
                var petOwnerResult = _PetDataService.GetPetDataFromService();
                if (petOwnerResult.Count() > 0)
                {
                    var result = petOwnerResult
                     .GroupBy(o => o.Gender)
                     .Select(r => new PetResultViewModel
                     {
                         Gender = r.Key,
                         PetNames = r
                         .SelectManyIgnoringNull(co => co.Pets)
                         .Where(c => c.Type== Constant.CatKey)
                         .Select(c=>c.Name)
                         .Distinct()
                         .OrderBy(pet => pet).ToList()
                     }).ToList< PetResultViewModel>();
                    return result;
                }
                return null;
            }
            catch(Exception ex)
            {
                Log.LogError(ex);
                throw;
            }
        }

       


    }
}
