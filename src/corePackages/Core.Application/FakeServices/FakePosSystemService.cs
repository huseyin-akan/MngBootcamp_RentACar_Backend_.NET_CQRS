using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.FakeServices
{
    public class FakePosSystemService
    {
        public bool GetPayment(string cardNo, string name, string cVV, string date)
        {
            if(cardNo.Length == 16 && cVV.Length == 3)
            {
                return true;
            }
            return false;
        }
    }
}
