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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee employee;
        public EmployeeController(IEmployee employee)
        {
            this.employee = employee;
        }

        [HttpPost("InsertEmployee")]
        public async Task<IActionResult> InsertEmployee(EmployeeModel employeeModel)
        {
            try
            {
                return Ok(await employee.InsertEmployee(employeeModel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                return Ok(await employee.UpdateEmployee(employeeModel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetEmployeesList")]
        public async Task<IActionResult> GetEmployeesList()
        {
            try
            {
                return Ok(await employee.GetAllEmployees());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("DeleteEmployee/{EmployeeId}")]
        public async Task<IActionResult> DeleteEmployee(int? EmployeeId)
        {
            try
            {
                return Ok(await employee.DeleteEmployee(EmployeeId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file == null || file.Length == 0)
                    return BadRequest("No file selected");

                return Ok(await employee.UploadFiles(file));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
