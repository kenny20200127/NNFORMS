

using NNPEFWEB.Models;

namespace NNPEFWEB.Repository
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        bool CheckIfUserRoleExist(int roleId, int userId);
    }
}
