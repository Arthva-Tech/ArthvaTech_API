using Dapper;
using System.Data;
using ArthvaTech.API.Models;
namespace ArthvaTech.API.Repository
{
    public class UserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var query = "Sp_GetAllUsersWithRoles";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<UserViewModel>(query, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var query = "GETALLROLES";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<RoleDto>(query, commandType: CommandType.StoredProcedure);
        }


        public async Task<int> AddUserAsync(UserViewModel user)
        {
            var query = "Sp_InsertUserWithRole";
            using var conn = _context.CreateConnection();
            return await conn.QuerySingleAsync<int>(query, new
            {
                user.UserName,
                user.Email,
                user.PasswordHash,
                user.RoleID
            }, commandType: CommandType.StoredProcedure);
        }


        public async Task<int> UpdateUserAsync(UserViewModel user)
        {
            var query = "Sp_UpdateUserWithRole";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteAsync(query, new
            {
                user.UserID,
                user.UserName,
                user.Email,
                user.RoleID
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteUserAsync(Guid userId)
        {
            var query = "Sp_DeleteUser";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteAsync(query, new { UserID = userId }, commandType: CommandType.StoredProcedure);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var query = "Sp_GetUserByEmail";
            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(query, new { Email = email }, commandType: CommandType.StoredProcedure);
        }

    }
}