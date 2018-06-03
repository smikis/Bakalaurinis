import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions, ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class UserService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }

  createUser(user: CreateUser) {
    let baseUrl = this.url.getApiUrl(Endpoints.users);
    return this.http.post(baseUrl,user,{ headers: this.login.applyAuthentication() });
  }

  updateUser(user: UpdateUser, id: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.getUser(id));
    return this.http.put(baseUrl,user,{ headers: this.login.applyAuthentication() });
  }

  searchUsers(searchQuery: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.searchUsers(searchQuery));
    return this.http.get<User[]>(baseUrl, { headers: this.login.applyAuthentication() });
  }

  getPage(page: number, pageSize: number, search?: string | undefined): Observable<UsersPage> {
    let baseUrl = this.url.getApiUrl(Endpoints.usersPage(page, pageSize)) + this.generateArgs(search);
    return this.http.get<UsersPage>(baseUrl, { headers: this.login.applyAuthentication() });
  }

  getUserLocation(userId: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.getUserLocation(userId));
    return this.http.get<Location>(baseUrl, { headers: this.login.applyAuthentication() });
  }

  getUser(userId: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.getUser(userId));
    return this.http.get<UserExtended>(baseUrl, { headers: this.login.applyAuthentication() });
  }

  generateArgs(search?: string | undefined): string {
    let args = "?";
    if (search !== undefined) {
      args += "&searchTerm=" + search;
    }
    return args;
  }

}

export interface UsersPage {
  total: number;
  data: User[];
}

export class CreateUser {
  name: string;
  lastName: string;
  address: string;
  email: string;
  password: string;
}

export class UpdateUser {
  name: string;
  lastName: string;
  address: string;
  email: string;
}


export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}

export interface UserExtended {
  id: string;
  name: string;
  lastName: string;
  email: string;
  userName: string;
  phoneNumber: string;
  address: string;
  registrationDate: string;
}


export interface Location {
  lat: number;
  lng: number;
  modifyDate: Date;
}
