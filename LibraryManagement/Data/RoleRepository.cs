using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(LibraryManagementContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            var roles = await FindAllAsync();
            return roles;
        }
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            var role = await FindByConditionAsync(x => x.Id == roleId);
            return role.FirstOrDefault();
        }
        public async Task CreateRoleAsync(Role role)
        {
            Create(role);
            await SaveAsync();
        }
        public async Task UpdateRoleAsync(int aithorId, Role newRole)
        {
            var role = await GetRoleByIdAsync(aithorId);
            role.RoleName = newRole.RoleName;
            Update(role);
            await SaveAsync();
        }
        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await GetRoleByIdAsync(roleId);
            Delete(role);
            await SaveAsync();
        }
    }
}
