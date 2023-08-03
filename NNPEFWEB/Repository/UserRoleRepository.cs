

using Microsoft.EntityFrameworkCore.Internal;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using System.Linq;

namespace NNPEFWEB.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        private readonly ApplicationDbContext context;

        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public bool CheckIfUserRoleExist(int roleId, int userId)
        {
            return context.UserRoles.Any(x => x.RoleId == roleId && x.UserId == userId);
        }
    }
}
