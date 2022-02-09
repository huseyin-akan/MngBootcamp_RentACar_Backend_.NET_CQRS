using Application.Features.Users.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Models
{
    public class UserListModel : BasePagebleModel
    {
        public IList<UserListDto> Items { get; set; }
    }
}
