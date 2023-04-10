import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient) { }
  APIUrl: any = "http://localhost:5041/";
  GetDepartmentList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'EmployeeApi/Department/GetDepartmentsList');
  }
  InsertDepartment(data: any[]):Observable<any>{
    return this.http.post(this.APIUrl+'EmployeeApi/Department/InsertDepartment', data);
  }
  UpdateDepartment(data: any[]):Observable<any>{
    return this.http.put(this.APIUrl+'EmployeeApi/Department/UpdateDepartment', data);
  }
  DeleteDepartment(data: any):Observable<any>{
    return this.http.delete(this.APIUrl+'EmployeeApi/Department/DeleteDepartment/'+data);
  }
  GetEmployeeList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'EmployeeApi/Employee/GetEmployeesList');
  }
  DeleteEmployee(data: any):Observable<any>{
    return this.http.delete(this.APIUrl+'EmployeeApi/Employee/DeleteEmployee/'+data);
  }
  InsertEmployee(data: any[]):Observable<any>{
    return this.http.post(this.APIUrl+'EmployeeApi/Employee/InsertEmployee', data);
  }
  UpdateEmployee(data: any[]):Observable<any>{
    return this.http.put(this.APIUrl+'EmployeeApi/Employee/UpdateEmployee', data);
  }
  UploadFiles(formData: any):Observable<any>{
    return this.http.post(this.APIUrl+'EmployeeApi/Employee/UploadFiles', formData);
  }
}

