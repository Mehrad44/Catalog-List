import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserRegister } from '../models/user-register.model';
import { UserToken } from '../models/user-token.model';
import { Observable } from 'rxjs';
import { UserLogin } from '../models/user-login.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  userServiceUrl: string = 'https://localhost:5001/users';

  constructor(private http: HttpClient) { }

  register(userRegister: UserRegister): Observable<UserToken> {
    let endPointUrl = `${this.userServiceUrl}/register`;

    return this.http.post<UserToken>(endPointUrl, userRegister);
  }

  login(userLogin: UserLogin): Observable<UserToken> {
    let endPointUrl = `${this.userServiceUrl}/login`;

    return this.http.post<UserToken>(endPointUrl, userLogin);
  }

  isAuthenticated(): boolean {
    return localStorage.getItem('token') != null;
  }

  logout() {
    localStorage.clear();
  }

  get userName(): string | null {
    return localStorage.getItem('userName');
  }
}
