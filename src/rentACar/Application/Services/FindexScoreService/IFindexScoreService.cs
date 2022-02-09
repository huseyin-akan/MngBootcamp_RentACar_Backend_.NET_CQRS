using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FindexScoreService
{
    public interface IFindexScoreService
    {
        Task<int> getICFindexScore(string nationalId);
        Task<int> getCCFindexScore(string taxNumber);
    }
}
