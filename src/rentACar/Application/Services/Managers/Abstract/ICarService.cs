using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Managers.Abstract
{
    public interface ICarService
    {
        Task<int> GetFindexScoreById(int id);
    }
}
