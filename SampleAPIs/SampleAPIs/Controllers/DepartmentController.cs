using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SampleAPIs.Abstractions;
using SampleAPIs.Models;

namespace SampleAPIs.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("/EmployeeApi/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment department;
        public DepartmentController(IDepartment _department)
        {
            department = _department;
        }
        [HttpPost("InsertDepartment")]
        public async Task<IActionResult> InsertDepartment(DepartmentModel departmentModel)
        {
            try
            {
                return Ok(await department.InsertDepartment(departmentModel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentModel departmentModel)
        {
            try
            {
                return Ok(await department.UpdateDepartment(departmentModel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpGet("GetDepartmentsList")]
        public async Task<IActionResult> GetDepartmentsList()
        {
            try
            {
                return Ok(await department.GetDepartmentsList());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("DeleteDepartment/{DepartmentId}")]
        public async Task<IActionResult> DeleteDepartment(int? DepartmentId)
        {
            try
            {
                return Ok(await department.DeleteDepartment(DepartmentId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
