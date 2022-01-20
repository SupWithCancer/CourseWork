using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IRoleService : IDisposable
    {
        Task<RoleDTO> GetRoleAsync(int id);

        Task<List<RoleDTO>> GetRolesAsync();

        Task<bool> AnyRolesAsync(Expression<Func<Role, bool>> expression);
    }
}
