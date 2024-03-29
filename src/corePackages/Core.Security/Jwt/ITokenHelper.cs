﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Jwt
{
    public interface ITokenHelper
    {
        Task<AccessToken> CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
