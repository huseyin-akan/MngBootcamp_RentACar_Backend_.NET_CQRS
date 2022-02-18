using Core.Application.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FindexScoreService
{
    public class FakeFindexScoreServiceAdapter : IFindexScoreService
    {
        FakeFindexScoreService findexScoreService = new FakeFindexScoreService();

        public Task<int> getCCFindexScore(string taxNumber)
        {
            Task<int> result = Task.Run(() =>
            {
                long taxNo = Convert.ToInt64(taxNumber);
                return this.findexScoreService.getFindexScoreOfCorporates(taxNo);
            });
            return result;
        }

        public Task<int> getICFindexScore(string nationalId)
        {
            Task<int> result = Task.Run(() =>
            {
                long tcNO = Convert.ToInt64(nationalId);
                return this.findexScoreService.getFindexScoreOfIndividuals(tcNO);
            });
            return result;
        }
    }
}
