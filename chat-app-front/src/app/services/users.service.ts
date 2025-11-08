import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { UserDto } from '../models/Dtos/UserDto';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private apiUrl = `${environment.apiUrl}/users`;

  constructor(private http: HttpClient) {}

  searchUsers(username: string, excludeIds: string[]): Observable<UserDto[]> {
    let params = new HttpParams();

    params = params.set('username', username ?? '');

    excludeIds.forEach((id) => {
      params = params.append('excludeIds', id);
    });

    return this.http.get<UserDto[]>(`${this.apiUrl}/search`, { params });
  }
}
