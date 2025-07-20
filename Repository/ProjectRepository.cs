using Dapper;
using System.Data;
using ArthvaTech.API.Models;

namespace ArthvaTech.API.Repository
{
    public class ProjectRepository
    {
        private readonly DapperContext _context;

        public ProjectRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var query = "Sp_GetAllProjects";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<ProjectDto>(query, commandType: CommandType.StoredProcedure);
        }

        public async Task<ProjectDto> GetByIdAsync(Guid id)
        {
            var query = "Sp_GetProjectById";
            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<ProjectDto>(query, new { ProjectID = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateAsync(ProjectDto dto)
        {
            var query = "Sp_CreateProject";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteAsync(query, new
            {
                dto.Title,
                dto.Description,
                dto.Image,
                Tech = string.Join(",", dto.Tech),
                dto.ServiceID
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(ProjectDto dto)
        {
            var query = "Sp_UpdateProject";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteAsync(query, new
            {
                dto.ProjectID,
                dto.Title,
                dto.Description,
                dto.Image,
                Tech = string.Join(",", dto.Tech),
                dto.ServiceID
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var query = "Sp_DeleteProject";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteAsync(query, new { ProjectID = id }, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ServiceDropdownDto>> GetDropdownAsync()
        {
            var query = "Sp_GetServiceDropdown";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<ServiceDropdownDto>(query, commandType: CommandType.StoredProcedure);
        }

}

}