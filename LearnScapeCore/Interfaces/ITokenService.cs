using LearnScapeCore.BusinessModels.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnScapeCore.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
