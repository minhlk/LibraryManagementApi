using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(int roleId);
        Task CreateRoleAsync(Role role);
        Task UpdateRoleAsync(int roleId, Role role);
        Task DeleteRoleAsync(int roleId);
    }
}
