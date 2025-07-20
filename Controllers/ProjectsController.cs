using Microsoft.AspNetCore.Mvc;
using ArthvaTech.API.Models;
using ArthvaTech.API.Repository;
using Microsoft.AspNetCore.Authorization;

namespace ArthvaTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectRepository _repo;

        public ProjectsController(ProjectRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetById(Guid id) =>
            await _repo.GetByIdAsync(id) is ProjectDto dto ? Ok(dto) : NotFound();

        [HttpPost("Create")]
public async Task<IActionResult> Create([FromBody] ProjectDto dto)
{
    if (!ModelState.IsValid)
    {
        // Log error for debugging
        return BadRequest(ModelState);
    }

    var result = await _repo.CreateAsync(dto);
    return result > 0 ? Ok("Project created") : BadRequest("Failed to create project");
}


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProjectDto dto) =>
            await _repo.UpdateAsync(dto) > 0 ? Ok("Updated") : BadRequest("Failed to update");

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id) =>
            await _repo.DeleteAsync(id) > 0 ? Ok("Deleted") : BadRequest("Failed to delete");
        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllServices() => Ok(await _repo.GetDropdownAsync());
}

}