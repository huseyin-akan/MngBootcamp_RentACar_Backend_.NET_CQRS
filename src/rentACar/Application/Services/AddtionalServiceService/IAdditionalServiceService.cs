using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AddtionalServiceService
{
    public interface IAdditionalServiceService
    {
        Task<List<AdditionalService>> GetAdditionalServicesByIdList(List<int> additionalServiceIds);
    }
}
