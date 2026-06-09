import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})

export class BaseService {

  constructor(protected readonly http: HttpClient) {}

  protected get<T>(path: string): Observable<T> {
    return this.http.get<T>(`${environment.apiBaseUrl}${path}`);
  }

  protected post<T>(path: string, body: any): Observable<T> {
    return this.http.post<T>(`${environment.apiBaseUrl}${path}`, body);
  }

  protected put<T>(path: string, body: any): Observable<T> {
    return this.http.put<T>(`${environment.apiBaseUrl}${path}`, body);
  }

  protected delete<T>(path: string): Observable<T> {
    return this.http.delete<T>(`${environment.apiBaseUrl}${path}`);
  }
}

