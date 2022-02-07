using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.FakeServices
{
    public class FakeFindexScoreService
    {
        public int getFindexScoreOfIndividuals(long nationalId)
        {
            Random random = new Random();
            return random.Next(0, 1901);
        }

        public int getFindexScoreOfCorporates(long taxNumber)
        {
            Random random = new Random();
            return random.Next(0, 1901);
        }
    }
}
