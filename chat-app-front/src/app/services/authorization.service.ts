import { Injectable } from '@angular/core';
import { UserInfoModel } from '../models/user-info-model';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { SignInDto } from '../models/sign-in-model';
import { SignUpDto } from '../models/sign-up-model';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {
  user!: UserInfoModel;

  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  signIn(data: SignInDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/signIn`, data);
  }

  tryGetCurrentUser(): Observable<any> {
    return this.http.get<{ id: string; userName: string }>(
      `${this.apiUrl}/users/current`
    );
  }

  initUser(value: UserInfoModel) {
    this.user = value;
  }

  signUp(data: SignUpDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/signUp`, data);
  }
}
