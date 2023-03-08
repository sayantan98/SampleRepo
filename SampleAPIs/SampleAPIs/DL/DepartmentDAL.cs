using System;
using System.Configuration;
using Dapper;
using SampleAPIs.Helper;
using SampleAPIs.Models;
using SampleAPIs.Abstractions;
using System.Data;

namespace SampleAPIs.DL
{
    public class DepartmentDAL: IDepartment
    {
        private readonly DapperContext dapperContext;
        public DepartmentDAL(DapperContext dapper)
        {
            dapperContext = dapper;
        }

        public async Task<string> InsertDepartment(DepartmentModel department)
        {
            string result = "";
            try
            {
                using(var connection = dapperContext.connection)
                {
                    var query = new DynamicParameters();
                    query.Add("@DepartmentName", department.DepartmentName);
                    query.Add("@errmsg", "", DbType.String, ParameterDirection.Output);
                    var r_result = dapperContext.connection.Execute("InsertDepartment", query, null, 0, System.Data.CommandType.StoredProcedure);
                    result = query.Get<string>("@errmsg");
                }
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public async Task<string> UpdateDepartment(DepartmentModel department)
        {
            string result = "";
            try
            {
                using (var connection = dapperContext.connection)
                {
                    var query = new DynamicParameters();
                    query.Add("@DepartmentId", department.DepartmentId);
                    query.Add("@DepartmentName", department.DepartmentName);
                    query.Add("@errmsg", "", DbType.String, ParameterDirection.Output);
                    var r_result = dapperContext.connection.Execute("UpdateDepartment", query, null, 0, System.Data.CommandType.StoredProcedure);
                    result = query.Get<string>("@errmsg");
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public async Task<List<DepartmentModel>> GetDepartmentsList()
        {
            List<DepartmentModel> departments = new List<DepartmentModel>();
            try
            {
                using(var connection = dapperContext.connection)
                {
                    var query = new DynamicParameters();
                    query.Add("@errmsg","", DbType.String, ParameterDirection.Output);
                    departments = (List<DepartmentModel>)await dapperContext.connection.QueryAsync<DepartmentModel>("GetDepartmentList", query, null, 0, System.Data.CommandType.StoredProcedure);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return departments;
        }

        public async Task<string> DeleteDepartment(int? DepartmentId)
        {
            string result = "";
            try
            {
                using (var connection = dapperContext.connection)
                {
                    var query = new DynamicParameters();
                    query.Add("@DepartmentId", DepartmentId);
                    query.Add("@errmsg", "", DbType.String, ParameterDirection.Output);
                    var r_result = dapperContext.connection.Execute("DeleteDepartment", query, null, 0, System.Data.CommandType.StoredProcedure);
                    result = query.Get<string>("@errmsg");
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
