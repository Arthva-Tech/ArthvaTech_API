using Microsoft.AspNetCore.Mvc;
using ArthvaTech.API.Models;
using ArthvaTech.API.Repository;
using Microsoft.AspNetCore.Authorization;

namespace ArthvaTech.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _repo;

        public ServicesController(IServiceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllServicesAsync());

        [HttpGet("EditService")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _repo.GetServiceByIdAsync(id);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpPost("CreateService")]
        public async Task<IActionResult> Create([FromBody] ServiceDto dto)
        {
            var result = await _repo.InsertServiceAsync(dto);
            return result > 0 ? Ok("Service created") : BadRequest("Failed to create");
        }

        [HttpPut("UpdateService")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceDto dto)
        {
            dto.ServiceID = id;
            var result = await _repo.UpdateServiceAsync(dto);
            return result > 0 ? Ok("Service updated") : NotFound();
        }

        [HttpDelete("DeleteService")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repo.DeleteServiceAsync(id);
            return result > 0 ? Ok("Service deleted") : NotFound();
        }
    }

}