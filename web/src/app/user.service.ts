import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class UserService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }

  searchUsers(searchQuery: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.searchUsers(searchQuery));
    return this.http.get<User[]>(baseUrl);
  }

  getPage(page:number, pageSize:number, search?: string | undefined) : Observable<UsersPage> {
    let baseUrl = this.url.getApiUrl(Endpoints.usersPage(page,pageSize)) + this.generateArgs(search);
    return this.http.get<UsersPage>(baseUrl);
  }

  getUserLocation(userId: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.getUserLocation(userId));
    return this.http.get<Location>(baseUrl);
  }

  getUser(userId: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.getUser(userId));
    return this.http.get<UserExtended>(baseUrl);
  }

  generateArgs(search?: string | undefined) : string {
    let args ="?";
      if(search !== undefined) {
        args += "&searchTerm=" + search;
      }        
    return args;
  }

}

export interface UsersPage {
    total : number;
    data : User[];
}
   

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}

export interface UserExtended {
  id:               string;
  name:             string;
  lastName:         string;
  email:            string;
  userName:         string;
  phoneNumber:      string;
  address:          string;
  registrationDate: string;
}


export interface Location {
  lat:        number;
  lng:        number;
  modifyDate: Date;
}
