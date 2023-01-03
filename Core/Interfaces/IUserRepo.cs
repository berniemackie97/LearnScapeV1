using Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepo
    {
        Task<UserBM> GetUserById(int id);
        Task<IReadOnlyList<UserBM>> GetUserList();
    }
}
