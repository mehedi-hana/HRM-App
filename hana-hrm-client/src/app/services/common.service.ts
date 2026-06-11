import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root',
})
export class CommonService extends BaseService {

  getDepartments(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/departments');
  }

  getDesignations(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/designations');
  }

  getGenders(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/genders');
  }

  getJobTypes(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/jobtypes');
  }

  getEmployeeTypes(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/employeetypes');
  }

  getMaritalStatuses(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/maritalstatuses');
  }

  getReligions(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/religions');
  }

  getSections(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/sections');
  }

  getWeekOffs(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/weekoffs');
  }

  getRelationships(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/relationships');
  }

  getEducationLevels(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/educationlevels');
  }

  getEducationExaminations(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/educationexaminations');
  }

  getEducationResults(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/educationresults');
  }

  reportingManagers(): Observable<ApiResponse> {
    return this.get<ApiResponse>('api/common/reportingmanagers');
  }
}
