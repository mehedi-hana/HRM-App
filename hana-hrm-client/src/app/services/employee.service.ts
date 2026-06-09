import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { EmployeeDetailDto } from '../models/employee-detail.model';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService extends BaseService {

  getAll(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/employee');
  }

  getById(id: number): Observable<ApiResponse> {
    return this.get<ApiResponse>(`api/employee/details/${id}`);
  }

  create(employee: EmployeeDetailDto): Observable<ApiResponse> {
    return this.post<ApiResponse>('api/employee', employee);
  }

  update(id: number, employee: EmployeeDetailDto): Observable<ApiResponse> {
    return this.put<ApiResponse>(`api/employee/${id}`, employee);
  }

  deleteEmployee(id: number): Observable<ApiResponse> {
    return this.delete<ApiResponse>(`api/employee/${id}`);
  }
}
