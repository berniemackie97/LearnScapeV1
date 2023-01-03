using Core.BusinessModels;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UsersRepo : IUserRepo
    {
        private readonly StoreContext _context;

        public UsersRepo(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<UserBM>> GetUserList()
        {
            return await _context.Users
                .Include(p => p.Address)
                .ToListAsync();
        }

        public async Task<UserBM> GetUserById(int id)
        {
            return await _context.Users
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
