using SampleAPIs.Models;
namespace SampleAPIs.Abstractions
{
    public interface IDepartment
    {
        Task<string> InsertDepartment(DepartmentModel department);
        Task<string> UpdateDepartment(DepartmentModel department);
        Task<List<DepartmentModel>> GetDepartmentsList();
        Task<string> DeleteDepartment(int? DepartmentId);

    }
}
