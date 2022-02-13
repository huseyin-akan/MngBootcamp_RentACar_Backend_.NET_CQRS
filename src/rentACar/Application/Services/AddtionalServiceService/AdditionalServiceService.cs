using Application.Services.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AddtionalServiceService
{
    public class AdditionalServiceService : IAdditionalServiceService
    {
        private readonly IAdditionalServiceRepository additionalServiceRepo;

        public AdditionalServiceService(IAdditionalServiceRepository additionalServiceRepo)
        {
            this.additionalServiceRepo = additionalServiceRepo;
        }

        public async Task<List<AdditionalService>> GetAdditionalServicesByIdList(List<int> additionalServiceIds)
        {
            var additionalServices= new List<AdditionalService>();
            foreach (var id in additionalServiceIds)
            {
                additionalServices.Add(await additionalServiceRepo.GetAsync(ads => ads.Id == id) );
            }
            return additionalServices;
        }
    }
}
