using SampleAPIs.Models;
namespace SampleAPIs.Abstractions
{
    public interface IEmployee
    {
        Task<string> InsertEmployee(EmployeeModel employeeModel);
        Task<string> UpdateEmployee(EmployeeModel employeeModel);
        Task<List<EmployeeModel>> GetAllEmployees();
        Task<string> DeleteEmployee(int? EmployeeId);
        Task<string> UploadFiles(IFormFile? file);
    }
}
