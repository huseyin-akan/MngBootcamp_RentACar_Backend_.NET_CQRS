using Application.Features.Users.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetUserClaims
{
    public class GetUserClaimsQuery :IRequest<UserClaimsListDto>
    {

    }
}
