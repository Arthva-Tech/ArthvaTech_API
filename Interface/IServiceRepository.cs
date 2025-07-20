public interface IServiceRepository
{
    Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
    Task<ServiceDto> GetServiceByIdAsync(Guid id);
    Task<int> InsertServiceAsync(ServiceDto model);
    Task<int> UpdateServiceAsync(ServiceDto model);
    Task<int> DeleteServiceAsync(Guid id);
}
