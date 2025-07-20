using Dapper;
using System.Data;
public class ServiceRepository : IServiceRepository
{
    private readonly DapperContext _context;

    public ServiceRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
    {
        var query = "Sp_GetAllServices";
        using var conn = _context.CreateConnection();
        var data = await conn.QueryAsync<ServiceDto>(query, commandType: CommandType.StoredProcedure);
        
        return data;
    }

    public async Task<ServiceDto> GetServiceByIdAsync(Guid id)
    {
        var query = "Sp_GetServiceById";
        using var conn = _context.CreateConnection();
        var result = await conn.QuerySingleOrDefaultAsync<ServiceDto>(query, new { ServiceID = id }, commandType: CommandType.StoredProcedure);
        return result;
    }

    public async Task<int> InsertServiceAsync(ServiceDto model)
    {
        var query = "Sp_InsertService";
        var parameters = new
        {
            model.Icon,
            model.Title,
            model.Description,
            Features = string.Join(",", model.Features),
            model.Color
        };

        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> UpdateServiceAsync(ServiceDto model)
    {
        var query = "Sp_UpdateService";
        var parameters = new
        {
            model.ServiceID,
            model.Icon,
            model.Title,
            model.Description,
            Features = string.Join(",", model.Features),
            model.Color
        };

        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> DeleteServiceAsync(Guid id)
    {
        var query = "Sp_DeleteService";
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(query, new { ServiceID = id }, commandType: CommandType.StoredProcedure);
    }

}
