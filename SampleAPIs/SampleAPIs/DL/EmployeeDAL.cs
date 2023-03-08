using System;
using System.Configuration;
using Dapper;
using SampleAPIs.Helper;
using SampleAPIs.Models;
using SampleAPIs.Abstractions;
using System.Data;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;

namespace SampleAPIs.DL
{
    public class EmployeeDAL: IEmployee
    {
        public readonly DapperContext dapperContext;
        private readonly Microsoft.Extensions.Hosting.IHostingEnvironment _hostingEnvironment;
        public EmployeeDAL(DapperContext dapperContext, Microsoft.Extensions.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.dapperContext = dapperContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> InsertEmployee(EmployeeModel employeeModel)
        {
            string res = "";
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeName", employeeModel.EmployeeName);
                dynamicParameters.Add("@Department", employeeModel.DepartmentId);
                dynamicParameters.Add("@DOJ", employeeModel.DOJ);
                dynamicParameters.Add("@ProfilePicture", employeeModel.ProfilePicture);
                dynamicParameters.Add("@errmsg", ParameterDirection.Output);
                var r_res = dapperContext.connection.Execute("InsertEmployee", dynamicParameters, null,0,CommandType.StoredProcedure);
                //res = dynamicParameters.Get<int>("@errmsg");
            }
            catch(Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }

        public async Task<string> UpdateEmployee(EmployeeModel employeeModel)
        {
            string res = "";
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeId", employeeModel.EmployeeId);
                dynamicParameters.Add("@EmployeeName", employeeModel.EmployeeName);
                dynamicParameters.Add("@Department", employeeModel.DepartmentId);
                dynamicParameters.Add("@DOJ", employeeModel.DOJ);
                dynamicParameters.Add("@ProfilePicture", employeeModel.ProfilePicture);
                dynamicParameters.Add("@errmsg", ParameterDirection.Output);
                var r_res = dapperContext.connection.Execute("UpdateEmployee", dynamicParameters, null, 0, CommandType.StoredProcedure);
                //res = dynamicParameters.Get<string>("@errmsg");
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }

        public async Task<List<EmployeeModel>> GetAllEmployees()
       {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                using(var connection = dapperContext.connection)
                {
                    var query = new DynamicParameters();
                    query.Add("@errmsg", ParameterDirection.Output);
                    employees = (List<EmployeeModel>)await dapperContext.connection.QueryAsync<EmployeeModel>("GetEmployeeList", query, null, 0, CommandType.StoredProcedure);
                    foreach(var item in employees)
                    {
                        if (item.ProfilePicture != "saved")
                        {
                            var fileExt = item.ProfilePicture.Split(".").Last();
                            byte[] bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.ContentRootPath, "ProfilePicture\\") + item.EmployeeName+"-"+ Convert.ToDateTime(item.DOJ.ToString().Trim()).ToString("yyyy-MM-dd") + "." + fileExt);
                            item.ProfilePicture = Convert.ToBase64String(bytes);
                        }
                    }
                }
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return employees;
        }

        public async Task<string> DeleteEmployee(int? EmployeeId)
        {
            string result = "";
            try
            {
                var query = new DynamicParameters();
                query.Add("@EmployeeId", EmployeeId);
                query.Add("@errmsg", ParameterDirection.Output);
                var r_res = dapperContext.connection.Execute("DeleteEmployee",query, null, 0, CommandType.StoredProcedure);

            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public async Task<string> UploadFiles(IFormFile? file)
        {
            string result = "";
            var fileExt = file.ContentType == "image/jpeg" ? ".jpeg" : ".jpg";
            try
            {
                bool exists = System.IO.Directory.Exists(Path.Combine(_hostingEnvironment.ContentRootPath, "ProfilePicture"));
                byte[] fileBytes;
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(Path.Combine(_hostingEnvironment.ContentRootPath, "ProfilePicture"));
                }

                if (File.Exists(Path.Combine(_hostingEnvironment.ContentRootPath, "ProfilePicture\\") + file.FileName + fileExt))
                {
                    File.Delete(Path.Combine(_hostingEnvironment.ContentRootPath, "ProfilePicture\\") + file.FileName + fileExt);
                }
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                        using (FileStream fs = File.Create(Path.Combine(_hostingEnvironment.ContentRootPath, "ProfilePicture\\", file.FileName + fileExt)))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        // act on the Base64 data
                    }
                }


            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

    }
}
